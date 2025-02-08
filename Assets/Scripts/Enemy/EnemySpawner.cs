using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static float delay = 1f, timeLeft;
    private float  X, Y;
    private Vector2 spawnArea;
    public GameObject enemy, newEnemy;

    private void Start() 
    {
        X = Random.Range(-11f, 11f);
        Y = Random.Range(-6f, 6f);

        timeLeft += Time.time;
    }
    private void Update() 
    {
        Spawn();
    }
    public void Spawn()
    {
        if (Time.time > timeLeft && !GameManager.gameHasEnded)
        {
            timeLeft += delay;

            X = Random.Range(-11f, 11f);
            Y = Random.Range(-6f, 6f);
            TargetManager.rand_x = Random.Range(-9f, 9f);
            TargetManager.rand_y = Random.Range(-5f, 5f);

            spawnArea = new Vector2(X, Y);

            newEnemy = (GameObject)Instantiate(enemy, spawnArea, Quaternion.identity);

            newEnemy.tag = "newEnemy";

            if(delay > 0.5f)
                delay -= 0.01f;
        }
    }
}
    