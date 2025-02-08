using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Joystick joy_stick;
    private TrailRenderer trail_renderer;
    private Vector3 HalfScale, offset, screenPoint;
    private float curMouseInputY, preMouseInputY, curMouseInputX, preMouseInputX; 
    public static float player_newSize;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trail_renderer = GetComponent<TrailRenderer>();
        player_newSize = 1f;
    }
    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(joy_stick.Horizontal * 10, joy_stick.Vertical * 10);

        if(GameManager.PlayerSizeLevel == 1)
            {
                player_newSize = .88f;
                trail_renderer.startWidth = .88f;
            } 
                else if(GameManager.PlayerSizeLevel == 2)
                    {
                        player_newSize = .66f;
                        trail_renderer.startWidth = .66f;
                    } 
                        else if(GameManager.PlayerSizeLevel == 3)
                            {
                                player_newSize = .5f;
                                trail_renderer.startWidth = .5f;
                            } 
        
        transform.localScale = new Vector2(player_newSize, player_newSize);

        ///Skins
    }
}
