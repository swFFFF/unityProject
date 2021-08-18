using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region 字段
    protected Rigidbody rigidbody;
    public float time;
    public GameObject explosionEffect;
    #endregion

    #region 生命周期
    protected virtual void Awake()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }
    #endregion

    #region 方法
    public virtual void Shoot(Vector3 target, Vector3 direction)
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void Explositon()
    {
        if (explosionEffect != null)
        {
            GameObject explosion = GameObject.Instantiate(explosionEffect);
            explosion.transform.position = transform.position;
            Destroy(explosion, 2);
        }

        Destroy(gameObject, 0.2f);
    }
    #endregion
}
