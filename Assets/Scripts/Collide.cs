using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    public DashVR sc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "marker2")
        {
            sc.isMoving = false; 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "marker2")
        {
            sc.isMoving = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

}
