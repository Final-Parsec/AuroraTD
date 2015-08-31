using UnityEngine;
using System.Collections;

public class OptionScreenAnimationEvents : MonoBehaviour {
	private GameObject OptionScreen;
	
	void Start()
	{
		OptionScreen = GameObject.Find ("OptionsScreen");
	}

	public void OptionsFadeOutFinished()
	{
		OptionScreen.SetActive (false);
	}
}
