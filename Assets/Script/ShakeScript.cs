using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScript : MonoBehaviour
{
    public static ShakeScript instance;

    public float Threshold0, Threshold1; // 0:自由方向シェイクのしきい値、1:方向指定シェイクのしきい値
    private float sqrThreshold0, sqrThreshold1;

    public float XdirThrehold; // 左右方向判定のしきい値

    private bool shake;

    void Awake() // インスタンス化
    {
        if(instance == null) instance = this;
    }
    
    void Start()
    {
        sqrThreshold0 = Mathf.Pow(Threshold0, 2);
        sqrThreshold1 = Mathf.Pow(Threshold1, 2);
        shake = false;
    }

    public bool GetShakeBool(){ // 自由方向シェイク判定用
        JudgeShake();
        return shake;
    }

    public bool GetShakeBool(int dir){ // 方向指定シェイク判定用
        JudgeShake(dir);
        return shake;
    }

    private void ShakeFlagSwitch(){
        shake = true;
        Invoke(nameof(ShakeFlagDown),0.1f); // 0.1秒後にオフ
    }

    private void ShakeFlagDown(){
        shake = false;
    }

    private void JudgeShake(){ // 自由方向シェイク用 判定本体
        Vector3 dir = Input.acceleration;
        if(dir.sqrMagnitude >= sqrThreshold0 && !shake){
            ShakeFlagSwitch();
        }
    }

    private void JudgeShake(int JudgeType){ // 方向指定シェイク用 判定本体
        Vector3 dir = Input.acceleration;
        switch(JudgeType){
            case 0: // 方向指定判定(左)
                if(dir.x > XdirThrehold){
                    if(dir.sqrMagnitude >= sqrThreshold1 && !shake){
                        ShakeFlagSwitch();
                    }
                }
                break;
            case 1: // 方向指定判定(右)
                if(dir.x < Mathf.Abs(XdirThrehold)){
                    if(dir.sqrMagnitude >= sqrThreshold1 && !shake){
                        ShakeFlagSwitch();
                    }
                }
                break;
            default:
                break;
        }
    }
}
