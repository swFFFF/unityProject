using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T:new()
{
    private static T instace;
    public static T GetInstance()
    {
        if(instace == null)
        {
            instace = new T();
        }
        return instace;
    }
}
