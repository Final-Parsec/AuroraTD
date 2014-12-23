using UnityEngine;
using System.Collections;

public class GoToMainMenu : MonoBehaviour {

	private ObjectManager _ObjectManager;
	
	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick ()
	{
        _ObjectManager.DestroySinglton();
		Application.LoadLevel("Main Menu");
	}
}
