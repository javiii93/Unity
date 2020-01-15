using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageFromPlayerBullet : MonoBehaviour
{
    public delegate void hitByPlayerBullet();
    public event hitByPlayerBullet hitByBullet;
    //Tractament de col.lisio amb DefenseCollider: si//es amb una bala, generar event.  
    void OnTriggerEnter2D(Collider2D collidedObject)
    { if (collidedObject.tag == "PlayerBullet"|| collidedObject.tag == "PlayerBullet2" ) {
            Debug.Log("TakeDamageFromPlayer, DefenseCollider colisiona amb: "+collidedObject.tag);
            if (hitByBullet != null)
            {
                Debug.Log("TakeDamageFromPlayer,enemic tocat ");
                hitByBullet();
            }
        }
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
