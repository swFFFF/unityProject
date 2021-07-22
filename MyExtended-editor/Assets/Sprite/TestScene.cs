using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : MonoBehaviour
{
    public Color color;
#if UNITY_EDITOR
    private void OnDrawGizmosSelected() //选中物体后绘制
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
        Gizmos.DrawSphere(transform.position, 2);
    }

    private void OnDrawGizmos() //不选中也会绘制
    {
        Gizmos.DrawIcon(transform.position, "Assets/Scenes/logo.png");
    }
#endif
}
