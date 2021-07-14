using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EllenAttackSMB : StateMachineBehaviour
{
    public int index;
    Transform attackEffect;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetComponent<PlayerController>().ShowWeapon();

        attackEffect = animator.transform.Find("TrailEffect/Ellen_Staff_Swish0" + index);
        attackEffect.gameObject.SetActive(false);
        attackEffect.gameObject.SetActive(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.GetComponent<PlayerController>().HideWeapon();
        attackEffect.gameObject.SetActive(false);
    }
}
