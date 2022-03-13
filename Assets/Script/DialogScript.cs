using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
    // データ
    float highSpeed;
    string playerName;
    
    void Start()
    {
        highSpeed = PlayerData.HiSpeed;
        playerName = PlayerData.PlayerName;
    }

    public void CloseDialog(){
        Destroy(this.gameObject);
    }

}
