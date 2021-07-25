using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    Renderer[] renderers;

    public float dissolveTime = 3.0f;
    private float dissolveTimer = 0;

    MaterialPropertyBlock propertyBlock;    //材质内存块
    private void Start()
    {
        renderers = transform.GetComponentsInChildren<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        dissolveTimer += Time.deltaTime;
        if(dissolveTimer >= dissolveTime)
        {
            return;
        }
        for(int i = 0; i < renderers.Length; i++)
        {
            //renderers[i].material.SetFloat("_Cutoff", dissolveTimer / dissolveTime);    //名字要设置成shader文件中的真实名。直接修改材质参数。需要注意其他应用场合。
            renderers[i].GetPropertyBlock(propertyBlock);
            propertyBlock.SetFloat("_Cutoff", dissolveTimer / dissolveTime);
            renderers[i].SetPropertyBlock(propertyBlock);
        }
    }

    private void OnEnable()
    {
        dissolveTimer = 0;
    }
}
