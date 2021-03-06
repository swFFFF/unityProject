using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DamageMessage
{
    public int damage;//伤害
    public Vector3 damagePosition;  //伤害来源位置
    public bool isResetPosition;    //是否需要重置位置
}
[Serializable]
public class DamageEvent:UnityEvent<Damageable, DamageMessage>
{
    
}

public class Damageable : MonoBehaviour
{
    #region 字段
    public int maxHp;   //最大血量
    private int hp;     //当前血量

    public float invincibleTime = 0;//无敌时间
    private bool isInvicible = false;  //是否是无敌状态
    public float invincibleTimer = 0;//无敌时间计时
    public DamageEvent onHurt;
    public DamageEvent onDie;
    public DamageEvent onReset;
    public DamageEvent onInvincibleTimeOut;

    public int CurrentHp
    {
        get { return hp; }
    }

    public bool isAlive
    {
        get { return CurrentHp > 0; }
    }
    #endregion

    #region Unity生命周期
    private void Start()
    {
        hp = maxHp;
    }

    private void Update()
    {
        if (isInvicible)
        {
            invincibleTimer += Time.deltaTime;
            if (invincibleTimer >= invincibleTime)
            {
                isInvicible = false;
                invincibleTimer = 0;
                onInvincibleTimeOut?.Invoke(this,null);
            }
        }
    }
    #endregion

    #region 方法
    public void OnDamage(DamageMessage data)
    {
        if (hp <= 0)
        {
            return;
        }

        //无敌状态
        if (isInvicible)
        {
            return;
        }

        hp -= data.damage;

        isInvicible = true;

        if (hp <= 0)
        {
            //打死了
            onDie?.Invoke(this, data);
        }
        else
        {
            //受伤
            onHurt?.Invoke(this, data);
        }
    }
    public void ResetDamage()
    {
        hp = maxHp;
        isInvicible = false;
        invincibleTimer = 0;
        onReset?.Invoke(this, null);
    }
    #endregion
}
