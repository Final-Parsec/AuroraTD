using System;
using System.Collections;
using SimpleJSON;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public static HighScore Instance = null;
    
    private GameObject highScoreInput;
    private UILabel nameList;
    private UILabel scoreList;
    
    // Use this for initialization
    private const string Url = "http://finalparsec.com/scores/";

    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    private IEnumerator SendRequest()
    {
        var playerName = GameObject.Find("PlayerNameTextField").GetComponent<Text>().text;
        var score = ObjectManager.GetInstance().gameState.score;

        var leaderboardName = string.Format(
            "Aurora TD {0} {1} {2}",
            ObjectManager.GetInstance().gameState.MapType,
            ObjectManager.GetInstance().gameState.friendlyDifficulty,
            ObjectManager.GetInstance().gameState.numberOfWaves == 300
                ? "Endless"
                : ObjectManager.GetInstance().gameState.numberOfWaves.ToString());

        var modifiedUrl = HighScore.Url + string.Format("{0}?limit={1}&player_name={2}&score={3}", leaderboardName, 10, playerName, score);
        modifiedUrl = modifiedUrl.Replace(" ", "%20");
        Debug.Log(modifiedUrl);

        var www = new WWW(modifiedUrl);

        yield return www;
        
        var jsonNode = JSON.Parse(www.text);

        this.nameList.text = string.Empty;
        this.scoreList.text = string.Empty;
        for (var x = 0; x < jsonNode["competitors"].Count; x++)
        {
            //format list of names with numbers and names
            this.nameList.text = this.nameList.text + jsonNode["competitors"][x]["player_name"].Value + "\n";

            //dump list of scores
            this.scoreList.text = this.scoreList.text + jsonNode["competitors"][x]["score"].AsInt + "\n";
        }
    }

    /// <summary>
    ///     Use for initialization.
    /// </summary>
    private void Start()
    {
        HighScore.Instance = this;

        this.highScoreInput = GameObject.Find("HighScoreInput");
        this.highScoreInput.SetActive(false);

        var namesList = GameObject.Find("NamesList");
        if (namesList == null) throw new InvalidOperationException("Names list must not be null.");
        this.nameList = namesList.GetComponent<UILabel>();

        var scoresList = GameObject.Find("ScoresList");
        if (scoresList == null) throw new InvalidOperationException("Scores list must not be null.");
        this.scoreList = scoresList.GetComponent<UILabel>();
    }

    public void MoveToSubmitScoreState()
    {
        this.highScoreInput.SetActive(true);
    }

    public void SubmitScore()
    {
        if (string.IsNullOrEmpty(GameObject.Find("PlayerNameTextField").GetComponent<Text>().text))
        {
            return;
        }

        this.StartCoroutine(this.SendRequest());
        var yourScoreLabel = GameObject.Find("PlayerScore").GetComponent<UILabel>();
        yourScoreLabel.text = ObjectManager.GetInstance().gameState.score.ToString();
        var highScoresFrame = GameObject.Find("HighScoresMenu");
        highScoresFrame.transform.position = new Vector3(highScoresFrame.transform.position.x, 90, highScoresFrame.transform.position.z);
        this.highScoreInput.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Application.LoadLevel("Main Menu");
    }
}