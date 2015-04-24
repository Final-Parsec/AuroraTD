using UnityEngine;
using System.Collections;

public class HelpText : MonoBehaviour
{
	Canvas canvas;
	MainMenu menu;
	
	/// <summary>
	/// 	Use this for initialization
	/// </summary>
	void Start ()
	{
		canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		canvas.enabled = false;
		menu = GameObject.Find("Main Camera").GetComponent<MainMenu>();		
	}
	
	/// <summary>
	/// 	Raised when the mouse is clicked.
	/// </summary>
	void OnMouseDown()
	{
		GetComponent<Renderer>().material.color = Color.blue;
	}
	
	/// <summary>
	/// 	Raised when the mouse is released.
	/// </summary>
	void OnMouseUp()
	{
		GetComponent<Renderer>().material.color = Color.white;
		this.canvas.enabled = true;
		this.menu.GoToTutorialOne();
	}
}