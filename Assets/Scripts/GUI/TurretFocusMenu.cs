using Assets.Scripts.Turrets;
using UnityEngine;

public class TurretFocusMenu : MonoBehaviour
{
	
	private UISprite background;
	private ObjectManager objectManager;
    private Turret selectedTurret = null;
    private UISprite upgradeOneIcon;
    private UISprite upgradeTwoIcon;
    private UISprite upgradeThreeIcon;
    private UILabel upgradeOneLabel;
    private UILabel upgradeTwoLabel;
    private UILabel upgradeThreeLabel;

	public UILabel[] prices = new UILabel[3];

    public Turret SelectedTurret
    {
        get
        {
            return selectedTurret;
        }
        set
        {
            Turret oldSelectedTurret = selectedTurret;
            selectedTurret = value;

            if (oldSelectedTurret != null)
            {
                oldSelectedTurret.Deselect();
            }            

            if (value != null)
            {
                value.Select();
                AttachToTurret();
            }
        }
    }


	// Called when the sell button is pressed.
	public void Sell()
    {
        objectManager.gameState.playerMoney += SelectedTurret.Msrp;
        objectManager.Map.UnBlockNode(SelectedTurret.transform.position);
        Destroy(SelectedTurret.gameObject);
        SelectedTurret = null;
		transform.position = new Vector3(transform.position.x, transform.position.y - 1000, transform.position.z);
	}

    public void UpgradeOne()
    {
        switch(SelectedTurret.TurretType)
        {
            case TurretType.EarthTurret:
                SelectedTurret.QuakeUpgrade();
                break;
            case TurretType.FireTurret:
                selectedTurret.InfernoUpgrade();
                break;
            case TurretType.StormTurret:
                selectedTurret.ChainLightningUpgrade();
                break;
            case TurretType.VoodooTurret:
                selectedTurret.PoisonUpgrade();
                break;
        }

		AttachToTurret();
    }

    public void UpgradeTwo()
    {
        switch (SelectedTurret.TurretType)
        {
            case TurretType.EarthTurret:
                SelectedTurret.MeteorShower();
                break;
            case TurretType.FireTurret:
                selectedTurret.ArmageddonUpgrade();
                break;
            case TurretType.StormTurret:
                selectedTurret.FrostUpgrade();
                break;
            case TurretType.VoodooTurret:
                selectedTurret.MindControlUpgrade();
                break;
        }

		AttachToTurret();
    }

    public void UpgradeThree()
    {
        switch (SelectedTurret.TurretType)
        {
            case TurretType.EarthTurret:
                SelectedTurret.RootBindingUpgrade();
                break;
            case TurretType.FireTurret:
                selectedTurret.BurnUpgrade();
                break;
            case TurretType.StormTurret:
                selectedTurret.LightningStrikeUpgrade();
                break;
            case TurretType.VoodooTurret:
                selectedTurret.HexUpgrade();
                break;
        }

		AttachToTurret();
    }

    #region Internal methods

    private void AttachToTurret()
    {
        // TODO: Change icons / text based on turret type and associated levels.
        background.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .5f);
        transform.position = new Vector3(SelectedTurret.transform.position.x, 99, SelectedTurret.transform.position.z);

        BoxCollider aButtonCollider = this.GetComponentInChildren<BoxCollider>();
        
        float slideWidthCheck = aButtonCollider.bounds.size.x * 5.6f;

        if (Camera.main.WorldToScreenPoint(transform.position).x < slideWidthCheck)
			transform.position = new Vector3(transform.position.x + 9, 99, SelectedTurret.transform.position.z - 9);
        else if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - slideWidthCheck)
			transform.position = new Vector3(transform.position.x - 9, 99, SelectedTurret.transform.position.z + 9);

        switch (SelectedTurret.TurretType)
        {
            case TurretType.EarthTurret:
                upgradeOneIcon.spriteName = "Quake";
                upgradeTwoIcon.spriteName = "MeteorShower";
                upgradeThreeIcon.spriteName = "RootBinding";

				prices[0].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.quakeCost, selectedTurret.UpgradeOneLevel);
				prices[1].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.meteorShowerCost, selectedTurret.UpgradeTwoLevel);
				prices[2].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.rootBindingCost, selectedTurret.UpgradeThreeLevel);

                break;
            case TurretType.FireTurret:
                upgradeOneIcon.spriteName = "Inferno";
                upgradeTwoIcon.spriteName = "Armageddon";
                upgradeThreeIcon.spriteName = "Burn";

				prices[0].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.infernoCost, selectedTurret.UpgradeOneLevel);
				prices[1].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.armageddonCost, selectedTurret.UpgradeTwoLevel);
				prices[2].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.burnCost, selectedTurret.UpgradeThreeLevel);
                break;
            case TurretType.StormTurret:
                upgradeOneIcon.spriteName = "ChainLightning";
                upgradeTwoIcon.spriteName = "Frost";
                upgradeThreeIcon.spriteName = "LightningStrike";

				prices[0].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.chainLightningCost, selectedTurret.UpgradeOneLevel);
				prices[1].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.frostCost, selectedTurret.UpgradeTwoLevel);
				prices[2].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.lightningStrikeCost, selectedTurret.UpgradeThreeLevel);
                break;
            case TurretType.VoodooTurret:
                upgradeOneIcon.spriteName = "Poison";
                upgradeTwoIcon.spriteName = "MindControl";
                upgradeThreeIcon.spriteName = "Hex";

				prices[0].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.poisonCost, selectedTurret.UpgradeOneLevel);
				prices[1].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.mindControlCost, selectedTurret.UpgradeTwoLevel);
				prices[2].text = TurretUpgrades.GetUpgradeCost(TurretUpgrades.hexCost, selectedTurret.UpgradeThreeLevel);
                break;
        }

		if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel == 3)
		{
			prices[0].text = "";
			prices[1].text = "";
			prices[2].text = "";
		}
		
        CorrectUpgradeNumbers();
    }

    private void CorrectUpgradeNumbers()
    {
        upgradeOneLabel.text = selectedTurret.UpgradeOneLevel.ToString();
        upgradeTwoLabel.text = selectedTurret.UpgradeTwoLevel.ToString();
        upgradeThreeLabel.text = selectedTurret.UpgradeThreeLevel.ToString();
    }

    // Runs when entity is Instantiated
    void Awake()
    {
        objectManager = ObjectManager.GetInstance();
    }


	// Use this for initialization
	void Start () {
		background = GameObject.Find("Background").GetComponent<UISprite>();
        upgradeOneIcon = GameObject.Find("UpgradeOneIcon").GetComponent<UISprite>();
        upgradeTwoIcon = GameObject.Find("UpgradeTwoIcon").GetComponent<UISprite>();
        upgradeThreeIcon = GameObject.Find("UpgradeThreeIcon").GetComponent<UISprite>();
        upgradeOneLabel = GameObject.Find("UpgradeOneLabel").GetComponent<UILabel>();
        upgradeTwoLabel = GameObject.Find("UpgradeTwoLabel").GetComponent<UILabel>();
        upgradeThreeLabel = GameObject.Find("UpgradeThreeLabel").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    #endregion
}