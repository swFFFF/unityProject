using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class CheckPoint
{
    public Transform point;
    public float radius;
}
public class WeaponAttackController : MonoBehaviour
{
    #region 字段
    public CheckPoint[] checkPoint;
    public Color color;
    private RaycastHit[] results = new RaycastHit[10];
    public LayerMask layerMask;
    private bool isAttack;
    public int damage;
    public GameObject myself;
    private List<GameObject> attackList = new List<GameObject>();
    public GameObject hitPrefab;
    public UnityEvent OnAttack;
    #endregion

    #region Unity生命周期
    void Start()
    {

    }

    void Update()
    {
        if (!isAttack)
        {
            return;
        }
        CheckGameobject();

    }

    private void OnDrawGizmosSelected()
    {
        if(checkPoint == null)
        {
            return;
        }
        for (int i = 0; i < checkPoint.Length; i++)
        {
            if(checkPoint[i] == null)
            {
                return;
            }
            Gizmos.color = color;
            Gizmos.DrawSphere(checkPoint[i].point.position, checkPoint[i].radius);
        }
    }
    #endregion


    #region 方法
    public void StartAttack()
    {
        isAttack = true;
    }

    public void EndAttack()
    {
        isAttack = false;
        attackList.Clear();
    }

    //检测敌人
    public void CheckGameobject()
    {
        for (int i = 0; i < checkPoint.Length; i++)
        {
            int count = Physics.SphereCastNonAlloc(checkPoint[i].point.position, checkPoint[i].radius, Vector3.forward, results, 0, layerMask.value);
            for (int j = 0; j < count; j++)
            {
                //Debug.Log("检测到敌人 进行攻击：" + results[j].transform.name);
                if(CheckDamage(results[j].transform.gameObject))
                {
                    if(hitPrefab != null)
                    {
                        GameObject hit = GameObject.Instantiate(hitPrefab);
                        hit.transform.position = checkPoint[i].point.position;
                        Destroy(hit, 2);
                    }
                }
            }
        }
    }

    //造成伤害
    public bool CheckDamage(GameObject obj)
    {
        //判断游戏物体是不是可以受伤
        Damageable damageable = obj.GetComponent<Damageable>();
        if (damageable == null) { return false; }

        //检测到自己
        if (obj == myself)
        {
            return false;
        }

        if (attackList.Contains(obj))
        {
            return false;
        }
        //进行攻击
        DamageMessage data = new DamageMessage();
        data.damage = damage;
        data.damagePosition = myself.transform.position;
        damageable.OnDamage(data);
        OnAttack?.Invoke();
        attackList.Add(obj);
        return true;
    }
    #endregion



}
