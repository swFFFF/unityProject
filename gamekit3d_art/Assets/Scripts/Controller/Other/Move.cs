using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//物体移动
public enum MoveType 
{
    Once,
    Again,
    Pingpong,
}

public enum PositionType
{
    World,
    Local,
}

public class Move : MonoBehaviour
{
    /// <summary>
    /// 开始移动位置
    /// </summary>
    public Vector3 startPosition;
    /// <summary>
    /// 停止移动位置
    /// </summary>
    public Vector3 endPosition;
    /// <summary>
    /// 移动时间
    /// </summary>
    public float time =  1f;
    /// <summary>
    /// 计时器
    /// </summary>
    private float timer;

    protected float percent;

    private bool isMoving = false;
    /// <summary>
    /// 移动类型
    /// </summary>
    public MoveType moveType = MoveType.Once;
    /// <summary>
    /// 位置类型
    /// </summary>
    public PositionType positionType = PositionType.Local;

    public UnityEvent onMoveEnd;
    /// <summary>
    /// 是否初始化时开始移动
    /// </summary>
    public bool moveOnAwake = false;

    public float delayTime = 0;
    private float delayTimer = 0;

    private void Awake()
    {
        if(moveOnAwake)
        {
            StartMove();
        }
    }

    private void Update()
    {
        if(isMoving)
        {
            CalculateMove();
        }
    }

    public void StartMove()
    {
        isMoving = true;
        timer = 0;
    }

    private void CalculateMove()
    {
        if(delayTimer < delayTime)
        {
            delayTimer += Time.deltaTime;
            return;
        }

        timer += Time.deltaTime / time;
        switch (moveType)
        {
            case MoveType.Once:
                percent = Mathf.Clamp01(timer);
                break;
            case MoveType.Again:
                percent = Mathf.Repeat(timer, 1);
                break;
            case MoveType.Pingpong:
                percent = Mathf.PingPong(timer, 1);
                break;
            default:
                break;
        }

        MoveExcute();

        if (timer >= 1 && moveType == MoveType.Once)
        {
            isMoving = false;
            timer = 0;
            onMoveEnd?.Invoke();
        }
    }

    protected virtual void MoveExcute()
    {
        switch (positionType)
        {
            case PositionType.World:
                transform.position = Vector3.Lerp(startPosition, endPosition, percent);
                break;
            case PositionType.Local:
                transform.localPosition = Vector3.Lerp(startPosition, endPosition, percent);
                break;
            default:
                break;
        }
    }
}
