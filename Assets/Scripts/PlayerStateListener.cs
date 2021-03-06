using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animator))]
public class PlayerStateListener : MonoBehaviour
{
    public float playerWalkSpeed = 3f, playerJumpStrongY = 500f, playerJumpStrongX = 1000f;
    public GameObject playerRespawnPoint = null;
    public Transform bulletSpawnTransform;
    private Animator playerAnimator = null;
    private bool right = true;
   // private int maximoDeSaltos=2 , saltosActuales=0 ;
    public bool playerHasLanded = true;
    private Rigidbody2D rb2D;
    public GameObject bulletPrefab = null;
    public GameObject bulletPrefab2 = null;
    private PlayerStateController.playerStates previousState =
PlayerStateController.playerStates.idle;
   
    private PlayerStateController.playerStates currentState =
    PlayerStateController.playerStates.idle;
    
    void OnEnable()
    {
        PlayerStateController.onStateChange += onStateChange;
    }

    void OnDisable()
    {
        PlayerStateController.onStateChange -= onStateChange;
    }
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        PlayerStateController.stateDelayTimer[(int)PlayerStateController.playerStates.firingWeapon] = 0.0f;
        PlayerStateController.stateDelayTimer[(int)PlayerStateController.playerStates.firingWeapon2] = 0.0f;


    }
    void LateUpdate()
    {
        onStateCycle();
    }
    public void hitDeathTrigger()
    {
        onStateChange(PlayerStateController.playerStates.kill);
    }
    // Processar l'estat en cada cicle
    void onStateCycle()
    {

        Vector3 localScale = transform.localScale;
        transform.localEulerAngles = Vector3.zero;
        switch (currentState)
        {
            case PlayerStateController.playerStates.idle:
                break;
            case PlayerStateController.playerStates.left:

                transform.Translate(new Vector3((playerWalkSpeed * -1.0f) *

                Time.deltaTime, 0.0f, 0.0f));
                right = false;

                if (localScale.x > 0.0f)
                {
                    localScale.x *= -1.0f;
                    transform.localScale = localScale;

                }
                /*Debug.Log(left);
                Debug.Log(right);*/

                break;
            case PlayerStateController.playerStates.right:
                transform.Translate(new Vector3(playerWalkSpeed * Time.deltaTime,
                0.0f, 0.0f));
                if (localScale.x < 0.0f)
                {
                    localScale.x *= -1.0f;
                    transform.localScale = localScale;


                }
                right = true;

                /*   Debug.Log(left);
                   Debug.Log(right);*/
                break;
            case PlayerStateController.playerStates.jump:
                break;
            case PlayerStateController.playerStates.landing:
               
                break;
            case PlayerStateController.playerStates.falling:
                break;
            case PlayerStateController.playerStates.kill:
                onStateChange(PlayerStateController.playerStates.resurrect);

                break;
            case PlayerStateController.playerStates.resurrect:
               /* transform.position = playerRespawnPoint.transform.position;
                transform.rotation = Quaternion.identity;
                rb2D.velocity = Vector2.zero;*/
                onStateChange(PlayerStateController.playerStates.idle);

                break;
        }
    }
    public void onStateChange(PlayerStateController.playerStates newState)
    {
        if (newState == currentState)
            return;
        // Comprovar que no hi hagi condicions per abortar l'estat
        if (checkIfAbortOnStateCondition(newState))
            return;

        if (!checkForValidStatePair(newState))
            return;
        Debug.Log(newState+", current: "+ currentState);
        rb2D = GetComponent<Rigidbody2D>();

        switch (newState)
        {

            case PlayerStateController.playerStates.idle:
                playerAnimator.SetBool("Walking", false);
                break;
            case PlayerStateController.playerStates.left:
                playerAnimator.SetBool("Walking", true);
                break;
            case PlayerStateController.playerStates.right:
                playerAnimator.SetBool("Walking", true);
                break;
            case PlayerStateController.playerStates.jump:

                if (playerHasLanded)
                {
                    if (right)
                    {
                        if (playerJumpStrongY < 0)
                        {
                            playerJumpStrongY = playerJumpStrongY * -1f;
                        }
                        rb2D.AddForce(new Vector2(playerJumpStrongY, playerJumpStrongX));
                    }
                    else 
                    {
                        if (playerJumpStrongY < 0)
                        {
                            rb2D.AddForce(new Vector2(playerJumpStrongY, playerJumpStrongX));

                        }
                        else
                        {
                            playerJumpStrongY = playerJumpStrongY * -1f;
                            rb2D.AddForce(new Vector2(playerJumpStrongY, playerJumpStrongX));
                        }
                    }
                }
                
                playerHasLanded = false;
                Debug.Log("landing = " + playerHasLanded);
                break;
            case PlayerStateController.playerStates.landing:
               // Debug.Log("Landed " + playerHasLanded);
                playerHasLanded = true;
              //  Debug.Log("Landed " + playerHasLanded);
                break;
            case PlayerStateController.playerStates.falling:
                break;
            case PlayerStateController.playerStates.kill:
                break;
            case PlayerStateController.playerStates.resurrect:

                transform.position = playerRespawnPoint.transform.position;
                transform.rotation = Quaternion.identity;
                rb2D.velocity = Vector2.zero;

                break;
            case PlayerStateController.playerStates.firingWeapon:
                PlayerStateController.stateDelayTimer[(int)PlayerStateController.playerStates.firingWeapon] = Time.time + 0.25f;
                GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
                newBullet.transform.position = bulletSpawnTransform.position;
                PlayerBulletController bullCon = newBullet.GetComponent<PlayerBulletController>();
                bullCon.playerObject = gameObject;
                bullCon.launchBullet();
                onStateChange(currentState);
                break;
              
            case PlayerStateController.playerStates.firingWeapon2:
                PlayerStateController.stateDelayTimer[(int)PlayerStateController.playerStates.firingWeapon2] = Time.time + 0.25f;
                GameObject newBullet2 = (GameObject)Instantiate(bulletPrefab2);
                newBullet2.transform.position = bulletSpawnTransform.position;
                PlayerBulletController2 bullCon2 = newBullet2.GetComponent<PlayerBulletController2>();
                bullCon2.playerObject = gameObject;
                bullCon2.launchBullet();
                onStateChange(currentState);
                break;
        }
        previousState = currentState;
        currentState = newState;
    }
    // Comprovar si es pot passar al nou estat des de l'actual.
    // Es tracten diversos estats que encara no estan implementats, perqu� el
    // codi sigui m�s ilustratiu
    bool checkForValidStatePair(PlayerStateController.playerStates newState)
    {
        rb2D = GetComponent<Rigidbody2D>();
        bool returnVal = false;
        // Comparar estat actual amb el candidat a nou estat.
        switch (currentState)
        {
            case PlayerStateController.playerStates.firingWeapon:
                returnVal = true;
                break;
            case PlayerStateController.playerStates.firingWeapon2:
                returnVal = true;
                break;
            case PlayerStateController.playerStates.idle:
                // Des de idle es pot passar a qualsevol altre estat
                returnVal = true;
                break;
            case PlayerStateController.playerStates.left:
                // Des de moving left es pot passar a qualsevol altre estat
                returnVal = true;
                break;
            case PlayerStateController.playerStates.right:
                // Des de moving right es pot passar a qualsevol altre estat
                returnVal = true;
                break;
            case PlayerStateController.playerStates.jump:
                // Des de Jump nom�s es pot passar a landing o a kill.
                if (
                newState == PlayerStateController.playerStates.landing
                || newState == PlayerStateController.playerStates.kill || newState == PlayerStateController.playerStates.firingWeapon || newState == PlayerStateController.playerStates.firingWeapon2
                )
                    returnVal = true;
                else
                    returnVal = false;
           if(newState == PlayerStateController.playerStates.landing||    newState == PlayerStateController.playerStates.kill || newState == PlayerStateController.playerStates.firingWeapon || newState == PlayerStateController.playerStates.firingWeapon2)
                    returnVal = true;
                else returnVal = false;
                break;
            case PlayerStateController.playerStates.landing:
                // Des de landing nom�s es pot passar a idle, left o right.
                if (
                newState == PlayerStateController.playerStates.left
                || newState == PlayerStateController.playerStates.right
                || newState == PlayerStateController.playerStates.idle || newState == PlayerStateController.playerStates.firingWeapon || newState == PlayerStateController.playerStates.firingWeapon2
                )
                    returnVal = true;
                else
                    returnVal = false;
                break;
            case PlayerStateController.playerStates.falling:
                // Des de falling nom�s es pot passar a landing o a kill.
                if (
                newState == PlayerStateController.playerStates.landing
                || newState == PlayerStateController.playerStates.kill || newState == PlayerStateController.playerStates.firingWeapon || newState == PlayerStateController.playerStates.firingWeapon2
                )
                    returnVal = true;
                else
                    returnVal = false;
                break;
            case PlayerStateController.playerStates.kill:
                // Des de kill nom�s es pot passar resurrect
                if (newState == PlayerStateController.playerStates.resurrect)
                    returnVal = true;
                else
                    returnVal = false;
                break;
            case PlayerStateController.playerStates.resurrect:
                // Des de resurrect nom�s es pot passar Idle
                if (newState == PlayerStateController.playerStates.idle)
                    returnVal = true;
                else
                    returnVal = false;
                break;
        }

        return returnVal;
    }
    // Aquesta funci� comprova si hi ha algun motiu que impedeixi passar al nou estat.
    // De moment no hi ha cap motiu per cancel�lar cap estat.
    bool checkIfAbortOnStateCondition(PlayerStateController.playerStates newState)
    {
        bool returnVal = false;
        switch (newState)
        {
            case PlayerStateController.playerStates.idle:
                break;
            case PlayerStateController.playerStates.left:
                break;
            case PlayerStateController.playerStates.right:
                break;
            case PlayerStateController.playerStates.jump:
                break;
            case PlayerStateController.playerStates.landing:
                break;
            case PlayerStateController.playerStates.falling:
                break;
            case PlayerStateController.playerStates.kill:
                break;
            case PlayerStateController.playerStates.resurrect:
               
                break;
            case PlayerStateController.playerStates.firingWeapon:
                if(PlayerStateController.stateDelayTimer[(int)PlayerStateController.playerStates.firingWeapon] > Time.time) { returnVal = true; }
                break;
            case PlayerStateController.playerStates.firingWeapon2:
                if (PlayerStateController.stateDelayTimer[(int)PlayerStateController.playerStates.firingWeapon2] > Time.time) { returnVal = true; }
                break;
        }
        // Retornar True vol dir 'Abort'. Retornar False vol dir 'Continue'.
        return returnVal;
    }
   
}