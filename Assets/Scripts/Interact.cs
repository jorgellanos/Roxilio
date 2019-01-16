using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class Interact : MonoBehaviour {

    [HideInInspector]
    public Hand activeHand = null;


}
