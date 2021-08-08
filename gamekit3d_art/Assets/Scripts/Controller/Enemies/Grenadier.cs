using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : EnemyBase
{
    #region 字段
    public float shortAttackDistance;

    //获取动画哈希值
    private int rangeAttack2Hash = Animator.StringToHash("GrenadierRangeAttack2");
    private int meleeAttackHash = Animator.StringToHash("GrenadierMeleeAttack");
    private int closeRangeAttackHash = Animator.StringToHash("GrenadierCloseRangeAttack");

    private AnimatorStateInfo currentAnimatorInfo;  //当前动画信息

    public GameObject bossBulletPrefab;     //boss子弹预制体
    public Transform ShootPosition;         //发射点
    private BossBullet bossBullet;          //boss当前持有的子弹

    public WeaponAttackController meleeAttackController;
    public WeaponAttackController rangeAttackController;
    private GameObject shield;
    #endregion

    #region Unity生命周期
    private void Awake()
    {
        shield = transform.Find("Shield").gameObject;
    }
    protected override void Update()
    {
        base.Update();
        currentAnimatorInfo = animator.GetCurrentAnimatorStateInfo(0);
    }
    #endregion

    #region 方法
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

    //创建子弹
    public void CreateBullte()
    {
        GameObject bullet = GameObject.Instantiate(bossBulletPrefab, ShootPosition);
        bullet.transform.localPosition = Vector3.zero;
        bossBullet = bullet.GetComponent<BossBullet>();
    }

    //射击
    public void Shoot()
    {
        if(target != null)
        {
            //GameObject bullet = GameObject.Instantiate(bossBulletPrefab);
            //bullet.transform.position = ShootPosition.position;
            bossBullet.Shoot(target.transform.position, transform.forward);
        }
        else 
        {
            Destroy(bossBullet.gameObject);
        }
        bossBullet = null;
    }
    
    public override void OnDeath(Damageable damageable, DamageMessage data)
    {
        base.OnDeath(damageable, data);
        animator.SetTrigger("die");

        Destroy(gameObject, 8);
    }

    #endregion

    #region 动画事件
    public void MeleeAttackStart()
    {
        meleeAttackController.StartAttack();
    }
    public void MeleeAttackEnd()
    {
        meleeAttackController.EndAttack();
    }

    public void RangeAttackStart()
    {
        rangeAttackController.StartAttack();
        
        if(shield !=null)
        {
            shield.SetActive(false);
            shield.SetActive(true);
        }

    }

    public void RangeAttackEnd()
    {
        rangeAttackController.EndAttack();
        shield.SetActive(false);
    }

    public void RangeAttackBegin()
    {
        GameObject charge = transform.Find("GrenadierSkeleton/Grenadier_Root/Grenadier_Hips/Grenadier_Sphere/Charge").gameObject;
        charge.SetActive(false);
        charge.SetActive(true);
    }
    #endregion
}
