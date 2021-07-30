using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScrollView : ScalePageScrollView
{
    public float rotation;
    protected override void Update()
    {
        base.Update();
        ListenerItemRotation();
    }

    public void ListenerItemRotation()
    {
        if(nextPage == lastPage)
        {
            return;
        }

        //比例
        float percent = (rect.horizontalNormalizedPosition - pages[lastPage]) / (pages[nextPage] - pages[lastPage]);

        if(nextPage > curPage)
        {
            items[lastPage].transform.localRotation = Quaternion.Euler(-Vector3.Lerp(Vector3.zero, new Vector3(0, rotation, 0), percent));
            items[nextPage].transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, rotation, 0), 1 - percent));
        }
        else 
        {
            items[lastPage].transform.localRotation = Quaternion.Euler(-Vector3.Lerp(Vector3.zero, new Vector3(0, rotation, 0), percent));
            items[nextPage].transform.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(0, rotation, 0), 1 - percent));
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (i != lastPage && i != nextPage)
            {
                if (i < curPage)
                {
                    items[i].transform.localRotation = Quaternion.Euler(new Vector3(0, -rotation, 0));
                }
                else if (i == curPage)
                {
                    //items[i].transform.localRotation = Quaternion.Euler(Vector3.zero);
                }
                else if (i > curPage)
                {
                    items[i].transform.localRotation = Quaternion.Euler(new Vector3(0, rotation, 0));
                }
            }
        }
    }
}
