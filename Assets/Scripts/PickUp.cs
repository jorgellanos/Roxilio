using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PickUp : MonoBehaviour {
    [SteamVR_DefaultAction("Squeeze")]
    public bool grabbed;
    public float dist;

    // Use this for initialization
    void Start()
    {
        grabbed = false;
    }

    private void Update()
    {
        if (SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.Any) > 1)
        {
            Debug.Log(SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.Any));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hand")
        {
            Debug.Log("Control touched the tarro");
            if (SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.Any) < 0)
            {
                Debug.Log("pressing that shit");
                transform.SetParent(other.transform.parent);
                transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                Debug.Log("NotPressing");
                transform.parent = null;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "hand")
        {
            Debug.Log("Control touched the tarro");
            if (SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.Any) < 0)
            {
                Debug.Log("pressing that shit");
                transform.SetParent(other.transform.parent);
                transform.position = new Vector3(0, 0, 0);
            }
            else
            {
                Debug.Log("NotPressing");
                transform.parent = null;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Control stopped touching the tarro");
    }

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
}
