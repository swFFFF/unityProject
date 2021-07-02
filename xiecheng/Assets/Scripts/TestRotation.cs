using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.rotation);
        Debug.Log(transform.eulerAngles);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.left, 1);
        //transform.RotateAround(target.transform.position, Vector3.right, 1);
        transform.LookAt(target.transform.localPosition);
    }
}
