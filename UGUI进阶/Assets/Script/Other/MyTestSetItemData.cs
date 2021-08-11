using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTestSetItemData : MonoBehaviour, ISetLoopItemData
{
    public void SetData(GameObject childItem, object data)
    {
        LoopDataItem loopDataItem = (LoopDataItem)data;
        childItem.transform.Find("Text").GetComponent<Text>().text = loopDataItem.id.ToString();
    }
}
