using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public static FancyScrollView.MusicData SelectedMusic; // 選曲された楽曲情報,内部タイトルで保持
    
    // Start is called before the first frame update
    void Start()
    {
        SelectedMusic = FancyScrollView.ScrollView.MusicList[0]; // 先頭の楽曲が選ばれた状態にする
    }

    // 選曲されてる楽曲情報表示
    public Text Title;
    public Text Artist;

    // Update is called once per frame
    void Update()
    {
        Title.text = SelectedMusic.Title;
        Artist.text = SelectedMusic.Artist;
    }

    
}
