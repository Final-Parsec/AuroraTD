using System;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
	public GameObject[] tutorialSections;

	private Canvas canvas;
	private int currentTutorialSection = 0;
	private MainMenu menu;
	
	/// <summary>
	/// 	Use this for initialization
	/// </summary>
	void Start ()
	{
		var tutorialCanvas = GameObject.Find("Tutorial Canvas");
		this.canvas = tutorialCanvas.GetComponent<Canvas>();
		this.canvas.enabled = false;
		
		this.menu = GameObject.Find("Main Camera").GetComponent<MainMenu>();		
	}
	

	public void Continue()
	{
		if (!this.canvas.enabled)
		{
			this.canvas.enabled = true;
			this.currentTutorialSection = -1;
		}
		
		this.currentTutorialSection++;
		
		if (this.currentTutorialSection >= this.tutorialSections.Length)
		{
			this.BackToMenu();
			return;
		}
		
		this.tutorialSections[this.currentTutorialSection].SetActive(true);
		for (int i = 0; i < this.tutorialSections.Length; i++)
		{
			if (i != this.currentTutorialSection)
			{
				this.tutorialSections[i].SetActive(false);
			}
		}
	}
	
	public void BackToMenu()
	{
		this.canvas.enabled = false;
	}
}