using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyManager : MonoBehaviour
{
    ////Spawn Randomizin
    public Sprite[] random_Sprite;
    SpriteRenderer sprite_rederer;
    int random_sprite;
    float random_size;
    TrailRenderer trial_randerer;

    public Rigidbody2D rigid_body;
    public TMP_Text popup_text;
    float random_money_value;
    public GameObject death_effect;
    
    void Start()
    {
        sprite_rederer = GetComponent<SpriteRenderer>();
        trial_randerer = GetComponent<TrailRenderer>();
        rigid_body = GetComponent<Rigidbody2D>();

        random_sprite = Random.Range(0, random_Sprite.Length);
        random_size = Random.Range(.75f, 1.5f);
        
        if(tag == "newEnemy")
        {
            sprite_rederer.sprite = random_Sprite[random_sprite];
            transform.localScale = new Vector2(0.1f, 0.1f);
            trial_randerer.startWidth = .1f;
        }

        transform.localScale = new Vector2(0.1f, 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, TargetManager.target_position, Time.deltaTime);

        if(tag == "newEnemy")
        {
            if(GameManager.gameHasEnded)
                DisableEnemy();
            
            if(transform.localScale.x < random_size)
                transform.localScale *= 1.5f;
            
            if(trial_randerer.startWidth < random_size)
                trial_randerer.startWidth *= 1.5f;
        }

        
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
            DisableEnemy();
    }

    void DisableEnemy()
    {
        if(tag == "newEnemy")
            {
                Destroy(gameObject);
                
                if(!GameManager.gameHasEnded)
                    ScoreScript.score++;
                
                Instantiate(death_effect, transform.position, transform.rotation);
                
                Instantiate(popup_text, transform.position, transform.rotation);
                random_money_value = Random.Range(.5f, 1.5f);
                random_money_value = Mathf.Round(random_money_value * 100.0f) * 0.01f;
                popup_text.text = "+ " + random_money_value;
                
                GameManager.Money += random_money_value;
                
                SoundManagerScript.PlaySound("asteriod_dead_sfx");

                GameManager.shake_camera = true;

                if(GameManager.tutorial_phase == 3)
                    GameManager.tutorial_phase++;

            }
    }
}
