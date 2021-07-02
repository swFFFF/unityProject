using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 startPosiotion;

    public float time;

    private float timer;
    private bool isMoving = false;

    public void StartMove(float time, Vector3 targetPos)
    {
        isMoving = true;
        timer = 0;
        this.time = time;
        this.targetPosition = targetPos;
        this.startPosiotion = transform.position;
    }

    public void StopMove()
    {
        isMoving = false;
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            timer += Time.deltaTime * 1 / time;
            transform.position = Vector3.Lerp(startPosiotion, targetPosition, timer);
            if(timer >= 1)
            {
                isMoving = false;
                timer = 0;
            }
        }
    }
}
