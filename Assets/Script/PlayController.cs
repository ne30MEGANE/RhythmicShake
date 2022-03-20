using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour
{
    // インスタンス化
    public static PlayController instance;

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
    public GameObject comboArea;

    // 判定表示
    public Image displayArea;
    public Sprite[] JudgeImage;

    // プレイ管理
    bool isPlaying = false;
    int notesCount = 0;
    public float offset = -1; // 正解タイミングに対してどのタイミングでノーツを生成するか

    // リザルト画面にも引き継ぐ情報
    public static int comboCount = 0;
    public static int performerCount = 0;
    public static int greatCount = 0;
    public static int nicetryCount = 0;
    public static int missCount = 0;

    // スコア関係
    private int[] tapScore = {500, 350, 200}; // 0:p, 1:g, 2:nt
    private int[] shakeScore = {700, 400}; // 0:p, 1:g
    public Text score;
    public static int scoreCount = 0;

    // for debug
    public bool TestMode;

    void Awake()
    {
        if(instance == null) instance = this;
    }

    void Start()
    {
        // 初期化
        type = new int[1024];
        option = new int[1024];
        timing = new float[1024];
        performerCount = 0;
        greatCount = 0;
        nicetryCount = 0;
        missCount = 0;
        comboCount = 0;
        scoreCount = 0;
        highSpeed = PlayerData.HiSpeed;
        lineY = judgeLine.gameObject.transform.position.y;
        tapSound = GameObject.Find("Tap").GetComponent<AudioSource>();

        // for debug
        if(TestMode){
            SelectController.SelectedMusic = new FancyScrollView.MusicData(1, "oheya","私たちのお部屋","nem*",1,4);
            SelectController.SelectedLevel = 2;
        }

        // 表示関係
        string titleText = "♪" + SelectController.SelectedMusic.Title;
        title.text = titleText;
        displayArea.enabled = false;
        comboArea.SetActive(false);
        score.text = scoreCount.ToString();

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
            if(comboCount > 4){ // 5combo未満のとき表示しない
                combo.text = comboCount.ToString();
                comboArea.SetActive(true);
            }

            // スコア表示
            score.text = scoreCount.ToString();
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
            case 2: // L shake
                GameObject shakeL = Instantiate(notes[2], new Vector3(0, highSpeed + lineY, 0), Quaternion.identity);
                shakeL.GetComponent<Note>().DataSet(timing);
                break;
            case 3: // R shake
                GameObject shakeR = Instantiate(notes[3], new Vector3(0, highSpeed + lineY, 0), Quaternion.identity);
                shakeR.GetComponent<Note>().DataSet(timing);
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
        while(timing[notesCount] + offset < GetMusicTime())
        {   
            if(timing[notesCount] == 0){ // 最後のノーツを生成した後
                Invoke(nameof(ToResultScene), 5f);
                break;
            }
            GenerateNote(timing[notesCount], type[notesCount], option[notesCount]);
            notesCount++;
        }
    }

    public void Performer(bool shake = false) // PERFORMER
    {
        tapSound.Play();
        if(shake) scoreCount += tapScore[0];
        else scoreCount += shakeScore[0];
        performerCount++;
        comboCount++;
        JudgeDisplay(0);
        // Debug.Log("コンボ: " + comboCount.ToString()); // for debug
    }

    public void Great(bool shake = false) // GREAT
    {
        tapSound.Play();
        if(shake) scoreCount += tapScore[1];
        else scoreCount += shakeScore[1];
        greatCount++;
        comboCount++;
        JudgeDisplay(1);
    }

    public void Bad() // NICE TRY
    {
        tapSound.Play();
        scoreCount += tapScore[2];
        nicetryCount++;
        ComboReset();
        JudgeDisplay(2);
    }

    public void Miss() // THROUGH
    {
        missCount++;
        ComboReset();
        JudgeDisplay(3);
    }

    private void JudgeDisplay(int judgeNum){ // 指定した番号の判定を表示させる
        displayArea.sprite = JudgeImage[judgeNum]; // 0:Per 1:Gr 2:NT 3:TH
        displayArea.enabled = true;
        Invoke(nameof(Hide), 0.35f);
    }

    private void Hide(){
        displayArea.enabled = false;
        
    }

    private void ComboReset(){
        comboCount = 0;
        comboArea.SetActive(false);
    }

    private void PlayStart() // プレイ開始処理
    {
        startTime = Time.time;
        isPlaying = true;
        musicSource.Play();
    }

    private void ToResultScene() // プレイ終了処理
    {
        musicSource.Stop();
        SceneManager.LoadScene("ResultScene");
    }
}
