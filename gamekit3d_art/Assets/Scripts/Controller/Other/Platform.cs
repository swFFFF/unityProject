using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : Move
{
    CharacterController player;//玩家
    Vector3 detalPositon;

    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<CharacterController>();
    }

    private void OnTriggerExit(Collider other)
    {
        player = null;
    }


    protected override void MoveExcute()
    {
        switch (positionType)
        {
            case PositionType.World:
                detalPositon = Vector3.Lerp(startPosition, endPosition, percent) - transform.position;
                break;
            case PositionType.Local:
                detalPositon = Vector3.Lerp(startPosition, endPosition, percent) - transform.localPosition;
                break;
            default:
                break;
        }

        base.MoveExcute();

        if (player != null)
        {
            player.Move(detalPositon);
        }


    }
}
