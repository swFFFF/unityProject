using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPView : ViewBase
{
    public GameObject HPItemPrefab;
    public Damageable damageable;
    private Toggle[] hps;

    private IEnumerator Start()
    {
        hps = new Toggle[damageable.maxHp];
        yield return null;
        for (int i = 0; i < damageable.maxHp; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject hpItem = GameObject.Instantiate(HPItemPrefab, transform.Find("HPs"));//实例化到对应路径下
            hps[i] = hpItem.GetComponent<Toggle>();
        }
    }
      
    public void UpdateHPView()
    {
        for(int i =0; i < hps.Length; i++)
        {
            if (hps[i].isOn && i >= damageable.CurrentHp)
            {
                hps[i].transform.Find("Background/Dissolve").gameObject.SetActive(false);
                hps[i].transform.Find("Background/Dissolve").gameObject.SetActive(true);
            }
            hps[i].isOn = i < damageable.CurrentHp;

        }
    }
}
