using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip enemyBreakSound_1, enemyBreakSound_2, erorrSound, enable_disable_sfx, asteriod_dead_sfx;
    static AudioSource audioSrc;
    void Start()
    {
        enemyBreakSound_1 = Resources.Load<AudioClip>("BreakSfx_A02");
        enemyBreakSound_2 = Resources.Load<AudioClip>("explosion_5");
        erorrSound = Resources.Load<AudioClip>("eroee");
        enable_disable_sfx = Resources.Load<AudioClip>("enable_disable_sfx");
        asteriod_dead_sfx = Resources.Load<AudioClip>("asteriod_dead_sfx");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {
        
        if(GameManager.isUsingSoundEffects)
            switch(clip) 
            {
                case "BreakSfx_A02" :
                    audioSrc.PlayOneShot(enemyBreakSound_1);
                    break;
                case "explosion_5" :
                    audioSrc.PlayOneShot(enemyBreakSound_2);
                    break;
                case "eroee" :
                audioSrc.PlayOneShot(erorrSound);
                    break;
                case "enable_disable_sfx" :
                audioSrc.PlayOneShot(enable_disable_sfx);
                    break;
                case "asteriod_dead_sfx" :
                audioSrc.PlayOneShot(asteriod_dead_sfx);
                    break;
        }
    }
}
