using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private float highSpeed;
    
    // プレハブ作成時にインスペクタで設定
    public int type;
    
    // 譜面生成時にプレイコントローラーから設定
    public float timing = 0;
    public int option = 0;

    // 判定関係の設定 justとの絶対値で指定
    float JudgeRange = (float)0.3; // 入力を受け付け時間
    float Performer = (float)0.1; // P判定
    float Great = (float)0.2;

    void Start()
    {
        // プレイヤー設定からハイスピを読み込む
        highSpeed = PlayerData.HiSpeed;
    }

    void Update()
    {
        // ノーツを落とす処理
        this.transform.position += Vector3.down * highSpeed * Time.deltaTime;
        if(this.timing + JudgeRange < PlayController.GetMusicTime()){ // 叩かずに通過した場合の処理
            PlayController.instance.Miss();
            Destroy(this.gameObject);
        }

        // 判定有効範囲だけ入力を確認
        if (CheckJudgeRange()){
            CheckInput();
        }
    }

    public void DataSet(float _timing, int _opt = 0)
    {
        timing = _timing;
        option = _opt;
    }

    bool CheckJudgeRange()
    {
        float now = PlayController.GetMusicTime();
        if( this.timing - JudgeRange < now && now < this.timing + JudgeRange) {
            return true;
        }else{
            return false;
        }
    }

    void CheckInput()
    {
        switch(this.type){
            case 0: // tap
                if(TapScript.instance.GetLaneBool(this.option)){
                    JudgeRank_tap(PlayController.GetMusicTime());
                    Destroy(this.gameObject);
                }
                break;
            case 1: // free shake
                if(ShakeScript.instance.GetShakeBool()){
                    JudgeRank_shake(PlayController.GetMusicTime());
                    Destroy(this.gameObject);
                }
                break;
            case 2: // L shake
                if(ShakeScript.instance.GetShakeBool(0)){
                    JudgeRank_shake(PlayController.GetMusicTime());
                    Destroy(this.gameObject);
                }
                break;
            case 3: // R shake
                if(ShakeScript.instance.GetShakeBool(1)){
                    JudgeRank_shake(PlayController.GetMusicTime());
                    Destroy(this.gameObject);
                }
                break;
            default:
                break;
        }
    }

    void JudgeRank_tap(float now) // 引数:入力タイミング
    {
        if(this.timing - Performer < now && now < this.timing + Performer) PlayController.instance.Performer();
        else if(this.timing - Great < now && now < this.timing + Great) PlayController.instance.Great();
        else PlayController.instance.Bad();
    }

    void JudgeRank_shake(float now) // シェイク判定用
    {
        if(this.timing - Performer < now && now < this.timing + Performer) PlayController.instance.Performer();
        else PlayController.instance.Great();
    }
}
