using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDrag : MonoBehaviour
{
    private GameObject curGameObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            bool b = Physics.Raycast(ray, out raycastHit, 1000, 1<<LayerMask.NameToLayer("gameObject"));
            if(b)
            {
                Debug.Log("名称"+ raycastHit.transform.name);
                curGameObject = raycastHit.transform.gameObject;
            }
        }

        if(Input.GetMouseButton(0) && curGameObject != null)
        {
            //拖动游戏物体
            Debug.Log("拖动！");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            bool b = Physics.Raycast(ray, out raycastHit, 1000, 1<<LayerMask.NameToLayer("ground"));
            if(b)
            {
                curGameObject.transform.position = raycastHit.point + new Vector3(0,curGameObject.transform.localScale.y / 2,0);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            curGameObject = null;
        }
    }
}
