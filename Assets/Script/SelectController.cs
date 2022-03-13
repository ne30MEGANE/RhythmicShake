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
        SelectedLevel = 1; // easyが選ばれた状態にする
    }

    // 選曲されてる楽曲情報表示
    public Text Title;
    public Text Artist;
    public Text Level1;
    public Text Level2;

    // Update is called once per frame
    void Update()
    {
        // 選択されている楽曲情報表示
        Title.text = SelectedMusic.Title;
        Artist.text = SelectedMusic.Artist;
        Level1.text = SelectedMusic.Easy.ToString();
        Level2.text = SelectedMusic.Normal.ToString();
    }

    // レベル選択
    public static int SelectedLevel; // 1:easy 2:normal
    public GameObject Cursor, Easy, Normal;
    public void LevelSelect(int level){
        SelectedLevel = level;
        // Debug.Log("level select: " + SelectedLevel.ToString()); // for debug
        switch(level){
            case 1:
                Cursor.GetComponent<RectTransform>().localPosition = new Vector3(-220, -275, 0); // ←
                Easy.GetComponent<RectTransform>().localPosition = new Vector3(-220, -45, 0); // 上げる
                Normal.GetComponent<RectTransform>().localPosition = new Vector3(220, -90, 0); // 下げる
                break;
            case 2:
                Cursor.GetComponent<RectTransform>().localPosition = new Vector3(220, -275, 0); // →
                Easy.GetComponent<RectTransform>().localPosition = new Vector3(-220, -90, 0); // 下げる
                Normal.GetComponent<RectTransform>().localPosition = new Vector3(220, -45, 0); // 上げる
                break;
            default:
                break;
        }
    }

    // setting open
    [SerializeField] private Canvas parent = default;
    [SerializeField] private DialogScript dialog = default;

    public void OpenDialog(){
        DialogScript _dialog = Instantiate(dialog);
        _dialog.transform.SetParent(parent.transform, false);
    }
    
}
