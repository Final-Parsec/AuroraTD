using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EventHandler : MonoBehaviour
{
	private readonly int numberOfTurrets = Enum.GetNames (typeof(TurretType)).Length;
    private ObjectManager objectManager;
    private readonly List<string> turretFocusMenuObjects = new List<string>()
	{
		"UpgradeOne",
		"UpgradeTwo",
		"UpgradeThree",
		"Sell"
	};

	// Initialization
	void Start ()
	{
		objectManager = ObjectManager.GetInstance ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log (objectManager.gameState.optionsOn);
		if (objectManager.gameState.optionsOn)
			return;

		// Left Click Down & Tuoch Event
		if (Input.GetMouseButtonDown (0)) {
			Vector3 mousePosition = Input.mousePosition;
						
			// Top 20% of screen is reserved for GUI. Game ignores this.
			var guiClick = (mousePosition.y / Screen.height) >= .8;
			if (guiClick || objectManager.gameState.gameOver)
				return;
			
			if (objectManager.TurretFocusMenu.SelectedTurret == null) {
				objectManager.TurretFactory.PlaceOrSelectTurret(mousePosition);
			}
			else {

				if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) {
                    objectManager.TurretFocusMenu.SelectedTurret = null;
				}
			}
		}
		
		// Check if a number has been pressed and change turret type we're producing.
		for (int i = 1; i <= numberOfTurrets; i++) {
			TurretType associatedType = (TurretType)(i - 1);  // Subtract one because enum indexes begin start 0.
			if (Input.GetKeyDown ("" + i)) {
				objectManager.TurretFactory.TurretType = associatedType;
				Debug.Log ("Selected " + objectManager.TurretFactory.TurretType);
			}
		}
				
		// Escape (Back button on Android)
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}