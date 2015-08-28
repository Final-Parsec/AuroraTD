using UnityEngine;
using System.Collections;

public class DisplayGrid : MonoBehaviour {

	private ObjectManager _ObjectManager;
	bool gridToggle = true;

	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick ()
	{
		gridToggle = !gridToggle;
		_ObjectManager.Map.SetGrid(gridToggle);
	}
}
