using Assets.Scripts.Turrets;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class TurretFocusMenu : MonoBehaviour
{
	
	private Image upgradeBackground;
	private Image selectedTurretBackground;
	private List<RawImage> upgradeIcons = new List<RawImage>();
	private List<Text> upgradeNames = new List<Text>();
	public Dictionary<string,Texture> IconLookup = new Dictionary<string,Texture>();

	private ObjectManager objectManager;
    private Turret selectedTurret = null;

	private GameObject upgradeMenu;
	private Animator upgradeAnimator;

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
			else
			{
				upgradeAnimator.SetTrigger("Swipe Out");
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
		upgradeAnimator.SetTrigger ("Swipe In");

        // TODO: Change icons / text based on turret type and associated levels.
        upgradeBackground.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .8f);
		selectedTurretBackground.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .8f);
		
        //BoxCollider aButtonCollider = this.GetComponentInChildren<BoxCollider>();
        
        //float slideWidthCheck = aButtonCollider.bounds.size.x * 5.6f;

        //if (Camera.main.WorldToScreenPoint(transform.position).x < slideWidthCheck)
		//	transform.position = new Vector3(transform.position.x + 9, 99, SelectedTurret.transform.position.z - 9);
        //else if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width - slideWidthCheck)
		//	transform.position = new Vector3(transform.position.x - 9, 99, SelectedTurret.transform.position.z + 9);

        switch (SelectedTurret.TurretType)
        {
            case TurretType.EarthTurret:
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.quakeCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.meteorShowerCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.rootBindingCost, selectedTurret.UpgradeThreeLevel)+" ";

				upgradeNames[0].text += "Quake";
				upgradeNames[1].text += "Meteor Shower";
				upgradeNames[2].text += "Root Binding";

				upgradeIcons[0].texture = IconLookup["Quake"];
				upgradeIcons[1].texture = IconLookup["Meteor Shower"];
				upgradeIcons[2].texture = IconLookup["Root Binding"];

				
                break;
            case TurretType.FireTurret:
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.infernoCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.armageddonCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.burnCost, selectedTurret.UpgradeThreeLevel)+" ";

				upgradeNames[0].text += "Inferno";
				upgradeNames[1].text += "Armageddon";
				upgradeNames[2].text += "Burn";

				upgradeIcons[0].texture = IconLookup["Inferno"];
				upgradeIcons[1].texture = IconLookup["Armageddon"];
				upgradeIcons[2].texture = IconLookup["Burn"];

				
                break;
            case TurretType.StormTurret:
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.chainLightningCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.frostCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.lightningStrikeCost, selectedTurret.UpgradeThreeLevel)+" ";

				upgradeNames[0].text += "Chain Lightning";
				upgradeNames[1].text += "Frost";
				upgradeNames[2].text += "Lightning Strike";

				upgradeIcons[0].texture = IconLookup["Chain Lightning"];
				upgradeIcons[1].texture = IconLookup["Frost"];
				upgradeIcons[2].texture = IconLookup["Lightning Strike"];

                break;
            case TurretType.VoodooTurret:
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.poisonCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.mindControlCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.hexCost, selectedTurret.UpgradeThreeLevel)+" ";

				upgradeNames[0].text += "Poison";
				upgradeNames[1].text += "Mind Control";
				upgradeNames[2].text += "Hex";

				upgradeIcons[0].texture = IconLookup["Poison"];
				upgradeIcons[1].texture = IconLookup["Mind Control"];
				upgradeIcons[2].texture = IconLookup["Hex"];

                break;
        }

		if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel == 3)
		{
			upgradeNames[0].text = "";
			upgradeNames[1].text = "";
			upgradeNames[2].text = "";
		}
		
        CorrectUpgradeNumbers();
    }

    private void CorrectUpgradeNumbers()
    {
		upgradeNames[0].text += " "+IntToI(selectedTurret.UpgradeOneLevel);
		upgradeNames[1].text += " "+IntToI(selectedTurret.UpgradeTwoLevel);
		upgradeNames[2].text += " "+IntToI(selectedTurret.UpgradeThreeLevel);
    }

	private string IntToI(int num)
	{
		string str = "";

		for (int x=0; x<num; x++)
			str += "I";

		return str;
	}

    // Runs when entity is Instantiated
    void Awake()
    {
        objectManager = ObjectManager.GetInstance();
    }


	// Use this for initialization
	void Start () {
		upgradeBackground = GameObject.FindGameObjectWithTag(Tags.upgradePanel).GetComponent<Image>();
		selectedTurretBackground = GameObject.FindGameObjectWithTag(Tags.selectedTurretPanel).GetComponent<Image> ();

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tags.upgradeButton))
		{
			upgradeIcons.Add (obj.transform.Find("UpgradeImage").GetComponent<RawImage> ());
			upgradeNames.Add(obj.transform.Find("UpgradeName").GetComponent<Text>());
		}

		LoadIcons ();

		// Turret Upgrade Menu
		upgradeMenu = GameObject.Find ("UpgradeMenu");
		upgradeAnimator = upgradeMenu.GetComponent<Animator>();
	}

	private void LoadIcons()
	{
		IconLookup.Add ("Armageddon", Resources.Load("GUI/Upgrade Icons/Armageddon") as Texture);
		IconLookup.Add ("Burn", Resources.Load("GUI/Upgrade Icons/Burn") as Texture);
		IconLookup.Add ("Inferno", Resources.Load("GUI/Upgrade Icons/Inferno") as Texture);

		IconLookup.Add ("Chain Lightning", Resources.Load("GUI/Upgrade Icons/ChainLightning") as Texture);
		IconLookup.Add ("Frost", Resources.Load("GUI/Upgrade Icons/Frost") as Texture);
		IconLookup.Add ("Lightning Strike", Resources.Load("GUI/Upgrade Icons/LightningStrike") as Texture);

		IconLookup.Add ("Hex", Resources.Load("GUI/Upgrade Icons/Hex") as Texture);
		IconLookup.Add ("Mind Control", Resources.Load("GUI/Upgrade Icons/MindControl") as Texture);
		IconLookup.Add ("Poison", Resources.Load("GUI/Upgrade Icons/Poison") as Texture);

		IconLookup.Add ("Meteor Shower", Resources.Load("GUI/Upgrade Icons/MeteorShower") as Texture);
		IconLookup.Add ("Quake", Resources.Load("GUI/Upgrade Icons/Quake") as Texture);
		IconLookup.Add ("Root Binding", Resources.Load("GUI/Upgrade Icons/RootBinding") as Texture);

	}

    #endregion
}