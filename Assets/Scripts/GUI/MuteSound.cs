using UnityEngine;
using System.Collections;

public class MuteSound : MonoBehaviour {

	private ObjectManager _ObjectManager;
	
	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick ()
	{
		_ObjectManager.gameState.isMuted = !_ObjectManager.gameState.isMuted;
	}
}
