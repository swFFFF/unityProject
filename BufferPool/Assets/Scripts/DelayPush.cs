using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPush : MonoBehaviour
{
    void Start()
    {
        Invoke("Push", 1);
    }
    void OnEnable()
    {
        Invoke("Push", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Push()
    {
        PoolManager.GetInstance().PushObj(this.gameObject.name,this.gameObject);
    }
}
