using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateProject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string createProject = @"Click on XFABManager/Projects to open the project list interface,
                                Click + add project, fill in the project name to create a project!
                                Click Create, select the project configuration save path, to complete the creation!";

        Debug.Log(createProject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
