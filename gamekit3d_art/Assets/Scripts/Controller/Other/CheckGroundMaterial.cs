using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundMaterial : MonoBehaviour
{
    RaycastHit hit;
    public Material curMaterial;
    void Start()
    {
        
    }

    void Update()
    {
        //起点往上加一个单位
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if(Physics.Raycast(ray, out hit, 2, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer renderer = hit.collider.GetComponentInChildren<Renderer>();
            curMaterial = renderer ? renderer.sharedMaterial : null;
        }
        else
        {
            curMaterial = null;
        }
    }
}
