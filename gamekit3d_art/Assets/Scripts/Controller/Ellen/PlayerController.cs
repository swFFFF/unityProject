using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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
    private void Awake()
    {
        playerInput = transform.GetComponent<PlayerInput>();
        characterController = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
    }

    private void Update()
    {
        //CalculateMove();
        CalculateVerticalSpeed();
        CalculateForwardSpeed();
        CalculateRotation();
    }

    private void OnAnimatorMove()
    {
        CalculateMove();
    }

    public void CalculateMove()
    {
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //Vector3 move = new Vector3(h, 0, v);

        if(isGrounded)
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
            }
        }
        else
        {
            if(!playerInput.Jump && verticalSpeed > 0)
            {
                verticalSpeed -= gravity * Time.deltaTime;
            }
            verticalSpeed -= gravity * Time.deltaTime;
        }

        animator.SetFloat("verticalSpeed", verticalSpeed);
    }

    private void CalculateForwardSpeed()
    {
        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed * playerInput.Move.normalized.magnitude, acceleratedSpeed * Time.deltaTime);
        animator.SetFloat("forwardSpeed",moveSpeed);

    }

    private void CalculateRotation()
    {
        if (playerInput.Move.x != 0 || playerInput.Move.y != 0)
        {
            Vector3 targetDirection = renderCamera.TransformDirection(new Vector3(playerInput.Move.x,0,playerInput.Move.y));
            targetDirection.y = 0;

            float turnSpeed = Mathf.Lerp(MaxAngleSpeed, MinAngleSpeed, moveSpeed / maxMoveSpeed) * Time.deltaTime; ;
            //transform.rotation = Quaternion.LookRotation(move); //旋转人物朝向
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), turnSpeed);   //人物朝向转动缓冲
        }
    }
}
