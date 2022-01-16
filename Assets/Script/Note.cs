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
            Destroy(this.gameObject);
        }
    }

    public void DataSet(float _timing, int _opt = 0)
    {
        timing = _timing;
        option = _opt;
    }
}
