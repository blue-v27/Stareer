using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameHasStared, gameHasEnded, isUsingSoundEffects = true, hasFinishedTutorial,postProcessingIsOn;
    
    /// Random Backgrounds
    public Sprite[] backGrounds;
    int random_backgorund;
    public GameObject BackGround;
    SpriteRenderer bg_render;

    ///Ui features
    public GameObject startMenu_DeathMenu_Panel, buy_upgrades_pannel, settings_menu_pannel, game_ui_pannel;
    public static bool startMenuIsDeathMenu = false;

    /// Buy Menu
    public static float Money = 0;
    float fixedMoney;
    bool buy_menu_is_open;

    /// settings menu
    public Sprite[] Sfx_enabled_disabled;

    ///Upgrades
    public static int PlayerSizeLevel, TargetHealthLevel, money_requierd_toUpradePlayerSize = 100, money_requierd_toUpgradeTargetHealth = 150;

    ///camera Shake
    Vector3 cameraInitialPosition;
	public float shakeMagnetude = 0.05f, shakeTime = 0.5f;
	public Camera mainCamera;
    int fixed_target_health = 10;
    public static bool shake_camera = false;

    /////Skins
    public static int player_skin = 0;
    public Sprite[] player_skin_button;
    public Color[] current_player_skin;

    
    /// Tutorial
    public static int tutorial_phase;
    bool turotrial_is_finished;

    ///Saving files
    bool hasSaved;
    

    private void Start() 
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.SetResolution(1920, 1080, true);

        Application.targetFrameRate = 144;   
        
        Time.timeScale = 0f;

        random_backgorund = Random.Range(0, backGrounds.Length);
        bg_render = BackGround.GetComponent<SpriteRenderer>();

        //loading save files

        hasSaved = false;
        
        LoadTheGame();
    }

    void OnApplicationQuit() 
    {
        ///Save
    }

    ///Button Functions
    public void Start_RestartTheGame()
    {   
        GameManager.gameHasStared = true;
        GameManager.gameHasEnded = false;
        startMenu_DeathMenu_Panel.SetActive(false);
        buy_upgrades_pannel.SetActive(false);
        settings_menu_pannel.SetActive(false);
        game_ui_pannel.SetActive(true);
        buy_menu_is_open = false;
        Time.timeScale = 1f;
        TargetManager.target_health = TargetManager.max_target_health;
        EnemySpawner.delay = 1f;
        EnemySpawner.timeLeft = Time.time;
        ScoreScript.score = 0;
        random_backgorund = Random.Range(0, backGrounds.Length);
        fixed_target_health = 0;
        
        Interstitial.starts++; 
        hasSaved = false;
        
    }

    private void FixedUpdate() 
    {
        if(gameHasEnded)
        {
            if(!buy_menu_is_open)
                startMenu_DeathMenu_Panel.SetActive(true);
            
            GameObject.Find("Start_StartTextButton").GetComponentInChildren<Text>().text = "Highest Score: " + ScoreScript.highestScore + "\nYour Score Was: " + ScoreScript.score + ". Tap to play again";
            GameObject.Find("Start_StartTextButton").transform.position = new Vector2(GameObject.Find("Start_StartTextButton").transform.position.x,675);
            GameObject.Find("Start_StartTextButton").GetComponentInChildren<Text>().fontSize = 125;
//          GameObject.Find("Start_StartTextButton").GetComponent<Text>().text = GameObject.Find("Start_StartTextButton").GetComponent<Text>().text.Replace("\\n", "\n");

            //Saving files

            if(!hasSaved)
            {
                PlayerPrefs.SetInt("HighScore", ScoreScript.highestScore);
                PlayerPrefs.Save();
                hasSaved = true;
            }
        }

        ///randomize backGround;
        
        bg_render.sprite = backGrounds[random_backgorund];
    }

    private void Update() 
    {
        /// shake the camera
        
        if(shake_camera && hasFinishedTutorial)
            ShakeIt();

        /// tutorial

        
        if(!hasFinishedTutorial && gameHasStared)
        {
            if(tutorial_phase == 0 && !turotrial_is_finished && gameHasStared)
            {                
                Time.timeScale = 0f;
                GameObject.Find("Tutorial_Text").GetComponent<Text>().text = "You are the little gray circle\nDrag across the screen to move";  

                if(Input.anyKeyDown)
                    tutorial_phase++;
            }
            else if(tutorial_phase == 1)
                {
                    Time.timeScale = 0f;
                    GameObject.Find("Tutorial_Text").GetComponent<Text>().text = "Your goal is to protect the ship\nfrom incoming asteroids";
                    if(Input.anyKeyDown)
                        tutorial_phase++;
                }
                else if(tutorial_phase == 2)
                    {
                        GameObject.Find("Tutorial_Text").GetComponent<Text>().text = "Move over them to destroy them and \n gather money to sped in the 'upgrades menu'";
                        if(Input.anyKeyDown)
                            tutorial_phase++;
                    }
                    else if(tutorial_phase == 3)
                        {
                            Time.timeScale = 1f;
                            GameObject.Find("Tutorial_Text").GetComponent<Text>().text = null;
                        }
                        else if(tutorial_phase == 4)
                            {
                                Time.timeScale = 0f;
                                GameObject.Find("Tutorial_Text").GetComponent<Text>().text = "The ship will take damage when hit by an asteroid\nYou can see it's health above it";
                                if(Input.anyKeyDown)
                                    tutorial_phase++;
                            }
                            else if(tutorial_phase == 5)
                                {
                                    GameObject.Find("Tutorial_Text").GetComponent<Text>().text = "Other stars or debris will appear now and again\nAvoid colliding with them";
                                    if(Input.anyKeyDown)
                                        tutorial_phase++;
                                }
                                else if(tutorial_phase >= 6)
                                    {
                                        Time.timeScale = 1f;
                                        PlayerPrefs.SetInt("Tutorial_phase", tutorial_phase);
                                        GameObject.Find("Tutorial_Text").GetComponent<Text>().text = null;
                                        hasFinishedTutorial = true;
                                        tutorial_phase += 69;
                                        Destroy(GameObject.Find("Tutorial_Text"));
                                    }

            GameObject.Find("Tutorial_Text").GetComponent<Text>().text =GameObject.Find("Tutorial_Text").GetComponent<Text>().text.Replace("\\n", "\n");
        }                            
    }

    public void OpenBuyMenu()
    {
        startMenu_DeathMenu_Panel.SetActive(false);
        game_ui_pannel.SetActive(false);
        buy_upgrades_pannel.SetActive(true);
        buy_menu_is_open = true;
    }

    public void PlayAdd()
    {

    }

    public void PlayerSizeUpgrade()
    {
        if(Money >= money_requierd_toUpradePlayerSize && PlayerSizeLevel < 3)
        {
            Money -= money_requierd_toUpradePlayerSize;
            PlayerSizeLevel++;
            money_requierd_toUpradePlayerSize *= 2;
            SoundManagerScript.PlaySound("BreakSfx_A02");

            ///Save
            PlayerPrefs.SetInt("PlayerSizeLevel", PlayerSizeLevel);
            PlayerPrefs.Save();
        }
    }

    public void TargetHealthUpgrade()
    {
        if(Money >= money_requierd_toUpgradeTargetHealth && TargetHealthLevel < 3)
        {
            Money -= money_requierd_toUpgradeTargetHealth;
            TargetHealthLevel++;
            money_requierd_toUpgradeTargetHealth *= 2;
            TargetManager.max_target_health += 50;
            SoundManagerScript.PlaySound("BreakSfx_A02");
            
            ///Save
            PlayerPrefs.SetInt("TargetHealthLevel", TargetHealthLevel);
            PlayerPrefs.Save();
        }
    }

    public void OpenSettingsMenu()
    {
        startMenu_DeathMenu_Panel.SetActive(false);
        settings_menu_pannel.SetActive(true);
        game_ui_pannel.SetActive(false);
    }

    public void Enable_Disable_Sfx()
    {
        if(isUsingSoundEffects)
        {
            isUsingSoundEffects = false; 
            GameObject.Find("Disable_Enable_Sfx").GetComponent<Image>().sprite = Sfx_enabled_disabled[0];
            SoundManagerScript.PlaySound("enable_disable_sfx");

            ///Save
        }
            else if(!isUsingSoundEffects)
            {
                isUsingSoundEffects = true;
                GameObject.Find("Disable_Enable_Sfx").GetComponent<Image>().sprite = Sfx_enabled_disabled[1];
                SoundManagerScript.PlaySound("enable_disable_sfx");

                ///Save
            }
    }

    public void Enable_Disable_GFX()
    {
        if(postProcessingIsOn)
        {
            postProcessingIsOn = false;  
            GameObject.Find("Disable_Enable_Gfx").GetComponentInChildren<Text>().text = "Gfx: Low";
            SoundManagerScript.PlaySound("enable_disable_sfx");

            ///Save
        }
            else if(!postProcessingIsOn)
            {
                postProcessingIsOn = true;  
                GameObject.Find("Disable_Enable_Gfx").GetComponentInChildren<Text>().text = "Gfx: High";
                SoundManagerScript.PlaySound("enable_disable_sfx");
                PlayerPrefs.SetInt("player_skin", player_skin);

                ///Save
            }
    }

    ///camera shake

    public void ShakeIt()
	{
		cameraInitialPosition = mainCamera.transform.position;
		InvokeRepeating ("StartCameraShaking", 0f, 0.005f);
		Invoke ("StopCameraShaking", shakeTime);
	}

	void StartCameraShaking()
	{
		float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
		float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
		Vector3 cameraIntermadiatePosition = mainCamera.transform.position;
		cameraIntermadiatePosition.x += cameraShakingOffsetX;
		cameraIntermadiatePosition.y += cameraShakingOffsetY;
		mainCamera.transform.position = cameraIntermadiatePosition;
	}

	void StopCameraShaking()
	{
		CancelInvoke ("StartCameraShaking");
		mainCamera.transform.position = cameraInitialPosition;
        shake_camera = false;
	}

    ///Change Player Skin
    public void ChangePlayerSkin()
    {       
        GameObject.Find("Player_Skin_Button").GetComponent<Image>().sprite = player_skin_button[player_skin];
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = current_player_skin[player_skin];
        GameObject.Find("Player").GetComponent<TrailRenderer>().startColor = current_player_skin[player_skin];
        GameObject.Find("Player").GetComponent<TrailRenderer>().endColor = current_player_skin[player_skin];

        player_skin++;
        
        if(player_skin == 8)
            player_skin -= 8;

        SoundManagerScript.PlaySound("enable_disable_sfx");

        ///Save
        PlayerPrefs.SetInt("player_skin", player_skin);
        PlayerPrefs.Save();
    }

    void LoadTheGame()
    {
        player_skin = PlayerPrefs.GetInt("player_skin");
        PlayerSizeLevel = PlayerPrefs.GetInt("PlayerSizeLevel");
        TargetHealthLevel = PlayerPrefs.GetInt("TargetHealthLevel");
        tutorial_phase = PlayerPrefs.GetInt("Tutorial_phase");
        ScoreScript.highestScore = PlayerPrefs.GetInt("HighScore");
    }
}
