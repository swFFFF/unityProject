using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMeassage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SendMessage("Test1","hello",SendMessageOptions.DontRequireReceiver);//发送给当前游戏物体上有所有对应方法的脚本

        SendMessageUpwards("Test1");//对自己和父物体发送消息

        BroadcastMessage("Test1");//向自己和子物体发送消息
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test1(string str)
    {
        Debug.Log("Test1 called"+ str);
    }
}
