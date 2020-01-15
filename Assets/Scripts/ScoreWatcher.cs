using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWatcher : MonoBehaviour
{
    public int currScore = 0;
    private TextMesh scoreMesh = null;
    // Start is called before the first frame update
    void Start()
    {
        scoreMesh = gameObject.GetComponent<TextMesh>();
        scoreMesh.text = "0";
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
        currScore += scoreToAdd;
        scoreMesh.text = currScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
