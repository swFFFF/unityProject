using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]    //使用本脚本自动添加相应组件
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Damageable))]
public class EnemyBase : MonoBehaviour
{
    #region 字段
    public float checkDistance; //检测的距离
    public float maxHeightDiff; //最大高度差
    [Range(0, 180)]
    public float lookAngle;     //视野范围     

    RaycastHit[] results = new RaycastHit[10];
    public LayerMask layerMask;
    public GameObject target;
    protected NavMeshAgent meshAgent;
    public float followDistance;
    public float attackDistance;
    protected Vector3 startPosition;
    public float runSpeed = 4;
    public float walkSpeed = 2;
    protected float moveSpeed = 0;
    public Animator animator;
    protected Rigidbody myRigidbody;
    protected bool isCanAttack;
    public float attackCooldownTime;    //攻击冷却时间
    protected float attackCooldownTimer;    //冷却倒计时
    protected Damageable damageable;
    #endregion

    #region Unity生命週期
    protected virtual void Start()
    {
        meshAgent = transform.GetComponent<NavMeshAgent>();
        startPosition = transform.position;
        animator = transform.GetComponent<Animator>();
        myRigidbody = transform.GetComponent<Rigidbody>();
        damageable = transform.GetComponent<Damageable>();
    }

    protected virtual void Update()
    {
        if(!damageable.isAlive)
        {
            return;
        }
        if (target != null && target.GetComponent<PlayerInput>() != null)
        {
            if (target.GetComponent<PlayerInput>().IsHaveControl() == false && target.GetComponent<Damageable>().isAlive)
            {
                animator.speed = 0;
                return;
            }
            else
            {
                animator.speed = 1;
            }
        }

        CheckTarget();
        FollowTarget();

        if (!isCanAttack)
        {
            attackCooldownTimer += Time.deltaTime;
            if (attackCooldownTimer >= attackCooldownTime)
            {
                isCanAttack = true;
                attackCooldownTimer = 0;
            }
        }
    }

    protected virtual void OnAnimatorMove()
    {
        myRigidbody.MovePosition(transform.position + animator.deltaPosition);//和CharactorController传参有区别
    }
    protected virtual void OnDrawGizmosSelected()
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
    #endregion

    #region 方法
    //检测目标
    public virtual void CheckTarget()
    {
        int count = Physics.SphereCastNonAlloc(transform.position, checkDistance, Vector3.forward, results, 0, layerMask.value);
        for (int i = 0; i < count; i++)
        {
            //判断物体是否可以攻击
            if (results[i].transform.GetComponent<Damageable>() == null) { continue; }
            //判断高度差
            if (Mathf.Abs(results[i].transform.position.y - transform.position.y) > maxHeightDiff) { continue; }

            //判断是否在视野范围内
            if (Vector3.Angle(transform.forward, results[i].transform.position - transform.position) > lookAngle) { continue; }

            //目标是否存活
            if (!results[i].transform.GetComponent<Damageable>().isAlive)
            {
                continue;
            }
            //找到一个最近的攻击目标
            if (target != null)
            {
                //判断距离
                float distance = Vector3.Distance(transform.position, target.transform.position);
                float curDistance = Vector3.Distance(transform.position, results[i].transform.position);
                if (curDistance < distance)
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
    public virtual void MoveToTarget()
    {
        if (target != null)
        {
            if (transform.GetComponent<Damageable>().isAlive)
            {
                meshAgent.SetDestination(target.transform.position);
            }
        }
    }

    //追踪目标
    public virtual void FollowTarget()
    {
        ListenerSpeed();
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

    //丢失目标
    public virtual void LoseTarget()
    {
        //目标丢失 回到初始位置
        target = null;
        if (transform.GetComponent<Damageable>().isAlive)
        {
            meshAgent.speed = walkSpeed;
            meshAgent.SetDestination(startPosition);
        }
    }

    //监听速度
    public virtual void ListenerSpeed()
    {
        if (target != null)
        {
            moveSpeed = runSpeed;

        }
        meshAgent.speed = moveSpeed;//设置导航最大移动速度
        animator.SetFloat("speed", meshAgent.velocity.magnitude);//把导航当前移动速度赋值给speed
    }

    public virtual void Attack()
    {

    }

    public virtual void OnDeath(Damageable damageable, DamageMessage data)
    {

    }

    #endregion
}
