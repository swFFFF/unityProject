using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExpandMethod 
{
    public static Move DoMove(this Transform trans,Vector3 targetPos, float time)
    {
        Move move = trans.gameObject.AddComponent<Move>();
        move.StartMove(time, targetPos);

        GameObject.Destroy(move, time + 0.5f);
        return move;
    }
}
public class TestExpand : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        transform.DoMove(target.transform.position,5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
