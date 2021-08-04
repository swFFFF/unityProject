using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : EnemyBase
{
    public float shortAttackDistance;

    //获取动画哈希值
    private int rangeAttack2Hash = Animator.StringToHash("GrenadierRangeAttack2");
    private int meleeAttackHash = Animator.StringToHash("GrenadierMeleeAttack");
    private int closeRangeAttackHash = Animator.StringToHash("GrenadierCloseRangeAttack");

    private AnimatorStateInfo currentAnimatorInfo;  //当前动画信息

    protected override void Update()
    {
        base.Update();
        currentAnimatorInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    public override void Attack()
    {
        base.Attack();

        if(currentAnimatorInfo.shortNameHash == rangeAttack2Hash || 
            currentAnimatorInfo.shortNameHash == meleeAttackHash || 
            currentAnimatorInfo.shortNameHash == closeRangeAttackHash)
        {
            animator.ResetTrigger("attack_long");
            animator.ResetTrigger("attack_range");
            animator.ResetTrigger("attack_short");
            return;
        }

        if(Vector3.Distance(transform.position, target.transform.position) > shortAttackDistance)
        {
            // 进行远程攻击
            Turn();
            animator.ResetTrigger("attack_long");
            animator.SetTrigger("attack_long");
        }
        else
        {
            //TODO 近距离攻击 
            if(Vector3.Angle( transform.forward, target.transform.position - transform.position) > 20)
            {
                //范围攻击
                animator.ResetTrigger("attack_range");
                animator.SetTrigger("attack_range");
            }
            else
            {
                //普通攻击
                animator.ResetTrigger("attack_short");
                animator.SetTrigger("attack_short");
            }
        }
    }

    protected override void OnAnimatorMove()
    {
        //base.OnAnimatorMove();
        transform.rotation *= animator.deltaRotation;
    }

    //转向
    public void Turn()
    {
        //计算角度
        float angle = Vector3.SignedAngle(transform.forward, target.transform.position - transform.position, Vector3.up);
        if(Mathf.Abs(angle) > 10)
        {
            animator.SetFloat("TurnAngle", angle);
            animator.SetTrigger("turn");
        }
    }
}
