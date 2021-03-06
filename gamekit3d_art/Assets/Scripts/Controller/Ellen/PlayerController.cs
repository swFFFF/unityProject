using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region 字段
    public float maxMoveSpeed = 5;
    public float moveSpeed = 0;
    public float jumpSpeed = 10;

    public float gravity = 20;
    public bool isGrounded = true;

    private float verticalSpeed;
    private CharacterController characterController;

    private PlayerInput playerInput;

    private Vector3 move;

    public Transform renderCamera;

    //public float angleSpeed = 400;  //旋转角速度

    private float MaxAngleSpeed = 1200;
    private float MinAngleSpeed = 400;
    public float acceleratedSpeed = 5;//人物加速度

    private Animator animator;

    private AnimatorStateInfo currentStateInfo;
    private AnimatorStateInfo nextStateInfo;

    public bool isCanAttack = false;

    public GameObject weapon;

    public Vector3 respawnPosition;

    public RandomAudioPlayer jumpPlayer;
    public RandomAudioPlayer attackPlayer;
    public RandomAudioPlayer stepPlayer;
    private CheckGroundMaterial groundMaterial;
    #endregion

    #region 常量
    //
    private int QuickTurnLeftHash = Animator.StringToHash("EllenQuickTurnLeft");
    private int QuickTurnRightHash = Animator.StringToHash("EllenQuickTurnRight");
    private int DeathHash = Animator.StringToHash("EllenDeath");
    #endregion

    #region Unity回调
    private void Awake()
    {
        playerInput = transform.GetComponent<PlayerInput>();
        characterController = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
        groundMaterial = transform.GetComponent<CheckGroundMaterial>();
    }

    private void Update()
    {
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        nextStateInfo = animator.GetNextAnimatorStateInfo(0);
        //CalculateMove();
        CalculateVerticalSpeed();
        CalculateForwardSpeed();
        CalculateRotation();

        animator.SetFloat("normalizedTime",Mathf.Repeat(currentStateInfo.normalizedTime, 1));
        animator.ResetTrigger("attack");
        if(playerInput.Attack && isCanAttack)
        {
            animator.SetTrigger("attack");
        }
    }
    private void OnAnimatorMove()
    {
        CalculateMove();
    }
    #endregion

    #region 方法
    public void CalculateMove()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //Vector3 move = new Vector3(h, 0, v);

        if (isGrounded)
        {
            move = animator.deltaPosition;
        }
        else
        {
            move = moveSpeed * transform.forward * Time.deltaTime;
        }

        //move.Set(playerInput.Move.x, 0, playerInput.Move.y);
        //move *= Time.deltaTime * moveSpeed;

        //move = renderCamera.TransformDirection(move);      //转换成跟随相机方向  不需要计算Y轴

        move += Vector3.up * verticalSpeed * Time.deltaTime;
        transform.rotation *= animator.deltaRotation;

        //move = transform.TransformDirection(move);     //转换成角色方向

        characterController.Move(move);
        isGrounded = characterController.isGrounded;
        animator.SetBool("isGround", isGrounded);
    }

    private void CalculateVerticalSpeed()
    {
        if (isGrounded)
        {
            verticalSpeed = -gravity * 0.3f;
            if (playerInput.Jump)
            {
                verticalSpeed = jumpSpeed;
                isGrounded = false;

                jumpPlayer.PlayRandomAudio();
            }
        }
        else
        {
            if (!playerInput.Jump && verticalSpeed > 0)
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }
            verticalSpeed -= gravity * Time.deltaTime;
        }

        if(!isGrounded)
        {
            animator.SetFloat("verticalSpeed", verticalSpeed);
        }
        
    }

    private void CalculateForwardSpeed()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed * playerInput.Move.normalized.magnitude, acceleratedSpeed * Time.deltaTime);
        animator.SetFloat("forwardSpeed", moveSpeed);

    }

    private void CalculateRotation()
    {
        if (playerInput.Move.x != 0 || playerInput.Move.y != 0)
        {
            Vector3 targetDirection = renderCamera.TransformDirection(new Vector3(playerInput.Move.x, 0, playerInput.Move.y));
            targetDirection.y = 0;

            float turnSpeed = Mathf.Lerp(MaxAngleSpeed, MinAngleSpeed, moveSpeed / maxMoveSpeed) * Time.deltaTime; ;
            //transform.rotation = Quaternion.LookRotation(move); //旋转人物朝向
            float turnAngle = Vector3.SignedAngle(transform.forward, targetDirection, Vector3.up);
            if (IsUpdateDirection())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), turnSpeed);   //人物朝向转动缓冲
            }

            animator.SetFloat("turnAngleRad", turnAngle * Mathf.Deg2Rad);
        }
    }

    public bool IsUpdateDirection()
    {
        //用hash判断播放的动画
        bool isUpdate = currentStateInfo.shortNameHash != QuickTurnLeftHash && currentStateInfo.shortNameHash != QuickTurnRightHash;
        isUpdate = nextStateInfo.shortNameHash != QuickTurnLeftHash && nextStateInfo.shortNameHash != QuickTurnRightHash;
        return isUpdate;
    }

    public void SetCanAttack(bool isAttack)
    {
        isCanAttack = isAttack;
    }

    public void ShowWeapon()
    {
        CancelInvoke("HideWeaponExcute");
        weapon.SetActive(true);
    }

    public void HideWeapon()
    {
        Invoke("HideWeaponExcute", 1);
    }

    public void HideWeaponExcute()
    {
        weapon.SetActive(false);
    }

    public void OnHurt(Damageable damageable, DamageMessage data)
    {
        Vector3 direction = data.damagePosition - transform.position;
        direction.y = 0;

        Vector3 localDirection = transform.InverseTransformDirection(direction); //转换成角色的局部方向

        //是否需要重置位置
        if (data.isResetPosition)
        {
            //失去控制
            playerInput.ReleaseControl();
            //播放死亡动画
            animator.SetTrigger("death");
            //重置位置
            StartCoroutine(ResetPosition());
        }
        else
        {
            //播放受伤动画
            animator.SetFloat("HurtX", localDirection.x);
            animator.SetFloat("HurtY", localDirection.z);
            animator.SetTrigger("hurt");
        }
    }

    public void OnDeath(Damageable damageable, DamageMessage data)
    {
        animator.SetTrigger("death");
        StartCoroutine(Respawn());
    }

    //重置位置
    public IEnumerator ResetPosition()
    {
        //播放死亡动画 
        while (currentStateInfo.shortNameHash != DeathHash)//等待死亡动画播放完毕
        {
            yield return null;
        }
        yield return null;

        //黑屏
        yield return StartCoroutine(BlackMaskView.Instance.FadeOut());//等待屏幕完全变黑再往下走

        //重置角色位置
        transform.position = respawnPosition;

        //播放重生动画
        animator.SetTrigger("respawn");

        yield return new WaitForSeconds(1.0f);

        //亮屏
        yield return StartCoroutine(BlackMaskView.Instance.FadeIn());//等待屏幕完全变黑再往下走


        //赋予角色控制权
        playerInput.GainControl();
        yield return null;
    }

    //重生
    public IEnumerator Respawn()
    {
        yield return ResetPosition();
        //重置角色血量
        transform.GetComponent<Damageable>().ResetDamage();

        //赋予角色控制权
        playerInput.GainControl();
    }

    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }
    #endregion

    #region 动画事件
    private void OnIdleStart()
    {
        animator.SetInteger("RandomIdle", -1);
    }

    private void OnIdleEnd()
    {
        animator.SetInteger("RandomIdle", Random.Range(0, 3));
    }

    private void MeleeAttackStart()
    {
        weapon.GetComponent<WeaponAttackController>().StartAttack();
        attackPlayer.PlayRandomAudio();
    }

    private void MeleeAttackEnd()
    {
        weapon.GetComponent<WeaponAttackController>().EndAttack();
    }

    public void PlayStep()
    {
        stepPlayer.PlayRandomAudio(groundMaterial.curMaterial);
    }
    #endregion



}
