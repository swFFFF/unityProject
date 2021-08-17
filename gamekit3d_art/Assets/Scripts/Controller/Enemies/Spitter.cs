using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spitter : Chomper
{
    //逃跑距离
    public float escapeDistance;
    public override void FollowTarget()
    {
        ListenerSpeed();

        //base.FollowTarget();
        if (target != null)
        {
            try
            {
                if (Vector3.Distance(transform.position, target.transform.position) <= escapeDistance)
                {
                    //逃跑
                    Escape();
                    return;
                }
                //向目标移动
                MoveToTarget();
                //判断路径是否有效 
                if (meshAgent.pathStatus == NavMeshPathStatus.PathPartial || meshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    if(Vector3.Distance(transform.position, target.transform.position) > attackDistance)
                    {
                        LoseTarget();
                        return;
                    }
                }
                //是否再追踪距离内
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) > followDistance)
                {
                    //目标丢失
                    LoseTarget();
                    return;
                }
                //目标是否存活
                if (!target.transform.GetComponent<Damageable>().isAlive)
                {
                    LoseTarget();
                    return;
                }

                //是否再攻击范围内
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= attackDistance)
                {
                    if (isCanAttack)
                    {
                        Attack();
                        isCanAttack = false;
                    }
                }
            }
            catch (Exception)
            {
                //追踪出错 目标丢失
                LoseTarget();
            }

        }
    }

    public void Escape()
    {
        animator.ResetTrigger("attack");
        meshAgent.isStopped = false;
        meshAgent.speed = moveSpeed;
        Vector3 targ = transform.position + (transform.position - target.transform.position).normalized;
        meshAgent.SetDestination(targ);
    }

    protected override void OnAnimatorMove()
    {
        //base.OnAnimatorMove();
    }
    public override void AttackBegin()
    {
        //base.AttackBegin();
    }

    public override void AttackEnd()
    {
        //base.AttackEnd();
    }

    public override void MoveToTarget()
    {
        base.MoveToTarget();
        if(Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
        {
            meshAgent.isStopped = true;
        }
        else
        {
            meshAgent.isStopped = false;
        }
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        UnityEditor.Handles.color = new Color(Color.grey.r, Color.gray.g, Color.gray.b, 0.2f);
        UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, transform.forward, 360, escapeDistance);
    }
}
