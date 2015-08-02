using UnityEngine;
using System.Collections;

public class GoToHighScores : MonoBehaviour {
	
	private ObjectManager _ObjectManager;
	private Vector3 oldMenuPosition;

	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}

	void OnClick ()
	{
	    this.oldMenuPosition = GameObject.Find ("EndGameMenu").GetComponent<EndGameMenu>().GoAway();
        HighScore.Instance.MoveToSubmitScoreState();
        //GameObject HighScoresFrame = GameObject.Find ("HighScoresMenu");
        //HighScoresFrame.transform.position = new Vector3(HighScoresFrame.transform.position.x, 9, HighScoresFrame.transform.position.z);
	    this.transform.position = this.oldMenuPosition;
	}
}