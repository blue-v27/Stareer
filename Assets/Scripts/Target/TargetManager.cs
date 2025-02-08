using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetManager : MonoBehaviour
{
    public TextMeshPro health;
    public static Vector2 target_position;
    public static int target_health = 100, max_target_health = 100;
    public static float rand_x, rand_y;
    Vector2 random_position;
    public GameObject deathEffect;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        health.text = "Health " + target_health;

        target_position = transform.position;

        random_position = new Vector2(rand_x, rand_y);
        transform.position = Vector2.Lerp(transform.position, random_position, Time.deltaTime);

        if(target_health <= 0)
        {
            GameManager.gameHasEnded = true;
          //  Instantiate(deathEffect, transform.position, Quaternion.identity);
            transform.localScale = new Vector2(0,0);
        }
        else
          transform.localScale = new Vector2(1,1);
    }

    


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag =="newEnemy")
            {
                target_health -= 10;
                rand_x = Random.Range(-7f, 7f);    
                rand_y = Random.Range(-5f, 5f); 

                GameManager.shake_camera = true;
                
                SoundManagerScript.PlaySound("eroee");
            }
    }
}
