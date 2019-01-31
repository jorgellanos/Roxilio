using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageAlpha : MonoBehaviour {

    //variables
    public Image imag;
    public GameObject obj;
    public float temps;

    private void Update()
    {
        if (imag)
        {
            SetImageAlpha(imag);
        }

        if (obj)
        {
            SetObjectAlpha(obj);
        }
    }

    public void SetImageAlpha(Image img)
    {
        Color temp = img.color;
        
        temp.a = temps;
        img.color = temp;
    }

    public void SetObjectAlpha(GameObject o)
    {
        Color temp = o.GetComponent<MeshRenderer>().material.color;

        temp.a = temps;
        o.GetComponent<MeshRenderer>().material.color = temp;

    }
}
