using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class RankViewScript : MonoBehaviour
{
    [SerializeField] private Canvas parent = default;
    [SerializeField] private RankView ranking = default;

    public void Call(){ // on click
        StartCoroutine(GetRanking(SelectController.SelectedMusic.ID));
    }

    IEnumerator GetRanking(int id){
        // string url = "http://localhost/RhythmicShake/rank.php?music_id=" + id.ToString() + "&limit=3";
        string url = "http://172.20.10.6/RhythmicShake/rank.php?music_id=" + id.ToString() + "&limit=3";

        UnityWebRequest rq = UnityWebRequest.Get(url);
        rq.SetRequestHeader("Content-Type", "application/json");
        yield return rq.SendWebRequest();

        // get 成功時
        if(rq.result == UnityWebRequest.Result.Success){
            string json = rq.downloadHandler.text;
            Rankings json_parse = JsonUtility.FromJson<Rankings>(json);
            // Debug.Log(json_parse.rankings.Length); // for debug
            
            int json_length = json_parse.rankings.Length;
            for(int i = 0; i < json_length; i++){ // データがあった分 0 ~ length
                RankView.player[i] = json_parse.rankings[i].player;
                RankView.score[i] = json_parse.rankings[i].score;
            }
            for(int i = json_length; i < 3; i++){ // データがなかった分 length ~ 3
                RankView.player[i] = "NoData";
                RankView.score[i] = 0;
            }            
            RankView.rankingGet = true;
        }

        // modal open
        RankView _ranking = Instantiate(ranking);
        _ranking.transform.SetParent(parent.transform, false);
    }

    [System.Serializable] public class Rankings{
        public Ranking[] rankings;
    }
    [System.Serializable] public class Ranking {
        public string player;
        public int score;
    }    
}
