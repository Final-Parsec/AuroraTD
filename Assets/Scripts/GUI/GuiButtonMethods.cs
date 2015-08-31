using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class GuiButtonMethods : MonoBehaviour
{
    private ObjectManager objectManager;
	private CanvasGroup[] canvasGroups;

	private Text sendWaveTime;
	private Text sendWaveName;
	private Text healthValue;
	private Text moneyValue;

	private GameObject OptionScreen;
	private Animator OptionAnimator;

	private bool gridToggle = true;
    
    void Start()
    {
		objectManager = ObjectManager.GetInstance();
		
		// Turret Selection Buttons
		GameObject turretButtonPanel = GameObject.Find ("TurretSelectPanel");
		canvasGroups = turretButtonPanel.GetComponentsInChildren<CanvasGroup>();
		canvasGroups [0].alpha = 1f;

		GameObject.Find ("EarthPrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.EarthTurret] + ")";
		GameObject.Find ("FirePrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.FireTurret] + ")";
		GameObject.Find ("StormPrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.StormTurret] + ")";
		GameObject.Find ("VoodooPrice").GetComponent<Text>().text = "(" + objectManager.TurretFactory.turretCosts[(int)TurretType.VoodooTurret] + ")";
    
		// Send Wave Button
		sendWaveName = GameObject.Find ("SendWaveName").GetComponent<Text>();
		sendWaveTime = GameObject.Find ("SendWaveTime").GetComponent<Text>();
		sendWaveName.text = "Start Game";
		sendWaveTime.text = objectManager.gameState.nextWaveCountDown.ToString();

		//Money And Health
		healthValue = GameObject.Find ("HealthValue").GetComponent<Text>();
		moneyValue = GameObject.Find ("MoneyValue").GetComponent<Text>();

		// Option Screen
		OptionScreen = GameObject.Find ("OptionsScreen");
		OptionAnimator = OptionScreen.GetComponent<Animator>();
		OptionScreen.SetActive (false);
	
	}
	
	void Update()
	{
		sendWaveTime.text = objectManager.gameState.nextWaveCountDown.ToString();
		moneyValue.text = objectManager.gameState.playerMoney.ToString();
		healthValue.text = objectManager.gameState.PlayerHealth.ToString();
	}

    public void TurretButtonPressed(int turretType)
    {
        objectManager.TurretFactory.TurretType = (TurretType)turretType;

		foreach (CanvasGroup uiSprite in canvasGroups)
        {
            uiSprite.alpha = .7f;
        }

		canvasGroups[turretType].alpha = 1f;        
    }

	public void SendWavePressed()
	{
		if (!objectManager.gameState.gameStarted)
		{
			
			Animator anim = GameObject.Find ("SendWave").GetComponent<Animator>();
			anim.SetBool("StartGame", true);
			anim.speed = 20;

			sendWaveName.text = "Send Wave";
			objectManager.gameState.gameStarted = true;

		}

		if (objectManager.Map.currentWaves.Count < 5)
		{
			objectManager.Map.playerTriggeredWave = true;
			
			// only if you actually sent a wave and it isn't just almost the end of the game
			if(objectManager.Map.upcomingWaves.Count > 0 && !objectManager.gameState.gameOver)
			{
				objectManager.gameState.playerMoney += objectManager.gameState.nextWaveCountDown;
				objectManager.gameState.score += objectManager.gameState.nextWaveCountDown;
			}
		}
	}

	public void OptionPressed()
	{

		if ((objectManager.Map.upcomingWaves.Count == 0 &&
		     objectManager.enemies.Count == 0 &&
		     objectManager.gameState.PlayerHealth > 0) ||
		     objectManager.gameState.PlayerHealth <= 0)
		{
			return;
		}
		objectManager.gameState.optionsOn = !OptionScreen.activeSelf;
		if(!OptionScreen.activeSelf){
			OptionScreen.SetActive (true);
			OptionAnimator.SetTrigger("Fade In");
		}else {
			OptionAnimator.SetTrigger("Fade Out");
		}
	}

	public void MutePressed()
	{
		var objectManager = ObjectManager.GetInstance();
		objectManager.gameState.isMuted = !objectManager.gameState.isMuted;
	}

	public void DisplayGridPressed()
	{
		gridToggle = !gridToggle;
		objectManager.Map.SetGrid(gridToggle);
	}

	public void QuitPressed()
	{
		Application.Quit ();
	}

	public void MainMenuPressed()
	{
		objectManager.DestroySinglton();
		Application.LoadLevel("Main Menu");
	}
}