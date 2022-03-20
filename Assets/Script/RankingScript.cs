using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RankingScript : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;
    [SerializeField] private ScoreModal dialog = default;

    int ID;
    string playerName;
    int totalScore;

    string msg;

    void Start(){
        ID = SelectController.SelectedMusic.ID;
        playerName = PlayerData.PlayerName;
        totalScore = PlayController.scoreCount;
    }

    public void trySend(){ // on click
        StartCoroutine(ScoreSend(ID, playerName, totalScore));
    }

    IEnumerator ScoreSend(int id, string name, int score){
        if(ResultController.posted){
            msg = "このスコアは送信済みです";
        }else{
            WWWForm form = new WWWForm();
            form.AddField("music_id", id);
            form.AddField("player", name);
            form.AddField("score", score);

            string url = "http://localhost/RhythmicShake/score.php";
            // string url = "http://192.168.xxx.xxx/RhythmicShake/rank.php";

            UnityWebRequest rq = UnityWebRequest.Post(url, form);
            yield return rq.SendWebRequest();

            if(rq.result == UnityWebRequest.Result.Success){
                Debug.Log("Success"); // for debug
                msg = "スコアを送信しました！";
                ResultController.posted = true; // 送信済みフラグオン
            }else{ // failed
                msg = "スコアの送信に失敗しました";
                Debug.Log("Failed"); // for debug
            }
        }

    // dialog open
    ScoreModal.postResult = msg;
    ScoreModal _dialog = Instantiate(dialog);
    _dialog.transform.SetParent(parent.transform, false);

    }

    public void CloseDialog(){
        Destroy(this.gameObject);
    }

}
