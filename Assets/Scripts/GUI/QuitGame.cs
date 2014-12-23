using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {

	private ObjectManager _ObjectManager;
	
	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
		
	}
	
	void OnClick ()
	{
		Application.Quit ();
	}
}
