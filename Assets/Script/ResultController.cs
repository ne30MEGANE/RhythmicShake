using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultController : MonoBehaviour
{
    public Text[] Header; // 0:title 1: artist
    public Image LevelColor;
    public Text LevelNumber;
    public Text[] Count;
    public Text Score;

    public GameObject FullCombo;
    public GameObject HighScore;

    void Start()
    {
        // タイトル・アーティスト
        Header[0].text = SelectController.SelectedMusic.Title;
        Header[1].text = SelectController.SelectedMusic.Artist;

        // 難易度
        int level = SelectController.SelectedLevel;
        LevelNumber.text = level.ToString();
        if(level == 1){ // easy
            LevelColor.GetComponent<Image>().color = new Color32(137, 193, 144, 255);
        }else if(level == 2){ // normal
            LevelColor.GetComponent<Image>().color = new Color32(240, 144, 168, 255);
        }

        // 判定
        Count[0].text = PlayController.performerCount.ToString();
        Count[1].text = PlayController.greatCount.ToString();
        Count[2].text = PlayController.nicetryCount.ToString();
        Count[3].text = PlayController.missCount.ToString();

        // スコア
        // Score.text = xxxx // update: スコア加算機能実装後

        // フルコン・ハイスコア表示管理
        FullCombo.SetActive(false);
        HighScore.SetActive(false);
        if(PlayController.missCount == 0 && PlayController.nicetryCount == 0){
            FullCombo.SetActive(true);
        }
        // update: スコア加算機能実装後
    }

}
