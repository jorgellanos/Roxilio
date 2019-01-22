using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class MovementTouchpad : MonoBehaviour
{
    [SteamVR_DefaultAction("MoveTouchpad")]
    public float speed;
    Vector2 movimiento;
    public Rigidbody rb;
    public GameObject ojos;
    bool keyUp;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (movimiento.x > 0 || movimiento.y > 0)
        {
            keyUp = true;
        }
        movimiento = SteamVR_Input._default.inActions.MoveTouchpad.GetAxis(SteamVR_Input_Sources.Any);
        Vector3 movement;
        if (keyUp)
        {
            movement = new Vector3(movimiento.x, 0.0f, movimiento.y);
            rb.MovePosition(movement * speed);
        }
        {

        }

            //Debug.Log(movement);
        
        /*if (transform.parent.position.y < 0)
        {
            movimiento = SteamVR_Input._default.inActions.MoveTouchpad.GetAxis(SteamVR_Input_Sources.Any);
            movement = new Vector3(-movimiento.x, 0.0f, movimiento.y);
            rb.AddForce(movement * speed);
            //Debug.Log(movement);
        }
        if (transform.parent.position.y > 0)
        {
            movimiento = SteamVR_Input._default.inActions.MoveTouchpad.GetAxis(SteamVR_Input_Sources.Any);
            movement = new Vector3(movimiento.x, 0.0f, -movimiento.y);
            rb.AddForce(movement * speed);
            //Debug.Log(movement);
        }*/
        keyUp = false;

    }
}
