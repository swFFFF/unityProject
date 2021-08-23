﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //单例
    public static PlayerInput Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2 Move
    {
        get
        {
            if(!isCanControl)
            {
                 return Vector2.zero;
            }
            return _move;
        }
    }

    public bool Jump
    {
        get
        {

            return _jump && isCanControl;
        }
    }

    public bool Attack
    {
        get
        {
            return _attack && isCanControl;
        }
    }

    public bool Pause
    {
        get
        {
            return _pause && isCanControl;
        }
    }
    private Vector2 _move;
    private bool _jump;
    private bool _attack;
    private bool _pause;

    private bool isCanControl = true;
    private void Update()
    {
        _move.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _jump = Input.GetButtonDown("Jump");
        _attack = Input.GetButtonDown("Fire1");
        _pause = Input.GetButtonDown("Pause");
    }

    //获得控制
    public void GainControl()
    {
        isCanControl = true;
    }

    //失去控制
    public void ReleaseControl()
    {
        isCanControl = false;
    }

    //是否有控制权
    public bool IsHaveControl()
    {
        return isCanControl;
    }
}
