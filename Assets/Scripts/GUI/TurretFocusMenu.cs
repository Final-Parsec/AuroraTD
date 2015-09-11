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
	private Image selectedTurretImage;
	private List<RawImage> upgradeIcons = new List<RawImage>();
	private List<Text> upgradeNames = new List<Text>();
	private List<Button> upgradeButtons = new List<Button>();
	private Text sellPrice;
	public Dictionary<string,Texture> IconLookup = new Dictionary<string,Texture>();
	
	private ObjectManager objectManager;
	private Turret selectedTurret = null;
	
	private GameObject upgradeMenu;
	private Animator upgradeAnimator;
	
	private GameSpeed lastGameSpeed = GameSpeed.X1;
	
	public Turret SelectedTurret
	{
		get
		{
			return selectedTurret;
		}
		set
		{
			Turret oldSelectedTurret = selectedTurret;
			
			if(selectedTurret != null && value == null)
			{
				upgradeAnimator.SetTrigger("Swipe Out");
				objectManager.gameState.GameSpeed = lastGameSpeed;
			}
			
			selectedTurret = value;
			
			if (oldSelectedTurret != null)
			{
				oldSelectedTurret.Deselect();
			}            
			
			if (value != null)
			{
				value.Select();
				upgradeAnimator.SetTrigger ("Swipe In");
				AttachToTurret();
				
				lastGameSpeed = objectManager.gameState.GameSpeed;
				objectManager.gameState.GameSpeed = GameSpeed.Paused;
			}
		}
	}
	
	
	// Called when the sell button is pressed.
	public void Sell()
	{
		objectManager.GuiButtonMethods.PlaySellSound();
		
		objectManager.gameState.playerMoney += SelectedTurret.Msrp;
		objectManager.Map.UnBlockNode(SelectedTurret.transform.position);
		Destroy(SelectedTurret.gameObject);
		SelectedTurret = null;
	}
	
	public void Upgrade(int upgradeType)
	{
		objectManager.GuiButtonMethods.PlayDefaultSound();
		
		switch (upgradeType) 
		{
		case 0:
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
			break;
			
		case 1:
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
			break;
			
		case 2:
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
			break;
		}
		AttachToTurret();
		
	}
	
	#region Internal methods
	
	private void AttachToTurret()
	{
		selectedTurretImage.sprite = selectedTurret.selectedSprite;
		selectedTurretImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2( 30f, 30f + 15*selectedTurret.Level);
		
		sellPrice.text = "+(" + selectedTurret.Msrp + ")";
		
		bool isMaxLevel = selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel == 3;
		foreach(Button button in upgradeButtons)
			button.interactable = !isMaxLevel;
		
		upgradeBackground.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .8f);
		selectedTurretBackground.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .8f);
		
		upgradeNames[0].text = "";
		upgradeNames[1].text = "";
		upgradeNames[2].text = "";
		
		switch (SelectedTurret.TurretType)
		{
			
		case TurretType.EarthTurret:
			if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel != 3)
			{
				upgradeNames[0].text =  TurretUpgrades.earthUpgrades["Quake"][selectedTurret.UpgradeOneLevel].Cost+" ";
				upgradeNames[1].text =  TurretUpgrades.earthUpgrades["Meteor Shower"][selectedTurret.UpgradeTwoLevel].Cost+" ";
				upgradeNames[2].text =  TurretUpgrades.earthUpgrades["Root Binding"][selectedTurret.UpgradeThreeLevel].Cost+" ";
			}
			
			upgradeNames[0].text += "Quake";
			upgradeNames[1].text += "Meteor Shower";
			upgradeNames[2].text += "Root Binding";
			
			upgradeIcons[0].texture = IconLookup["Quake"];
			upgradeIcons[1].texture = IconLookup["Meteor Shower"];
			upgradeIcons[2].texture = IconLookup["Root Binding"];
			
			
			break;
		case TurretType.FireTurret:
			if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel != 3)
			{
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.infernoCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.armageddonCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.burnCost, selectedTurret.UpgradeThreeLevel)+" ";
			}
			
			upgradeNames[0].text += "Inferno";
			upgradeNames[1].text += "Armageddon";
			upgradeNames[2].text += "Burn";
			
			upgradeIcons[0].texture = IconLookup["Inferno"];
			upgradeIcons[1].texture = IconLookup["Armageddon"];
			upgradeIcons[2].texture = IconLookup["Burn"];
			
			
			break;
		case TurretType.StormTurret:
			if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel != 3)
			{
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.chainLightningCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.frostCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.lightningStrikeCost, selectedTurret.UpgradeThreeLevel)+" ";
			}
			
			upgradeNames[0].text += "Chain Lightning";
			upgradeNames[1].text += "Frost";
			upgradeNames[2].text += "Lightning Strike";
			
			upgradeIcons[0].texture = IconLookup["Chain Lightning"];
			upgradeIcons[1].texture = IconLookup["Frost"];
			upgradeIcons[2].texture = IconLookup["Lightning Strike"];
			
			break;
		case TurretType.VoodooTurret:
			if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel != 3)
			{
				upgradeNames[0].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.poisonCost, selectedTurret.UpgradeOneLevel)+" ";
				upgradeNames[1].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.mindControlCost, selectedTurret.UpgradeTwoLevel)+" ";
				upgradeNames[2].text =  TurretUpgrades.GetUpgradeCost(TurretUpgrades.hexCost, selectedTurret.UpgradeThreeLevel)+" ";
			}
			
			upgradeNames[0].text += "Poison";
			upgradeNames[1].text += "Mind Control";
			upgradeNames[2].text += "Hex";
			
			upgradeIcons[0].texture = IconLookup["Poison"];
			upgradeIcons[1].texture = IconLookup["Mind Control"];
			upgradeIcons[2].texture = IconLookup["Hex"];
			
			break;
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
		TurretUpgrades.MakeUpgrades ();

		upgradeBackground = GameObject.FindGameObjectWithTag(Tags.upgradePanel).GetComponent<Image>();
		selectedTurretBackground = GameObject.FindGameObjectWithTag(Tags.selectedTurretPanel).GetComponent<Image> ();
		selectedTurretImage = GameObject.FindGameObjectWithTag(Tags.selectedTurretPanel).transform.FindChild("SelectedImage").GetComponent<Image> ();
		
		int upgradeType = 0;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tags.upgradeButton))
		{
			// The struggle
			int upTypeReal = upgradeType;
			
			upgradeIcons.Add (obj.transform.Find("UpgradeImage").GetComponent<RawImage> ());
			upgradeNames.Add(obj.transform.Find("UpgradeName").GetComponent<Text>());
			upgradeButtons.Add(obj.GetComponent<Button>());
			
			UnityEngine.Events.UnityAction action1 = () => { Upgrade(upTypeReal); };
			obj.GetComponent<Button>().onClick.AddListener(action1);
			upgradeType++;
		}
		
		LoadIcons ();
		
		// Turret Upgrade Menu
		upgradeMenu = GameObject.Find ("UpgradeMenu");
		upgradeAnimator = upgradeMenu.GetComponent<Animator>();
		
		
		sellPrice = GameObject.Find ("CashBack").GetComponent<Text> ();
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