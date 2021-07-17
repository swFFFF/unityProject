using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomper : MonoBehaviour
{
    public float checkDistance; //检测的距离
    public float maxHeightDiff; //最大高度差
    [Range(0,180)]
    public float lookAngle;     //视野范围     

    RaycastHit[] results = new RaycastHit[10];
    public LayerMask layerMask;
    public GameObject target;
    private NavMeshAgent meshAgent;
    public float followDistance;
    public float attackDistance;
    private Vector3 startPosition;

    private void Start()
    {
        meshAgent = transform.GetComponent<NavMeshAgent>();
        startPosition = transform.position;
    }

    private void Update()
    {
        CheckTarget();
        FollowTarget();
    }

    //检测目标
    public void CheckTarget()
    {
        int count = Physics.SphereCastNonAlloc(transform.position, checkDistance, Vector3.forward , results, 0 , layerMask.value);
        for(int i = 0; i <count; i++)
        {
            //判断物体是否可以攻击
            if (results[i].transform.GetComponent<Damageable>() == null) { continue; }
            //判断高度差
            if (Mathf.Abs(results[i].transform.position.y - gameObject.transform.position.y) > maxHeightDiff) { continue; }
            //判断是否在视野范围内
            if (Vector3.Angle(transform.forward, results[i].transform.position - gameObject.transform.position) > lookAngle) { continue; }
            //找到一个最近的攻击目标

            if(target != null)
            {
                //判断距离
                float distance = Vector3.Distance(transform.position, target.transform.position);
                float curDistance = Vector3.Distance(transform.position, results[i].transform.position);
                if(curDistance < distance)
                {
                    target = results[i].transform.gameObject;
                }
            }
            else
            {
                target = results[i].transform.gameObject;
            }
            
        }
    }

    //向目标移动
    public void MoveToTarget()
    {
        if(target != null)
        {
            meshAgent.SetDestination(target.transform.position);
        }
    }

    //追踪目标
    public void FollowTarget()
    {
        if (target != null)
        {
            try
            {
                //向目标移动
                MoveToTarget();
                //判断路径是否有效 
                if (meshAgent.pathStatus == NavMeshPathStatus.PathPartial || meshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    //目标丢失
                    LoseTarget();
                    return;
                }
                //是否再追踪距离内
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) > followDistance)
                {
                    //目标丢失
                    LoseTarget();
                    return;
                }
                //是否再攻击范围内
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= attackDistance)
                {
                    //攻击 TODO
                    Debug.Log("攻击");
                }
            }
            catch (Exception)
            {
                //追踪出错 目标丢失
                LoseTarget();
            }
            
        }
    }

    public void LoseTarget()
    {
        //目标丢失 回到初始位置
        meshAgent.SetDestination(startPosition);
    }

    private void OnDrawGizmosSelected()
    {
        //画出检测范围
        Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.4f);
        Gizmos.DrawSphere(transform.position, checkDistance);
        //画出追踪距离
        Gizmos.color = new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.4f);
        Gizmos.DrawSphere(transform.position, followDistance);
        //画出攻击距离
        Gizmos.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.4f);
        Gizmos.DrawSphere(transform.position, attackDistance);
        //画出高度差检测线
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * maxHeightDiff);
        Gizmos.DrawLine(transform.position, transform.position - Vector3.up * maxHeightDiff);
        //画出视野范围检测线
        UnityEditor.Handles.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.4f);
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, lookAngle, checkDistance);
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.down, transform.forward, lookAngle, checkDistance);
    }
}
