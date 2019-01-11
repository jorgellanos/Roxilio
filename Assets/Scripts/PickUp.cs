using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

    public bool grabbed;
    public float dist;

    // Use this for initialization
    void Start()
    {
        grabbed = false;
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
