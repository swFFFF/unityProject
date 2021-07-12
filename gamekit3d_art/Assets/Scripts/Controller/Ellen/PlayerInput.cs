using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move
    {
        get
        {
            return _move;
        }
    }

    public bool Jump
    {
        get
        {
            return _jump;
        }
    }

    public bool Attack
    {
        get
        {
            return _attack;
        }
    }
    private Vector2 _move;
    private bool _jump;
    private bool _attack;

    private void Update()
    {
        _move.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        _jump = Input.GetButtonDown("Jump");
        _attack = Input.GetButtonDown("Fire1");
    }
}
