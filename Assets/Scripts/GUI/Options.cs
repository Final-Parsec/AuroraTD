using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {

	private ObjectManager _ObjectManager;
	public Transform OptionsMenu;
	
	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick ()
	{
        if ((_ObjectManager.Map.upcomingWaves.Count == 0 &&
           _ObjectManager.enemies.Count == 0 &&
           _ObjectManager.gameState.PlayerHealth > 0) ||
           _ObjectManager.gameState.PlayerHealth <= 0)
        {
            return;
        }

		_ObjectManager.gameState.optionsOn = !_ObjectManager.gameState.optionsOn;

		if(_ObjectManager.gameState.optionsOn){
			Vector3 center = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2, Screen.height/2, 0));
			center.y = OptionsMenu.position.y;
			OptionsMenu.position = center;
		}else {
			OptionsMenu.position = new Vector3(45,OptionsMenu.position.y,-300);
		}
	}

}
