using System;
using System.Collections;
using SimpleJSON;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private UILabel nameList;
    private UILabel scoreList;
    // Use this for initialization
    public string url = "http://finalparsec.com/scores/test";

    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    private IEnumerator SendRequest()
    {
        var www = new WWW(this.url);
        yield return www;
        
        Debug.Log(www.text);
        var jsonNode = JSON.Parse(www.text);
        
        string playerString = jsonNode["player_name"];
        var rank = jsonNode["rank"].AsInt;
        var score = jsonNode["score"].AsInt;
        //Debug.Log(N["competitors"][0]["rank"].AsInt);
        Debug.Log(jsonNode["competitors"].Count);
        for (var x = 0; x < jsonNode["competitors"].Count; x++)
        {
            //formate list of names with numbers and names
            this.nameList.text = this.nameList.text + "\n" + jsonNode["competitors"][x]["rank"].AsInt;
            this.nameList.text = this.nameList.text + "  " + jsonNode["competitors"][x]["player_name"].Value;

            //dump list of scores
            this.scoreList.text = this.scoreList.text + "\n" + jsonNode["competitors"][x]["score"].AsInt;
            //Debug.Log (N["competitors"][x]["score"].AsInt);
            //Debug.Log (N["competitors"][x]["player_name"].Value);
        }
    }

    /// <summary>
    ///     Use for initialization.
    /// </summary>
    private void Start()
    {
        var namesList = GameObject.Find("NamesList");
        if (namesList == null) throw new InvalidOperationException("Names list must not be null.");
        this.nameList = namesList.GetComponent<UILabel>();

        var scoresList = GameObject.Find("ScoresList");
        if (scoresList == null) throw new InvalidOperationException("Scores list must not be null.");
        this.scoreList = scoresList.GetComponent<UILabel>();
        
        this.StartCoroutine(this.SendRequest());
    }
}