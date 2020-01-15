using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerScript : MonoBehaviour
{
    public float walkingSpeed = 0.45f;
    private bool walkingLeft = true;
    public TakeDamageFromPlayerBullet bulletColliderListener = null;
    public ParticleSystem deathFxParticlePrefab = null;
    public delegate void enemyEventHandler(int scoreMod);
    public static event enemyEventHandler enemyDied;

    // Start is called before the first frame update
    void Start()
    {
        walkingLeft = (Random.Range(0, 2) == 1);
        updateVisualWalkOrientation();
    }
    public void hitByPlayerBullet()
    {// Esperar un moment i destruir l'objecte Enemy
       
        ParticleSystem deathFxParticle = (ParticleSystem)Instantiate(deathFxParticlePrefab);
        // Obtenir la posició de l'enemic 
        Vector3 enemyPos = transform.position;
        // Crear un nou vector davant de l'enemic (incrementar component z)
        Vector3 particlePosition = new Vector3(enemyPos.x,enemyPos.y,enemyPos.z + 1.0f);
        // Posicionar l'emissor de partícules en aquesta nova posició
        deathFxParticle.transform.position = particlePosition;
        // Generar event enemyDied i donar una puntuacio de 25 punts.
        if (enemyDied != null)
            enemyDied(25);
        // Esperar un moment i destruir l'objecte Enemy
        Destroy(gameObject,0.1f); 
    }
    void OnEnable()
    { // Suscripció a l'event hitByBullet. 
        bulletColliderListener.hitByBullet += hitByPlayerBullet;
    }
    void OnDisable()
    { // cancel.lar la suscripció a l'event hitByBullet. 
        bulletColliderListener.hitByBullet -= hitByPlayerBullet;
    }

      // Update is called once per frame
        void Update()
    {
        if (walkingLeft)
        {
            transform.Translate(new Vector3(walkingSpeed * Time.deltaTime, 0.0f, 0.0f));
        }
        else
        {
            transform.Translate(new Vector3((walkingSpeed * -1.0f) * Time.deltaTime, 0.0f, 0.0f));
        }
    }
    public void switchDirections()
    {
        walkingLeft = !walkingLeft;
        updateVisualWalkOrientation();
    }
    void updateVisualWalkOrientation()
    {
        Vector3 localScale = transform.localScale;
        if (walkingLeft)
        {
            if (localScale.x > 0.0f)
            {
                localScale.x = localScale.x * -1.0f;
                transform.localScale = localScale;
            }
        }
        else
        {
            if (localScale.x < 0.0f)
            {
                localScale.x = localScale.x * -1.0f;
                transform.localScale = localScale;
            }
        }
    }
}
