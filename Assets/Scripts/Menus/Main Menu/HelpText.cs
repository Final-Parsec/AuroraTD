using UnityEngine;
using System.Collections;

public class HelpText : MonoBehaviour
{
	TutorialManager tutorialManager;
	
	/// <summary>
	/// 	Use this for initialization
	/// </summary>
	void Start ()
	{
		var tutorialCanvas = GameObject.Find("Tutorial Canvas");
		this.tutorialManager = tutorialCanvas.GetComponent<TutorialManager>();
	}
	
	/// <summary>
	/// 	Raised when the mouse is clicked.
	/// </summary>
	void OnMouseDown()
	{
		this.GetComponent<Renderer>().material.color = Color.blue;
	}
	
	/// <summary>
	/// 	Raised when the mouse is released.
	/// </summary>
	void OnMouseUp()
	{
		this.GetComponent<Renderer>().material.color = Color.white;
		this.tutorialManager.Continue();
	}
}