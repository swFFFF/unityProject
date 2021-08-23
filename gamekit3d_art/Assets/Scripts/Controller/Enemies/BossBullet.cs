using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bullet
{

    public override void Shoot(Vector3 target, Vector3 direction)
    {
        base.Shoot(target, direction);
        transform.SetParent(null);
        rigidbody.isKinematic = false;
        Vector3 toTarget = target - transform.position;
        toTarget.y = 0;
        float speed = (toTarget.magnitude / time) * 2.0f;//向量长度除时间
        rigidbody.velocity = direction.normalized * speed + Vector3.up * 3; //刚体速度 = 方向 * 速度值 + 向上的速度 （抛射）
        Invoke("Attack", time);
    }

    public override void Attack()
    {
        base.Attack();
        //爆炸
        Explositon();
        //攻击
        transform.GetComponent<WeaponAttackController>().StartAttack();
    }
}
