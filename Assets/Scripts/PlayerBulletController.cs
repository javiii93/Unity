﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    //Se li assignara un objecte automaticament, en crear una bala a PlayerStateListener   
    public GameObject playerObject = null;
    public float bulletSpeed = 15.0f;
    private Rigidbody2D rb2D;
    private float selfDestructTimer = 0.0f;
    public void launchBullet()
    { // Volem que el Player dispari cap al costat al que mira.// Aixo ens ho indica el component "local scale"
        float mainXScale = playerObject.transform.localScale.x;
        Vector2 bulletForce;
        if (mainXScale < 0.0f)
        {// Disparar cap a l'esquerra
            bulletForce = new Vector2(bulletSpeed * -1.0f, 0.0f);
        }
        else
        {// Disparar cap a la dreta
            bulletForce = new Vector2(bulletSpeed, 0.0f);
        }
      rb2D = GetComponent<Rigidbody2D>();
       rb2D.velocity = bulletForce;
        selfDestructTimer = Time.time + 1.0f;

    }
    void Update()
    {//Destruir l'objecte si ha consumit el temps
        if (selfDestructTimer> 0.0f) {
            if (selfDestructTimer<Time.time)
                Destroy(gameObject);
        }
    }
    }


