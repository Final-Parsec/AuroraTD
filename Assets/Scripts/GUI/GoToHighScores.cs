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
		oldMenuPosition = GameObject.Find ("EndGameMenu").GetComponent<EndGameMenu>().GoAway ();
		GameObject HighScoresFrame = GameObject.Find ("HighScoresMenu");
		HighScoresFrame.transform.position = new Vector3(HighScoresFrame.transform.position.x, 9, HighScoresFrame.transform.position.z);
		transform.position = oldMenuPosition;
	}
}