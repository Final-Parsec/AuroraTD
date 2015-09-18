using Assets.Scripts.Turrets;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UpgradeButton
{
	public RawImage image;
	public Text upgradeName;
	public Text description;
	public Text stats;
	public Button button;

	public UpgradeButton(Button button, Text upgradeName, Text description, Text stats, RawImage image)
	{
		this.button = button;
		this.upgradeName = upgradeName;
		this.description = description;
		this.stats = stats;
		this.image = image;
	}
}

public class TurretFocusMenu : MonoBehaviour
{
	private Image upgradeBackground;
	private Image selectedTurretBackground;
	private Image selectedTurretImage;
	private Text selectedTurretStats;

	private List<UpgradeButton> upgradeButtons = new List<UpgradeButton>();

	private Text sellPrice;
	public Dictionary<string,Texture> IconLookup = new Dictionary<string,Texture>();
	
	private ObjectManager objectManager;
	private Turret selectedTurret = null;
	
	private GameObject upgradeMenu;
	private Animator upgradeAnimator;
	
	private GameSpeed lastGameSpeed = GameSpeed.X1;

	public bool isActive = false;
	
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
				isActive = false;
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
				isActive = true;
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
		foreach(UpgradeButton button in upgradeButtons)
			button.button.interactable = !isMaxLevel;
		
		upgradeBackground.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .8f);
		selectedTurretBackground.color = new Color(SelectedTurret.focusMenuRed, SelectedTurret.focusMenuGreen, SelectedTurret.focusMenuBlue, .8f);

		selectedTurretStats.text = selectedTurret.GetStats ();
		
		upgradeButtons[0].upgradeName.text = "";
		upgradeButtons[1].upgradeName.text = "";
		upgradeButtons[2].upgradeName.text = "";

		upgradeButtons[0].description.text = "";
		upgradeButtons[1].description.text = "";
		upgradeButtons[2].description.text = "";

		upgradeButtons[0].stats.text = "";
		upgradeButtons[1].stats.text = "";
		upgradeButtons[2].stats.text = "";
		
		switch (SelectedTurret.TurretType)
		{
			
		case TurretType.EarthTurret:
			UpdateUpgradeButton("Quake", "Meteor Shower", "Root Binding");
			break;
		case TurretType.FireTurret:
			UpdateUpgradeButton("Inferno", "Armageddon", "Burn");			
			break;
		case TurretType.StormTurret:
			UpdateUpgradeButton("Chain Lightning", "Frost", "Lightning Strike");			
			break;
		case TurretType.VoodooTurret:
			UpdateUpgradeButton("Poison", "Mind Control", "Hex");
			break;
		}
		
		
		CorrectUpgradeNumbers();
	}

	private void UpdateUpgradeButton(string upgrade1, string upgrade2, string upgrade3)
	{
		if (selectedTurret.UpgradeOneLevel + selectedTurret.UpgradeTwoLevel + selectedTurret.UpgradeThreeLevel != 3)
		{
			upgradeButtons[0].upgradeName.text =  TurretUpgrades.upgrades[upgrade1][selectedTurret.UpgradeOneLevel].Cost+" ";
			upgradeButtons[1].upgradeName.text =  TurretUpgrades.upgrades[upgrade2][selectedTurret.UpgradeTwoLevel].Cost+" ";
			upgradeButtons[2].upgradeName.text =  TurretUpgrades.upgrades[upgrade3][selectedTurret.UpgradeThreeLevel].Cost+" ";

			upgradeButtons[0].description.text =  TurretUpgrades.upgrades[upgrade1][selectedTurret.UpgradeOneLevel].Description;
			upgradeButtons[1].description.text =  TurretUpgrades.upgrades[upgrade2][selectedTurret.UpgradeTwoLevel].Description;
			upgradeButtons[2].description.text =  TurretUpgrades.upgrades[upgrade3][selectedTurret.UpgradeThreeLevel].Description;
			
			upgradeButtons[0].stats.text =  TurretUpgrades.upgrades[upgrade1][selectedTurret.UpgradeOneLevel].GetPrettyStats();
			upgradeButtons[1].stats.text =  TurretUpgrades.upgrades[upgrade2][selectedTurret.UpgradeTwoLevel].GetPrettyStats();
			upgradeButtons[2].stats.text =  TurretUpgrades.upgrades[upgrade3][selectedTurret.UpgradeThreeLevel].GetPrettyStats();
		}
		
		upgradeButtons[0].upgradeName.text += upgrade1;
		upgradeButtons[1].upgradeName.text += upgrade2;
		upgradeButtons[2].upgradeName.text += upgrade3;
		
		upgradeButtons[0].image.texture = IconLookup[upgrade1];
		upgradeButtons[1].image.texture = IconLookup[upgrade2];
		upgradeButtons[2].image.texture = IconLookup[upgrade3];
		

	}
	
	private void CorrectUpgradeNumbers()
	{
		upgradeButtons[0].upgradeName.text += " "+IntToI(selectedTurret.UpgradeOneLevel);
		upgradeButtons[1].upgradeName.text += " "+IntToI(selectedTurret.UpgradeTwoLevel);
		upgradeButtons[2].upgradeName.text += " "+IntToI(selectedTurret.UpgradeThreeLevel);
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
		selectedTurretStats = GameObject.FindGameObjectWithTag (Tags.selectedTurretPanel).transform.Find ("TurretStats").GetComponent<Text> ();
		
		int upgradeType = 0;
		foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tags.upgradeButton))
		{
			// The struggle
			int upTypeReal = upgradeType;

			UpgradeButton upgradeButton = new UpgradeButton(obj.GetComponent<Button>(),obj.transform.Find("UpgradeName").GetComponent<Text>(),
			                                                obj.transform.Find("UpgradeDescription").GetComponent<Text>(),
			                                                obj.transform.Find("UpgradeStats").GetComponent<Text>(),
			                                                obj.transform.Find("UpgradeImage").GetComponent<RawImage> ());

			upgradeButtons.Add(upgradeButton);

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