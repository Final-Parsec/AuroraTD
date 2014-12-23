using UnityEngine;
using System.Collections;

public class Right4 : MonoBehaviour {
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
		renderer.material.color = Color.blue;
	}
	
	/// <summary>
	/// Raises the mouse up event.
	/// </summary>
	void OnMouseUp(){
		renderer.material.color = Color.white;
		_Menu.GoToMainMenuFast();
		Debug.Log ("Right4");
	}
}
