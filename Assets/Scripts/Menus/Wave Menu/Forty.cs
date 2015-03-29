using UnityEngine;
using System.Collections;

public class Forty : MonoBehaviour {
	MainMenu _Menu;
	// Use this for initialization
	void Start () {
		_Menu = GameObject.Find("Main Camera").GetComponent<MainMenu>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// Raises the mouse Down event.
	/// </summary>
	void OnMouseDown(){
		GetComponent<Renderer>().material.color = Color.red;
	}
	
	/// <summary>
	/// Raises the mouse up event.
	/// </summary>
	void OnMouseUp(){
		GetComponent<Renderer>().material.color = Color.white;
        _Menu.gameState.numberOfWaves = 40;
		_Menu.GoToGame();
	}
}