using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreWatcher2 : MonoBehaviour
{
    public int currScore2 = 0;
    private Text scoreMesh2 = null;
    // Start is called before the first frame update
    void Start()
    {
        scoreMesh2 = gameObject.GetComponent<Text>();
        scoreMesh2.text = "0";
    }
    void OnEnable()
    {
        EnemyControllerScript.enemyDied += addScore;
    }
    void OnDisable()
    {
        EnemyControllerScript.enemyDied -= addScore;
    }
    void addScore(int scoreToAdd)
    {
        currScore2 += scoreToAdd;
        scoreMesh2.text = currScore2.ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
