using UnityEngine;
using System.Collections;
using System.Linq;

public class TurretSelectionButton : MonoBehaviour
{
    public TurretType turretType;
	public UILabel cost;

    private ObjectManager objectManager;
    private UISprite myUiSprite;
    private UISprite[] uiSprites;
    
    void Start()
    {
        objectManager = ObjectManager.GetInstance();
        myUiSprite = this.GetComponentInChildren<UISprite>();
        uiSprites = (from button in FindObjectsOfType<TurretSelectionButton>()
                     select button.GetComponentInChildren<UISprite>()).ToArray();

		cost.text = "(" + objectManager.TurretFactory.turretCosts[(int)turretType] + ")";
    }

    void OnClick()
    {
        objectManager.TurretFactory.TurretType = turretType;

        foreach (UISprite uiSprite in uiSprites)
        {
            uiSprite.alpha = .4f;
        }

        myUiSprite.alpha = 1f;        
    }
}