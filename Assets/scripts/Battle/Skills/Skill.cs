﻿using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour 
{
    [SerializeField]
    protected float recoil = 0.0f;

    [SerializeField]
    protected proto_game.Skills type;

    private long lastCastTime_ = 0;

    #if UNITY_EDITOR
    public float Recoil
    {
        get { return recoil; }
    }

    public proto_game.Skills Type
    { get { return type; } }
    #endif

    public long Cooldown
    { get; set; }

    public Unit Owner
    { get; set; }

    public void Cast(long serverSpawnTime = -1, int firstBulletID = -1)
    {
        OnCast(serverSpawnTime, firstBulletID);
        lastCastTime_ = GameApp.Instance.ServerTimeMs();
    }

    public bool CanCast()
    {
        var time = GameApp.Instance.ServerTimeMs();
        if (time - lastCastTime_ < Cooldown)
        {
            return false;
        }
        return true;
    }

    protected virtual void OnCast(long serverSpawnTime = -1, int firstBulletID = -1)
    {}
}
