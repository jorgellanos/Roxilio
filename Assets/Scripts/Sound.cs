using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public float moveVolume, impactVolume, awareness;
    public float soundRadio;

    // Start is called before the first frame update
    void Start()
    {
        soundRadio = 0.1f;
        moveVolume = 0.1f;
        impactVolume = 0.1f;
    }

    private void Update()
    {
        awareness = moveVolume + impactVolume;
    }

    public void Still()
    {
        if (moveVolume > 0f)
        {
            moveVolume -= 10f * Time.deltaTime;
        }
    }

    public void Moving()
    {
        if (moveVolume < 100f)
        {
            moveVolume += 5f * Time.deltaTime;
        }
    }

    public void Impact()
    {
        impactVolume += 20;
        if (impactVolume > 0f)
        {
            impactVolume -= 10f * Time.deltaTime;
        }
    }
}
