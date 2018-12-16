using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MoveToward : MonoBehaviour {

    public float distance;
    public Transform pointer;
    public bool isMoving;
    public float speed;
    public GameObject marker, stop;
    private GameObject go;
    public RaycastHit hit;
    private Ray landingRay;

    // Use this for initialization
    void Start () {
        isMoving = false;
	}

    // Update is called once per frame
    void Update () {

        landingRay = new Ray(pointer.position, pointer.forward);

        if (Physics.Raycast(landingRay, out hit, distance))
        {
            // Marker
            Destroy(go);
            go = Instantiate(marker, hit.point, Quaternion.identity);
            go.transform.position = hit.point;
            PCMethod();
            VRMethod();
        }
        else
        {
            Destroy(go);
            Move(stop.transform, 0);
        }
        
        if (isMoving)
        {
            Move(stop.transform, speed);
        }
    }

    public void Move(Transform target, float MSpeed)
    {
        
        float moving = MSpeed * Time.deltaTime;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3 (target.position.x, 0, target.position.z), moving);
        if (Vector3.Distance(transform.localPosition, target.position) <= 1)
        {
            isMoving = false;
        }
    }

    public void VRMethod()
    {
        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            isMoving = true;
            Destroy(stop);
            stop = Instantiate(stop, hit.point, Quaternion.identity);
            stop.transform.position = go.transform.position;
        }
        
    }

    public void PCMethod()
    {
        if (Input.GetKeyDown("space"))
        {
            isMoving = true;
            Destroy(stop);
            stop = Instantiate(marker, hit.point, Quaternion.identity);
            stop.transform.position = go.transform.position;
        }
    }

}
