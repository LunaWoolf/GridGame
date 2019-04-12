using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    float timer = 0.5f;

    void Start()
    {
        
    }


    void Update()
    {


        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
