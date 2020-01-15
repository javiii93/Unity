using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuideWatcher : MonoBehaviour
{
    public EnemyControllerScript enemyObject = null;
   // Start is called before the first frame update
   void Start()
    {
        
    }
    void OnTriggerExit2D(Collider2D otherObj)
    { // Aquest event es produeix quan Enemy està a punt
      //de sortir de la Platform.
      //Canviem la direcció del desplaçament
        if (otherObj.tag == "Platform")
            enemyObject.switchDirections();    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
