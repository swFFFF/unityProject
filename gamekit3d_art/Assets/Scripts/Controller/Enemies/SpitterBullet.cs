using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterBullet : Bullet
{
    protected override void Awake()
    {
        base.Awake();
        Destroy(gameObject, 8);
    }

    public override void Shoot(Vector3 target, Vector3 direction)
    {
        base.Shoot(target, direction);
        target += Vector3.up * 0.5f;
        //竖直向上的速度
        float g = Mathf.Abs(Physics.gravity.y);
        float v0 = 8;
        float t0 = v0 / g;
        float y0 = 0.5f * g * t0 * t0;//加速度公式

        float t = 0;

        if (transform.position.y + y0 > target.y)
        {

            float y = transform.position.y - target.y + y0;
            t = Mathf.Sqrt(y * 2 / g) + t0;
        }
        else
        {
            //射程不足 从目标点下方经过
            t = t0;
        }

        Vector3 transPos = transform.position;
        transPos.y = 0;
        target.y = 0;
        float speed = Vector3.Distance(transPos, target) / t;

        Vector3 velocity = direction.normalized * speed + Vector3.up * v0;
        rigidbody.isKinematic = false;
        rigidbody.velocity = velocity;
        transform.GetComponent<WeaponAttackController>().StartAttack();
    }

    public override void Attack()
    {
        base.Attack();
        Destroy(gameObject);
    }
}
