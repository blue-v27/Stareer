using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    float random_rotation_value;
    SpriteRenderer sprite_render;
    bool isImploading;
    float random_size;
    void Start()
    {
        random_rotation_value = Random.Range(0f, 360f);
        transform.Rotate(0,0,random_rotation_value);

        sprite_render=GetComponent<SpriteRenderer>();
        
        
        ///start with random color

      //  if(tag == "BrokenShip")
        //    sprite_render.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

        random_size = Random.Range(.75f,1f);

        if(tag != "fan")
            transform.localScale = new Vector2(0.1f,0.1f);

        Invoke("DestoryObstacle", 20);
        Invoke("makeSureItIsDestroyed", 21);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameHasEnded)
            Destroy(gameObject);

        if(tag == "fan")
            transform.Rotate(0,0,1);
                else
                    transform.Rotate(0,0,.1f);

        if(transform.localScale.x < random_size &&  tag != "fan" && !isImploading)
            transform.localScale *= 1.1f;
    }

    void DestoryObstacle()
    {
        isImploading = true;

        transform.localScale *= .1f;

        if(transform.localScale.x <= 0.1f)
            Destroy(gameObject);
    }

    void makeSureItIsDestroyed()
    {
        Destroy(gameObject);
    }
}
