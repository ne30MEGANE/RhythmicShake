using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class PlayController : MonoBehaviour
{
    // オーディオ関係
    public AudioSource musicSource;
    private AudioClip musicClip;
    static public AudioSource tapSound;
    static float startTime = 0;

    // ノーツ関係
    int[] type, option;
    float[] timing;
    public GameObject[] notes; // 0:通常ノーツ 1:シェイク(自由方向) 2:左シェイク 3:右シェイク
    float highSpeed;
    public GameObject judgeLine;
    float lineY;

    // 表示関係
    public Text title;
    public Text combo;

    // プレイ管理
    bool isPlaying = false;
    int notesCount = 0;
    public float offset = -1; // 正解タイミングに対してどのタイミングでノーツを生成するか

    // リザルト画面にも引き継ぐ情報
    static int comboCount = 0;

    // for debug
    public bool TestMode;

    void Start()
    {
        // 初期化
        type = new int[1024];
        option = new int[1024];
        timing = new float[1024];
        highSpeed = PlayerData.HiSpeed;
        lineY = judgeLine.gameObject.transform.position.y;
        tapSound = GameObject.Find("Tap").GetComponent<AudioSource>();

        // for debug
        if(TestMode){
            SelectController.SelectedMusic = new FancyScrollView.MusicData("oheya","私たちのお部屋","nem*",1,4);
            SelectController.SelectedLevel = 2;
        }

        // 表示関係
        string titleText = "♪" + SelectController.SelectedMusic.Title;
        title.text = titleText;

        // プレイ開始準備
        string musicID = SelectController.SelectedMusic.MusicID;
        SetMusic(musicID);
        LoadChart(musicID, SelectController.SelectedLevel);

        // プレイ開始
        Invoke(nameof(PlayStart), 1f);
    }
    
    void Update()
    {
        if(isPlaying){
            // ノーツ生成
            GenerateChart();

            // コンボ表示
            // if(comboCount > 5) combo.text = comboCount.ToString();
        }

    }

    private void SetMusic(string musicID) // 音声ファイルを取得してオーディオソースを設定
    {


        string filepath = "Audio/" + musicID;
        musicClip = Resources.Load<AudioClip>(filepath);
        musicSource.GetComponent<AudioSource>().clip = musicClip;
    }

    private void LoadChart(string musicID, int chartNum) // 譜面ファイルを取得して解釈
    {
        string filepath = "Chart/" + musicID + "_" + chartNum.ToString(); // Chart/musicId_n
        TextAsset chartCSV = Resources.Load<TextAsset>(filepath);

        StringReader reader = new StringReader(chartCSV.text);
        for(int i = 0; reader.Peek() > -1; i++){
            string line = reader.ReadLine();
            string[] values = line.Split(','); // timing, type, (option)
            timing[i] = float.Parse(values[0]);
            type[i] = int.Parse(values[1]);
            if(!(string.IsNullOrEmpty(values[2]))) option[i] = int.Parse(values[2]);
        }
    }

    private void GenerateNote(float timing, int type, int opt = 0) // ノーツオブジェクトを生成
    {
        switch(type){
            case 0: // tap
                GameObject tap = Instantiate(notes[0], new Vector3(-6.0f + (4.0f*opt), highSpeed + lineY, 0), Quaternion.identity);
                tap.GetComponent<Note>().DataSet(timing, opt);
                break;
            case 1: // free shake
                GameObject shake = Instantiate(notes[1], new Vector3(0, highSpeed + lineY, 0), Quaternion.identity);
                shake.GetComponent<Note>().DataSet(timing);
                break;
            default:
                break;
        }
    }

    public static float GetMusicTime() // 曲が始まってからの経過時間を返す
    {
        return Time.time - startTime;
    }

    private void GenerateChart() // ノーツを生成して譜面を作成
    {
        while(timing[notesCount] + offset < GetMusicTime() && timing[notesCount] != 0)
        {
            GenerateNote(timing[notesCount], type[notesCount], option[notesCount]);
            notesCount++;
        }
    }

    public static void Success() // PERFORMER・GREAT
    {
        PlayController.tapSound.Play();
        PlayController.comboCount++;
        Debug.Log("コンボ: " + comboCount.ToString()); // for debug
    }

    public static void Bad() // NICE TRY
    {
        PlayController.tapSound.Play();
        PlayController.comboCount = 0;
    }

    public static void Miss() // THROUGH
    {
        PlayController.comboCount = 0;
    }

    private void PlayStart() // プレイ開始処理
    {
        startTime = Time.time;
        isPlaying = true;
        musicSource.Play();
    }
}
