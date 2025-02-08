using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    Text text;
    public static int curStage, offset, score, highestScore;
    int enemiesDisabled, obstacleNumer, random_value;
    float coordX, coordY;
    public GameObject[] obstacles;
    GameObject newObstacle;
    void Start()
    {
        text = GetComponent<Text>();
        coordX = Random.Range(-9f,9f);
        coordY = Random.Range(-4f,4f);

        obstacleNumer = Random.Range(0, 10);
    }
    void FixedUpdate()
    {
        if(!GameManager.gameHasEnded)
            text.text = "Score" + " " + score;

        enemiesDisabled = score;

        if(enemiesDisabled >= offset + 5)
        {
            curStage++;
            SpawnRandomObstacles();
            offset += 5;
        }

        if(GameManager.gameHasEnded)
            {
               Destroy(newObstacle); 
               offset = 0;
            }

        if(GameManager.gameHasEnded)
            if(score > highestScore)
                highestScore = score;
    }

    void SpawnRandomObstacles()
    {
        random_value = Random.Range(0,obstacles.Length);

        coordX = Random.Range(-9f,9f);
        coordY = Random.Range(-4f,4f);

        newObstacle = (GameObject)Instantiate(obstacles[random_value], new Vector2(coordX, coordY), Quaternion.identity);
    }
}
