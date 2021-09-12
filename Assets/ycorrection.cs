using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ycorrection : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(this.transform.localRotation.eulerAngles.y != 0f)
        {
            Quaternion rot = Quaternion.Euler(0f, 0f, 0f);
            this.transform.localRotation = rot;
        }    
    }
}
