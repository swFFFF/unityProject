using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomper : EnemyBase
{
    public WeaponAttackController weaponAttackController;
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

    public void AttackBegin()
    {
        weaponAttackController.StartAttack();
    }

    public void AttackEnd()
    {
        weaponAttackController.EndAttack();
    }
}
