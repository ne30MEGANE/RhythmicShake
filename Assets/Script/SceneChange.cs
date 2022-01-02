using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // 任意のシーンに遷移する
    public void MoveTo(string scene){
        SceneManager.LoadScene(scene);
    }

}
