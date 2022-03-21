using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    // データ
    float highSpeed;
    string playerName;

    // UI
    public Slider slider;
    public Text slider_dsp;

    public InputField input;
    
    void Start()
    {
        highSpeed = PlayerData.HiSpeed;
        playerName = PlayerData.PlayerName;

        slider.value = highSpeed;
        slider_dsp.text = "現在の設定 ： " + highSpeed.ToString();

        input.text = playerName;

    }

    public void CloseDialog(){
        PlayerData.HiSpeed = highSpeed;
        PlayerData.PlayerName = playerName;
        Debug.Log(playerName);
        Destroy(this.gameObject);
    }

    public void HighSpeedSet(){
        highSpeed = slider.value;
        slider_dsp.text = "現在の設定 ： " + highSpeed.ToString();
    }

    public void PlayerNameSet(){
        playerName = input.text;
    }

}
