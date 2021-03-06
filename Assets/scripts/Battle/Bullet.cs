﻿using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

public class Bullet : ArenaObject 
{
    private static readonly float SmoothTightness = 0.1f;
    private static readonly float DefaulTightness = 1.0f;

    [SerializeField]
    private proto_game.Bullets bulletType = proto_game.Bullets.Common;

    [SerializeField]
    private float moveSpeed = -1.0f;

    [SerializeField]
    private float timeAlive = 1.4f;

    [SerializeField]
    protected bool penetrate = false;

    [SerializeField]
    protected bool predictable = true;

    protected Unit owner_;
    private float speed_;
    private Vector3 serverDirection_;
    private Vector2 desiredPosition_;
    private Vector3 direction_;
    private float damage_ = 0;
    private float lifetimeLeft_ = 0.0f;
    private Vector2 startPoint_;
    private Vector2 serverVelocity_;
    private float tightness_ = DefaulTightness;
    private GameObject ghost_ = null;
    private bool noCollision_ = false;

    public bool TriggerDamageOnSync
    { get; set; }

    public Vector2 StartPoint
    {
        get { return startPoint_; }
    }

    public int TickOfCreation
    { get; set; }

    public List<int> Targets
    { get; set; }

    public float Radius
    {
        get { return (collider as CircleCollider2D).radius; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public Unit Owner
    { get { return owner_; } }

    public float Damage
    { get { return damage_; } }

    #if UNITY_EDITOR
    public proto_game.Bullets Type
    {
        get { return bulletType; }
    }


    #endif

    public float TimeAlive
    { get { return timeAlive; } }

    public bool Penetrate
    { get { return penetrate; } }

    public Vector2 ServerDirection
    {
        get { return serverDirection_; }
    }

    public Vector2 ServerPosition
    {
        get; set; 
    }

    public Vector2 Direction
    {
        get { return direction_; }
    }

    public void SetOwner(Unit owner)
    {
        owner_ = owner;
    }

    public void SetDamage(float damage)
    {
        damage_ = damage;
    }

    public void SetMoveSpeed(float speed)
    {
        speed_ = speed;
    }

    public void SetStartPoint(Vector2 pos)
    {
        startPoint_ = pos;
        ServerPosition = pos;
        Position = pos;
        DesiredPosition = pos;
    }

    public void SetDirection(float dir)
    {
        float rad = dir;
        float x = Vector2.right.x * Mathf.Cos(rad) - Vector2.right.y * Mathf.Sin(rad);
        float y = Vector2.right.x * Mathf.Sin(rad) + Vector2.right.y * Mathf.Cos(rad);
        SetDirection(new Vector2(x, y));
        sprite.transform.eulerAngles = new Vector3(0,0,dir*Mathf.Rad2Deg + 90.0f);
    }

    public void SetDirection(Vector2 dir)
    {
        dir.Normalize();
        serverDirection_ = direction_ = dir;
        //sprite.transform.Rotate(Vector3.forward, Vector2.Angle(Position, dir));
    }

    public void AdjustPositionToServerTime(float timeAlpha)
    {
        var correctedLifeTime = timeAlive;
        if (timeAlpha > 0)
        {
            //adjust bullet to server time spawn, move slightly forward, since we predict player's movement
            //remove difference in time from total bullet lifetime
            correctedLifeTime -= timeAlpha;
            //advance bullet's spawn point by alpha var advance = direction_;
            var advance = serverDirection_;
            advance.x *= speed_ * timeAlpha;
            advance.y *= speed_ * timeAlpha;
            DesiredPosition += new Vector2(advance.x, advance.y);
            ServerPosition = DesiredPosition;
            tightness_ = SmoothTightness;
            lifetimeLeft_ = correctedLifeTime;
        }
    }

    public void ResetTimeAlive()
    {
        lifetimeLeft_ = timeAlive;
    }

    public override void Init(arena.ArenaController controller)
    {
        base.Init(controller);
        lifetimeLeft_ = timeAlive;

        if (Targets == null)
        {
            Targets = new List<int>();
        }

        if (Mathf.Approximately(speed_, 0.0f))
        {
            speed_ = moveSpeed;
        }

        var color = sprite.color;
        color.a = 1.0f;
        sprite.color = color;
        DisableVelocityMovement = true;
        TriggerDamageOnSync = false;
        noCollision_ = false;
        ID = -1;
    }

    private Vector2 DesiredPosition
    {
        set 
        { 
            desiredPosition_ = value; 
            tightness_ = SmoothTightness;
        }
        get 
        {
            return desiredPosition_;
        }
    }

    public override void OnUpdate(float dt)
    {
        base.OnUpdate(dt);

        lifetimeLeft_ -= dt;
        serverVelocity_ = speed_ * serverDirection_;
        ServerPosition += serverVelocity_ * dt;
        desiredPosition_ = ServerPosition + MovementAdjustment;

        if (lifetimeLeft_ <= 0)
        {
            OnExpired();
        }
        else 
        {
            Position = Position + (desiredPosition_ - Position) * tightness_;
            tightness_ += (DefaulTightness - tightness_) * 0.01666f;
            if (ghost_ != null)
                ghost_.transform.localPosition = new Vector3(ServerPosition.x, ServerPosition.y, 0);
        }
    }

    public void OnExpired()
    {
        noCollision_ = true;

        if (owner_.Controller.IsLocalPlayer(owner_) && predictable)
        {
            (owner_ as Player).BulletExpired(this);
        }
        else 
        {
            owner_.Controller.OnBulletExpired(this);
            owner_.UnregisterBullet(this);
        }
    }

    public Vector2 MovementAdjustment
    { get; set; }

    public virtual void Prepare()
    {
        Targets.Clear();
        Velocity = serverDirection_ * speed_;
        serverVelocity_ = Velocity;

        var newSize = initialScale_;
        var scale = owner_.Stats.GetFinValue(proto_game.Stats.BulletSize);
        newSize.Scale(new Vector3(scale,scale,scale));
        transform.localScale = newSize;
        MovementAdjustment = Vector2.zero;

        /*ghost_ = gameObject.GetPooled();
        ghost_.GetComponent<SpriteRenderer>().color = Color.red;
        owner_.Controller.gameObject.AddChild(ghost_);*/
    }

    protected virtual void HandleCollision(Entity target)
    {
        var delayedSync = owner_.Controller.IsLocalPlayer(owner_) && !(Owner as Player).IsSyncedWithServer(this) && predictable;

        if (delayedSync)
        {
            //delay damage done packet till bullet will be synced with server
            TriggerDamageOnSync |= true;
        }

        if (!Penetrate)
        {
            noCollision_ = true;
            PlayDestroyAnimation(false);
        }

        Targets.Add(target.ID);

        if (!delayedSync)
            owner_.Controller.OnBulletCollision(target.ID, this);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (Destroyed) return;
        if (other.gameObject == owner_.gameObject) return;

        Entity target = other.gameObject.GetComponent<Entity>();

        if (target != null)
        {
            var enemyMask = LayerMask.NameToLayer("Enemy");
            var playerMask = LayerMask.NameToLayer("Player");
            var expBlockMask = LayerMask.NameToLayer("Block");

            var ownerLayer = owner_.gameObject.layer;
            var targetLayer = target.gameObject.layer;

            //handle AI bullets and local player bullets collisions
            if ((ownerLayer == enemyMask && targetLayer == playerMask) ||
                (owner_.Controller.IsLocalPlayer(owner_) && 
                    (targetLayer == expBlockMask || targetLayer == enemyMask || targetLayer == playerMask)))
            {
                if (!noCollision_)
                    HandleCollision(target);
            }
        }
    }

    protected override void OnBeforeRemove()
    {
        base.OnBeforeRemove();

        speed_ = 0.0f;
    }

    protected override void OnRemove()
    {
        base.OnRemove();
        //safe to use owner here, in case owner already pooled back
        if (ghost_ != null)
            ghost_.ReturnPooled();
    }
}
