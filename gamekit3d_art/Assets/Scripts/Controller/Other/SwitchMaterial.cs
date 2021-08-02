using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
    public Material[] target;
    private Renderer renderer;

    private void Awake()
    {
        renderer = transform.GetComponent<Renderer>();
        if(renderer == null)
        {
            throw new System.Exception("未找到renderder组件");
        }
    }

    public void Switch()
    {
        renderer.materials = target;
    }
}
