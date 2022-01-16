using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapScript : MonoBehaviour
{   
    bool[] touching; // 各レーンのタッチ状況を保持

    void Start()
    {
        touching = new bool[] {false, false, false, false};
    }

    public void TapDown(int lane){ // レーンがタップされたとき
        touching[lane] = true;
    }

    public void TapUp(int lane){ // レーンから手が離れた時
        if(touching[lane]) {
            touching[lane] = false;
        }
    }

    public bool GetLaneBool(int lane){ // レーンへの入力情報を返す
        return touching[lane];
    }
}
