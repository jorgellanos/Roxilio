using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    public AudioClip Clip;
    public AudioSource Fuente;
    // Start is called before the first frame update
    void Start()
    {
        Fuente.clip = Clip;
    }

    // Update is called once per frame
    void Update()
    {
        Fuente.Play();
    }
}
