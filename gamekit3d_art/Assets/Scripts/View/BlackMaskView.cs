using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMaskView : MonoBehaviour
{
    public static BlackMaskView Instance;
    public Image image;

    public void Awake()
    {
        if(Instance != null)
        {
            throw new System.Exception("场景中有多个 BlackMaskView");
        }
        Instance = this;
    }

    public void Start()
    {
        image = transform.GetComponent<Image>();
    }
    public IEnumerator FadeIn()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        yield return null;
        while(image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime);
        }
    }

    public IEnumerator FadeOut()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        yield return null;
        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
