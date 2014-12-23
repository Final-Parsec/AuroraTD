using UnityEngine;
using System.Collections;

public class EndGameMenu : MonoBehaviour {

	public UILabel conditionLabel;
	public UILabel scoreLabel;

	private ObjectManager objectManager;


	// Use this for initialization
	void Start () 
	{
		objectManager = ObjectManager.GetInstance ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(objectManager.Map.upcomingWaves.Count == 0 &&
		   objectManager.enemies.Count == 0 &&
		   objectManager.gameState.PlayerHealth > 0)
		{
			MoveEndGameMenu("Victory!");
		}
		else if(objectManager.gameState.PlayerHealth <= 0)
		{
			MoveEndGameMenu("Defeat!");
		}

	}

	private void MoveEndGameMenu(string conditionText)
	{
        objectManager.gameState.displayGrid = false;

		Vector3 center = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2, Screen.height/2, 0));
		center.y = transform.position.y;
		transform.position = center;

		conditionLabel.text = conditionText;
		scoreLabel.text = "Score: "+objectManager.gameState.score;

		objectManager.gameState.gameOver = true;
	}
}
