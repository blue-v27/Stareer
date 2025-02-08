using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMoney : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tag == "targetHealthUpgradeText")
        {
            if(GameManager.TargetHealthLevel < 3)
                text.text = "Current Helth(" + TargetManager.max_target_health + ") + 50 \n Price " + GameManager.money_requierd_toUpgradeTargetHealth; 
                    else
                        text.text = "Current Helth " + TargetManager.max_target_health;
        }
            else if(tag == "playerSizeUpgradeText")
            {
                if(GameManager.PlayerSizeLevel < 3)
                    text.text = "Current Size(" + PlayerScript.player_newSize + ") - 0.1 \n Price " + GameManager.money_requierd_toUpradePlayerSize;
                        else
                            text.text = "Current Size " + PlayerScript.player_newSize;
            }    
                else
                    text.text = "Money " + GameManager.Money;

        text.text = text.text.Replace ("\\n", "\n");
    }
}
