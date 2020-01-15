using UnityEngine;
using System.Collections;
public class PlayerStateController : MonoBehaviour
{
    //Definicio dels diferents estats del player
    public enum playerStates
    {
        idle = 0,
        left,
        right,
        jump,
        landing,
        falling,
        kill,
        resurrect,
        firingWeapon,
        firingWeapon2,
        _stateCount
    }
    public static float[] stateDelayTimer = new float[(int)playerStates._stateCount];
    //Definicio del delegate playerStateHandler
    public delegate void playerStateHandler(PlayerStateController.playerStates newState);
    //public delegate void playerBulletHandler(PlayerStateController.playerStates newState);

    //Definicio d'event onStateChange i assignacio de onStateChange com a EventHandler
    public static event playerStateHandler onStateChange;
    // Aquest metode es crida despres de Update() a cada frame.
    void LateUpdate()
    {
        // Recollir l'input actual en el Horizontal axis (eix horitzontal)
        float horizontal = Input.GetAxis("Horizontal");
        float salto = Input.GetAxis("Jump");
        //Tractar segons el valor de l'input recollit
        if (horizontal != 0f)
        {
            //Hi ha algun moviment: canviar l'estat del protagonista a left o right
            if (horizontal < 0f)
            {
                if (onStateChange != null) onStateChange(PlayerStateController.
                 playerStates.left);

            }
            else
            {
                if (onStateChange != null) onStateChange(PlayerStateController.
                 playerStates.right);
            }
        }
        else
        //No hi ha cap moviment: canviar l'estat del protagonista a idle
        {
            if (onStateChange != null) onStateChange(PlayerStateController.
             playerStates.idle);
        }

        if (salto != 0f)
        //Debug.Log("Jump " + salto);
        {
            if (onStateChange != null) onStateChange(PlayerStateController.
             playerStates.jump);
            salto = 0;
        }

        //Disparar
        float firing = Input.GetAxis("Fire1");
        if (firing > 0.0f) {
            if (onStateChange != null)
                onStateChange(PlayerStateController.playerStates.firingWeapon);
        }
        //Disparar2
        float firing2 = Input.GetAxis("Fire2");
        if (firing2 > 0.0f)
        {
            if (onStateChange != null)
                onStateChange(PlayerStateController.playerStates.firingWeapon2);
        }


    }

}