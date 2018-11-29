using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {

    public float distance;
    public GameObject marker;
    private GameObject go;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        Ray landingRay = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward * distance);

        if (Physics.Raycast(landingRay, out hit, distance))
        {
            // Marker
            Destroy(go);
            go = Instantiate(marker);
            go.transform.position = hit.point;
        }

        if (Input.GetKeyDown("space"))
        {
            teleport();
        }

    }

    public void teleport()
    {
        transform.position = go.transform.position;
    }
}
