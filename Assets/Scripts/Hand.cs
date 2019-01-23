﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour {

    public SteamVR_Action_Boolean grabbing = null;
    public SteamVR_Action_Boolean clicked = null;
    public SteamVR_Action_Vector2 padPos;

    public bool up, down, left, right;
    public Vector2 state;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint joint = null;
    private Interact current = null;
    private List<Interact> contacts = new List<Interact>();
    

    private void Awake()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();

        GetComponent<Hand>().enabled = false;
    }
    
	// Update is called once per frame
	void Update () {

        state = padPos.GetAxis(SteamVR_Input_Sources.Any);

        if (grabbing.GetStateDown(pose.inputSource))
        {
            PickUp();
        }

        if (grabbing.GetStateUp(pose.inputSource))
        {
            Drop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("ObjetoSuelto"))
        {
            return;
        }

        contacts.Add(other.gameObject.GetComponent<Interact>());

        if (other.tag == "Manilla")
        {
            bool holded = false;
            SceneControllerScript door = other.gameObject.GetComponent<SceneControllerScript>();

            if (!door.doorFlag)
            {
                if (grabbing.GetStateDown(pose.inputSource) && !holded)
                {
                    door.doorFlag = true;
                    holded = true;
                }

                if (grabbing.GetStateUp(pose.inputSource))
                {
                    holded = false;
                }
            }
            else
            {
                if (grabbing.GetStateDown(pose.inputSource) && !holded)
                {
                    door.doorFlag = false;
                    holded = true;
                }

                if (grabbing.GetStateUp(pose.inputSource))
                {
                    holded = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Manilla")
        {
            bool holded = false;
            SceneControllerScript door = other.gameObject.GetComponent<SceneControllerScript>();

            if (!door.doorFlag)
            {
                if (grabbing.GetStateDown(pose.inputSource) && !holded)
                {
                    door.doorFlag = true;
                    holded = true;
                }

                if (grabbing.GetStateUp(pose.inputSource))
                {
                    holded = false;
                }
            }
            else
            {
                if (grabbing.GetStateDown(pose.inputSource) && !holded)
                {
                    door.doorFlag = false;
                    holded = true;
                }

                if (grabbing.GetStateUp(pose.inputSource))
                {
                    holded = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("ObjetoSuelto"))
        {
            return;
        }

        contacts.Remove(other.gameObject.GetComponent<Interact>());
    }

    public void PickUp()
    {
        // get nearest
        current = GetNearest();

        // null check
        if (!current)
        {
            return;
        }

        // already held
        if (current.activeHand)
        {
            current.activeHand.Drop();
        }

        // position
        current.transform.position = transform.position;

        // attach
        Rigidbody target = current.GetComponent<Rigidbody>();
        joint.connectedBody = target;

        // set active hand
        current.activeHand = this;
    }

    public void Drop()
    {
        // nullcheck
        if (!current)
        {
            return;
        }

        // apply velocity
        Rigidbody target = current.GetComponent<Rigidbody>();
        target.velocity = pose.GetVelocity();
        target.angularVelocity = pose.GetAngularVelocity();

        // detach
        joint.connectedBody = null;

        // clear
        current.activeHand = null;
        current = null;

    }

    private Interact GetNearest()
    {
        Interact nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interact inter in contacts)
        {
            distance = (inter.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = inter;
            }
        }

        return nearest;
    }

    public void Estados()
    {

        if (clicked.GetStateDown(SteamVR_Input_Sources.Any))
        {
            //RIGHT
            if (state.x > 0.7)
            {
                right = true;
                if (!GetComponent<Hand>().enabled)
                {
                    GetComponent<DashVR>().enabled = false;
                    GetComponent<Hand>().enabled = true;
                }
                else
                {
                    GetComponent<Hand>().enabled = false;
                    GetComponent<DashVR>().enabled = true;
                }
            }
            else
            {
                right = false;
            }

            //LEFT
            if (state.x < -0.7)
            {
                left = true;
                if (!GetComponent<Hand>().enabled)
                {
                    GetComponent<DashVR>().enabled = false;
                    GetComponent<Hand>().enabled = true;
                }
                else
                {
                    GetComponent<Hand>().enabled = false;
                    GetComponent<DashVR>().enabled = true;
                }
            }
            else
            {
                left = false;
            }

            //UP
            if (state.y > 0.7)
            {
                up = true;
            }
            else
            {
                up = false;
            }

            //DOWN
            if (state.y < -0.7)
            {
                down = true;
            }
            else
            {
                down = false;
            }
        }

        if (clicked.GetStateUp(SteamVR_Input_Sources.Any))
        {
            up = false;
            down = false;
            left = false;
            right = false;
        }

    }
}
