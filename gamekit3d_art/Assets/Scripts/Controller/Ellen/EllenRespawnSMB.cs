﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenRespawnSMB : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)   //进入状态机时调用
    {
        animator.transform.Find("Ellen_Body").gameObject.SetActive(false);
        animator.transform.Find("Ellen_Body_Respawn").gameObject.SetActive(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)    //退出状态机时调用
    {
        animator.transform.Find("Ellen_Body").gameObject.SetActive(true);
        animator.transform.Find("Ellen_Body_Respawn").gameObject.SetActive(false);
    }
}
