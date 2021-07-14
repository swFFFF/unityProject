using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CheckPoint
{
    public Transform point;
    public float radius;
}
public class WeaponAttackController : MonoBehaviour
{
    public CheckPoint[] checkPoint;
    public Color color;
    private RaycastHit[] results = new RaycastHit[10];
    public LayerMask layerMask;
    public bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttack)
        {
            return;
        }
        CheckGameobject();

    }

    public void StartAttack()
    {
        isAttack = true;
    }

    public void EndAttack()
    {
        isAttack = false;
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
                CheckDamage(results[j].transform.gameObject);
            }
        }
    }
    
    //造成伤害
    public void CheckDamage(GameObject obj)
    {
        //判断游戏物体是不是可以受伤   
    }

    private void OnDrawGizmosSelected()
    {
        for(int i =0; i <checkPoint.Length; i++)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(checkPoint[i].point.position, checkPoint[i].radius);
        }
    }
}
