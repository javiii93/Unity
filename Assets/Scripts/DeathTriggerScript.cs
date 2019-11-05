using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collidedObject)
    {
        collidedObject.SendMessage("hitDeathTrigger");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
