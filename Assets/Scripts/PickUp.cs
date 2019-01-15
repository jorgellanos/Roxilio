﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUp : MonoBehaviour {
    [SteamVR_DefaultAction("Squeeze")]
    public bool grabbed,squeezed, touching;
    public float dist;
    public Vector3 theHand, velocity;
    SteamVR_TrackedObject trackedObject;

    // Use this for initialization
    void Start()
    {
        grabbed = false;
        squeezed = false;
        touching = false;
    }

    private void Update()
    {
        velocity = this.GetComponent<Rigidbody>().velocity;
        if (SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.Any) > 0.3f)
        {
            squeezed = true;
        }
        else
        {
            squeezed = false;
        }

        if (squeezed && touching)
        {
            transform.position = theHand;
        }
        {
            touching = false;
        }

    }

    #region VR-Method
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand")
        {
            theHand = other.transform.position;
            touching = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "hand")
        {
            theHand = other.transform.position;
            touching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        touching = false;
        if (other.tag == "hand")
        {
            
            theHand = new Vector3(0, 0, 0);
            Debug.Log("Control stopped touching the tarro");
        }
    }
    #endregion

    // PC
    #region PC-Method
    private void OnMouseEnter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("grabed " + gameObject.name);
            grabbed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            grabbed = false;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("grabed " + gameObject.name);
            grabbed = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            grabbed = false;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
        Vector3 objeto = Camera.main.ScreenToWorldPoint(mouse);
        if (grabbed)
        {
            transform.position = objeto;
            Scroll();
        }
    }

    public void Scroll()
    {
        float distChange = 1f;
        if (Input.mouseScrollDelta.y > 0)
        {
            dist -= distChange * Time.deltaTime * 10;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            dist += distChange * Time.deltaTime * 10;
        }
    }
    #endregion
}
