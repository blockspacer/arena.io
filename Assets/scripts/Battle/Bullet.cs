﻿using UnityEngine;

using System;
using System.Collections;

using DG.Tweening;

public class Bullet : MonoBehaviour 
{
    [SerializeField]
    private new Collider2D collider = null;

    [SerializeField]
    private SpriteRenderer sprite = null;

    private Entity owner_;
    private float speed_;
    private Vector3 direction_;
    private int collsionCount_ = 0;
    private float damage_ = 0;
    private Transform transform_;
    private const float timeAlive = 1.4f;
    private Tweener moveTween_;
    private Vector3 oldSize_;
    private bool destroyed_ = false;

    public Collider2D Collider
    { get { return collider; } }

    public void Init(Entity owner, Vector3 direction, float speed, Vector3 spawnPoint, float damage)
    {
        destroyed_ = false;
        owner_ = owner;
        transform_ = transform;
        oldSize_ = transform.localScale;
        var newSize = oldSize_;
        var scale = owner_.Stats.GetFinValue(proto_game.Stats.BulletSize);
        newSize.Scale(new Vector3(scale,scale,scale));
        transform.localScale = newSize;
        transform_.localPosition = spawnPoint;
        direction_ = direction;
        direction_.Normalize();

        speed_ = speed;
        collsionCount_ = 0;
        damage_ = damage;

        var MaxDistanceToTravel = timeAlive * speed_;
        var move = direction_;
        move.x *= MaxDistanceToTravel;
        move.y *= MaxDistanceToTravel;

        moveTween_ = transform_.DOLocalMove(move+spawnPoint, timeAlive);
        moveTween_.SetEase(Ease.InSine);
        moveTween_.OnComplete(()=>
        {
            HandleDestroyBullet();
        });

        var color = sprite.color;
        color.a = 1.0f;
        sprite.color = color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyed_) return;
        if (other.gameObject == owner_.gameObject) return;

        Entity entity = other.gameObject.GetComponent<Entity>();

        if (entity != null)
        {
            if (owner_.Local)
                owner_.DealDamage(entity, damage_);
            collsionCount_++;

            //if (owner_.MaxPenetration == collsionCount_)
            {
                HandleDestroyBullet();
            }
        }
    }

    void HandleDestroyBullet()
    {
        destroyed_ = true;
        sprite.DOFade(0.0f, 0.1f).OnComplete(()=>
        {
            gameObject.ReturnPooled();
            moveTween_.Kill(false);
            moveTween_ = null;
        });
        transform_.localScale = oldSize_;
    }
}
