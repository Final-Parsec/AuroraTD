using UnityEngine;

public class TurretFocusMenuButtonHandler : MonoBehaviour {
	// Configurable
	public string buttonName = "none";
	
	// Internal
	private ObjectManager objectManager;	
	
	
	// Runs when entity is Instantiated
	void Awake ()
	{
		objectManager = ObjectManager.GetInstance ();
	}
	
	void OnClick () {
		switch (buttonName) {
			case "UpgradeOne":
                objectManager.TurretFocusMenu.UpgradeOne();
				break;
			case "UpgradeTwo":
                objectManager.TurretFocusMenu.UpgradeTwo();
				break;
			case "UpgradeThree":
                objectManager.TurretFocusMenu.UpgradeThree();
				break;
			case "Sell":
				objectManager.TurretFocusMenu.Sell();
				break;
		}	
	}
	
	public void OnClick(string buttonName) {
		this.buttonName = buttonName;
		OnClick();
	}
	
	// Use this for initialization
	void Start () {
	
	}
}