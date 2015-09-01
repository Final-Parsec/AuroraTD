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

	private GameObject optionScreen;
	private Animator optionAnimator;

	private GameObject gameOverScreen;
	private Animator gameOverAnimator;
	private Text condition;
	private Text score;

	private GameObject submitScoreScreen;
	private Animator submitScoreAnimator;

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
		optionScreen = GameObject.Find ("OptionsScreen");
		optionAnimator = optionScreen.GetComponent<Animator>();
		optionScreen.SetActive (false);

		// Game Over Screen
		gameOverScreen = GameObject.Find ("GameOverScreen");
		gameOverAnimator = gameOverScreen.GetComponent<Animator>();
		condition = GameObject.Find ("GameOverCondition").GetComponent<Text> ();
		score = GameObject.Find ("GameOverScore").GetComponent<Text> ();
		gameOverScreen.SetActive (false);

		// Submit Score Screen
		submitScoreScreen = GameObject.Find ("SubmitScoreScreen");
		submitScoreAnimator = submitScoreScreen.GetComponent<Animator>();
		submitScoreScreen.SetActive (false);

	}
	
	void Update()
	{
		sendWaveTime.text = objectManager.gameState.nextWaveCountDown.ToString();
		moneyValue.text = objectManager.gameState.playerMoney.ToString();
		healthValue.text = objectManager.gameState.PlayerHealth.ToString();

		if(objectManager.Map.upcomingWaves.Count == 0 &&
		   objectManager.enemies.Count == 0 &&
		   objectManager.gameState.PlayerHealth > 0 &&
		   !objectManager.gameState.gameOver)
		{
			
			EndGame("Victory!");
		}
		else if(objectManager.gameState.PlayerHealth <= 0 && !objectManager.gameState.gameOver)
		{
			EndGame("Defeat!");
		}
	}

	private void EndGame(string conditionText)
	{
		objectManager.Map.SetGrid(false);
		
		gameOverScreen.SetActive (true);
		gameOverAnimator.SetTrigger("Fade In");
		condition.text = conditionText;
		score.text = "Score: " + objectManager.gameState.score;
		
		objectManager.gameState.gameOver = true;
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
		objectManager.gameState.optionsOn = !optionScreen.activeSelf;
		if(!optionScreen.activeSelf){
			optionScreen.SetActive (true);
			optionAnimator.SetTrigger("Fade In");
		}else {
			optionAnimator.SetTrigger("Fade Out");
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

	public void HighScorePressed()
	{
		gameOverScreen.SetActive (false);

		submitScoreScreen.SetActive (true);
		submitScoreAnimator.SetTrigger("Fade In");
	}
}