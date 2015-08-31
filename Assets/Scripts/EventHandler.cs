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
				string first = UICamera.lastHit.collider.gameObject.name;

				RaycastHit[] hits = Physics.RaycastAll(UICamera.currentCamera.ScreenPointToRay(mousePosition));
				bool turretFocusMenuClicked = false;
								
				foreach (RaycastHit hit in hits) {
					if (turretFocusMenuObjects.Contains(hit.collider.gameObject.name)) {
						turretFocusMenuClicked = true;
					}
				}
				
				if (turretFocusMenuClicked && first.Contains("Turret(Clone)")) {
					string name = hits[0].collider.gameObject.name;

					var handler = GameObject.Find(name).GetComponent<TurretFocusMenuButtonHandler>();
					if (handler != null) { 
						handler.OnClick(name);
					}
				}
				
				if (!turretFocusMenuClicked) {
                    objectManager.TurretFocusMenu.SelectedTurret = null;
					objectManager.TurretFocusMenu.transform.position = new Vector3(
						objectManager.TurretFocusMenu.transform.position.x,
						objectManager.TurretFocusMenu.transform.position.y - 1000,
						objectManager.TurretFocusMenu.transform.position.z
					);                    
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