using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float fuseLength = 0.1f; 
    // llargada de la metxaprivate
    float destructTime = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {
        //Quan es crea l'objecte, establir moment de la destrucció
        destructTime = Time.time + fuseLength; 
    }

    // Update is called once per frame
    void Update()
    {
        //A cada frame, mirar si ha arribat el moment de la destrucció.
        if (destructTime<Time.time)
            Destroy(gameObject); 
    }
}
