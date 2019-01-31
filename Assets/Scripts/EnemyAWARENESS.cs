using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAWARENESS : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerPER" || other.tag == "Player")
        {
            Debug.Log("AAAAAAAAA");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "PlayerPER" || other.tag == "Player")
        {
            Debug.Log("AAAAAAAAA");
            StareAt(other.gameObject);
        }
    }

    public void StareAt(GameObject player)
    {
        Vector3 lookVector = player.transform.position - parent.transform.position;
        lookVector.y = parent.transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        parent.transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
        Debug.Log("I SEE YOU");
    }
}
