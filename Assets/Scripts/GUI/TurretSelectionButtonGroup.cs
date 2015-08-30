using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class TurretSelectionButtonGroup : MonoBehaviour
{
    private ObjectManager objectManager;
	private CanvasGroup myCanvasGroup;
	private CanvasGroup[] canvasGroup;
    
    void Start()
    {
        objectManager = ObjectManager.GetInstance();
		myCanvasGroup = this.GetComponentInChildren<CanvasGroup>();
		canvasGroup = GetComponentsInChildren<CanvasGroup>();

		GameObject.Find ("EarthPrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.EarthTurret] + ")";
		GameObject.Find ("FirePrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.FireTurret] + ")";
		GameObject.Find ("StormPrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.StormTurret] + ")";
		GameObject.Find ("VoodooPrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.VoodooTurret] + ")";
    }

    public void Pressed(int turretType)
    {
        objectManager.TurretFactory.TurretType = (TurretType)turretType;

		foreach (CanvasGroup uiSprite in canvasGroup)
        {
            uiSprite.alpha = .7f;
        }

		canvasGroup[turretType].alpha = 1f;        
    }
}