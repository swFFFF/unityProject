using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomper : EnemyBase
{
    public WeaponAttackController weaponAttackController;

    protected override void Start()
    {
        base.Start();
        animator.Play("ChomperIdle", 0, UnityEngine.Random.Range(0.0f,1.0f));
    }

    public override void Attack()
    {
        ChangeDirection();
        base.Attack();
        animator.SetTrigger("attack");
    }

    //修改方向
    public void ChangeDirection()
    {
        if(target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.LookRotation(direction);//修改角度
        }
    }

    public virtual void AttackBegin()
    {
        weaponAttackController.StartAttack();
    }

    public virtual void AttackEnd()
    {
        weaponAttackController.EndAttack();
    }

    public override void OnDeath(Damageable damageable, DamageMessage data)
    {
        base.OnDeath(damageable, data);

        //丢失目标
        LoseTarget();
        //停止追踪
        meshAgent.isStopped = true;
        meshAgent.enabled = false;
        //播放死亡动画
        animator.SetTrigger("death");
        //给一个力
        Vector3 force = transform.position - data.damagePosition;//攻击游戏物体的位置
        force.y = 0;
        myRigidbody.isKinematic = false;
        myRigidbody.AddForce(force.normalized * 8 + Vector3.up * 4, ForceMode.Impulse);

        Invoke("Dissolve", 3); 
    }

    public void Dissolve()
    {
        transform.Find("Body").gameObject.SetActive(false);
        transform.Find("Body_dissolve").gameObject.SetActive(true);
        Destroy(gameObject, 1);
    }
}
