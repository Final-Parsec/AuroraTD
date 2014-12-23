using UnityEngine;
using System.Collections;

public class DisplayGrid : MonoBehaviour {

	private ObjectManager _ObjectManager;

	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick ()
	{
		_ObjectManager.gameState.displayGrid = !_ObjectManager.gameState.displayGrid;
	}
}
