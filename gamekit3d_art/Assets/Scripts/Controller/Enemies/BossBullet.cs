using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    Rigidbody rigidbody;
    public float time;
    private void Awake()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }

    public void Shoot(Vector3 target, Vector3 direction)
    {
        rigidbody.isKinematic = false;
        Vector3 toTarget = target - transform.position;
        toTarget.y = 0;
        float speed = (toTarget.magnitude / time) *2.0f ;//向量长度除时间
        rigidbody.velocity = direction.normalized * speed + Vector3.up * 3; //刚体速度 = 方向 * 速度值 + 向上的速度 （抛射）
        Invoke("Attack", time);
    }

    public void Attack()
    {
        //爆炸

        //攻击
    }
}
