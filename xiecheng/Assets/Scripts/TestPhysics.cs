using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPhysics : MonoBehaviour
{
    public GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        Ray ray = new Ray(Vector3.zero, Vector3.forward);
        //Debug.DrawRay(ray.origin, ray.direction, Color.red, 10);

        //RaycastHit raycastHit;
        //bool isCheck = Physics.Raycast(ray, out raycastHit, 10, 1<<0);
        //if(isCheck)
        //{
        //    Debug.Log("射到物体："+raycastHit.transform.name + "射到的点" +raycastHit.point);
        //}

        //RaycastHit[] raycastHits = Physics.RaycastAll(ray);//自动申请扩容内存空间，可能造成浪费
        ////RaycastHit[] raycastHits1 = new RaycastHit[10];//手动申请
        ////int count = Physics.RaycastNonAlloc(ray, raycastHits1);
        //if (raycastHits != null)
        //{
        //    for(int i = 0; i < raycastHits.Length; i++)
        //    {
        //        RaycastHit raycastHit = raycastHits[i];
        //        Debug.Log("射到物体："+ raycastHit.transform.name + "射到的物体："+ raycastHit.point);
        //    }
        //}

        //bool b = Physics.BoxCast(Vector3.zero, Vector3.one, Vector3.forward);
        //bool b = Physics.SphereCast();
        //Debug.Log("b：" + b);

        //Physics.Linecast()线段 Raycast()射线 用法相同

       // go = GameObject.Find("Cube(1)");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool b = Physics.CheckBox(Vector3.zero, Vector3.one);
            if (b)
            {
                Collider[] colliders = Physics.OverlapBox(go.transform.localPosition, Vector3.one);
                //Collider[] colliders = Physics.OverlapBox(Vector3.zero, Vector3.one);
                for (int i = 0; i < colliders.Length; i++)
                {
                    Debug.Log("名字：" + colliders[i].transform.name);
                }
            }
        }
    }
}
