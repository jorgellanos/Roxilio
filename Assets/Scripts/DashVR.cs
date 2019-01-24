using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DashVR : MonoBehaviour {
    
    [SteamVR_DefaultAction("Teleport")]

    public SteamVR_Action_Boolean move = null;

    public float distance, dashDistance;
    public Transform pointer;
    public Transform parent;
    public bool isMoving;
    public float speed;
    public RaycastHit hit;
    public Ray landingRay;
    public GameObject marker, marker2;
    public Animator an;
    private GameObject go;
    private LineRenderer ln;

    // Use this for initialization
    void Start()
    {
        isMoving = false;
        ln = this.GetComponent<LineRenderer>();
        ln.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Move when button up
        ChangeState();
        
        if (isMoving)
        {
            Move(marker2.transform.position);
        }
    }

    public void Move(Vector3 target)
    {
        float moving = speed * Time.deltaTime;
        parent.localPosition = Vector3.MoveTowards(new Vector3(parent.localPosition.x, -2.3f, parent.localPosition.z), target, moving);
        dashDistance = Vector3.Distance(new Vector3(parent.localPosition.x, -2.3f, parent.localPosition.z), target);
    }


    public void SetLaser(bool enabled)
    {
        // enable/disable LASER
        ln.enabled = enabled;

        if (ln.enabled)
        {
            // Ray direction
            landingRay = new Ray(pointer.position, pointer.forward);
            Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        }

        // Show Marker at direction
        if (Physics.Raycast(landingRay, out hit, distance))
        {
            Destroy(go); // destroy clones
            go = Instantiate(marker, hit.point, Quaternion.identity); // set marker 1
            go.transform.position = hit.point;
        }
    }

    public void ChangeState()
    {
        if (move.GetStateDown(SteamVR_Input_Sources.Any))
        {
            SetLaser(true);
            isMoving = false;
        }

        if (move.GetStateUp(SteamVR_Input_Sources.Any))
        {
            SetLaser(false);
            isMoving = true;
            Destroy(marker2);
            marker2 = Instantiate(marker2, hit.point, Quaternion.identity);
            marker2.transform.position = go.transform.position;
        }
    }

}
