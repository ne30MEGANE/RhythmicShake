using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreModal : MonoBehaviour
{

    public Text postResult_dsp;
    public static string postResult = "";
    
    void Start()
    {
        postResult_dsp.text = postResult;
    }

    public void CloseDialog(){
        Destroy(this.gameObject);
    }

}
