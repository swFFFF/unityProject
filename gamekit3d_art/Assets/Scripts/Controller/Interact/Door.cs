using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DoorStatus
{
    Open,
    Close
}

public class Door : MonoBehaviour
{
    public DoorStatus doorStatus = DoorStatus.Close;

    public int keyNum = 1;  //需要要是的数量
    public int curKeyNum = 0;
    public UnityEvent onOpen;
    public UnityEvent onClose;

    public void Open() 
    {
        curKeyNum++;
        if(doorStatus == DoorStatus.Close && curKeyNum == keyNum)
        {
            doorStatus = DoorStatus.Open;
            onOpen?.Invoke();
        }
    }

    public void Close()
    {
        if (doorStatus == DoorStatus.Open)
        {
            doorStatus = DoorStatus.Close;
            onClose?.Invoke();
        }
    }
}
