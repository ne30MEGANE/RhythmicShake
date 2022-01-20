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
    float JudgeRange = (float)0.08; // 入力を受け付け時間
    float Performer; // P判定
    float Great;
    float NiceTry;

    void Start()
    {
        // プレイヤー設定からハイスピを読み込む
        highSpeed = PlayerData.HiSpeed;
    }

    void Update()
    {
        // ノーツを落とす処理
        this.transform.position += Vector3.down * highSpeed * Time.deltaTime;
        if(this.transform.position.y < -7.0f){ // 叩かずに通過した場合の処理
            PlayController.Miss();
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
                if(TapScript.GetLaneBool(this.option)){
                    PlayController.Success();
                    Destroy(this.gameObject);
                }
                break;
            case 1: // free shake
                // if(){}
                break;
            default:
                break;
        }
    }

}
