using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankView : MonoBehaviour
{
    public Text subTitle;
    public Text[] player_dsp;
    public Text[] score_dsp;
    public Text error_dsp;
    public GameObject content_wrapper;

    public static string[] player = new string[3];
    public static int[] score = new int[3];
    
    public static bool rankingGet = false;

    void Start()
    {
        subTitle.text = SelectController.SelectedMusic.Title + " / " + SelectController.SelectedMusic.Artist;
        
        if(rankingGet){
            for(int i = 0; i < 3; i++){
                player_dsp[i].text = player[i];
                score_dsp[i].text = score[i].ToString();
            }

            content_wrapper.SetActive(true);
            error_dsp.enabled = false;
        }else{
            content_wrapper.SetActive(false);
            error_dsp.enabled = true;
            error_dsp.text = "ランキング取得に失敗しました";
        }
    }

    public void CloseDialog(){
        Destroy(this.gameObject);
    }
}
