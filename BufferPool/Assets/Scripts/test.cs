﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            PoolManager.GetInstance().GetObj("Prefab/Cube");
        }

        if(Input.GetMouseButtonDown(1))
        {
            PoolManager.GetInstance().GetObj("Prefab/Sphere");
        }
    }
}