using UnityEngine;
using System.Collections;

public class FreeSpaceClickDetector : MonoBehaviour {
	private ObjectManager _ObjectManager;
	public Transform OptionsMenu;
	
	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick ()
	{

		if(_ObjectManager.gameState.optionsOn){
			_ObjectManager.gameState.optionsOn = false;
			OptionsMenu.position = new Vector3(45,OptionsMenu.position.y,-300);
		}
	}
}
