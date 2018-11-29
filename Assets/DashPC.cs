using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class DashPC : MonoBehaviour {

    [SteamVR_DefaultAction("Teleport")]
    public float distance;
    public Transform pointer;
    public Transform parent;
    public bool isMoving;
    public float speed, trigger;
    public RaycastHit hit;
    public Ray landingRay;
    public GameObject marker;
    public Animator an;
    public GameObject marker2;
    private GameObject go, stop;
    private LineRenderer ln;
    
    // Use this for initialization
    void Start()
    {
        isMoving = false;
        ln = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        landingRay = new Ray(pointer.position, pointer.forward);
        Debug.DrawRay(transform.position, transform.forward * distance, Color.red);

        VRMethod();

        if (Physics.Raycast(landingRay, out hit, distance))
        {
            // Marker
            Destroy(go);
            go = Instantiate(marker, hit.point, Quaternion.identity);
            go.transform.position = hit.point;
        }

        if (Input.GetKeyDown("space"))
        {
            Destroy(stop);
            stop = Instantiate(marker, hit.point, Quaternion.identity);
            isMoving = true;
        }

        if (isMoving)
        {
            Move(stop.transform.position);
        }

        if (Input.GetKeyDown("space"))
        {
            isMoving = true;
        }
        

        if (isMoving)
        {
            Move(marker2.transform.position);
            //an.SetBool("walking", true);
            //an.Play("Camera_Walk");
        }
        else
        {
            //an.SetBool("walking",false);
        }
    }

    public void Move(Vector3 target)
    {
        float moving = speed * Time.deltaTime;
        parent.localPosition = Vector3.MoveTowards(parent.localPosition, target, moving);
        if (Vector3.Distance(parent.localPosition, target) <= 1)
        {
            isMoving = false;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "marker2")
        {
            Destroy(other.gameObject);
            isMoving = false;
        }
    }

    public void VRMethod()
    {
        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            isMoving = true;
            Destroy(stop);
            stop = Instantiate(marker2, hit.point, Quaternion.identity);
            stop.transform.position = go.transform.position;
        }
    }
}
