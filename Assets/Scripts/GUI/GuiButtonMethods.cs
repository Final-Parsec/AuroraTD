using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;

[RequireComponent(typeof(AudioSource))]
public class GuiButtonMethods : MonoBehaviour
{
	private const string Url = "http://finalparsec.com/scores/";

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

	private GameObject highScoreScreen;
	private Animator highScoreAnimator;

	private GameObject upgradeMenu;
	private Animator upgradeAnimator;

	private AudioSource audioSource;
	private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

	private bool gridToggle = true;
    
    void Start()
    {
		objectManager = ObjectManager.GetInstance();
		audioSource = GetComponent<AudioSource>();
		// Load Audio Clips
		audioClips.Add("defaultPress" , Resources.Load("GUI/Sounds/ButtonPress") as AudioClip);
		audioClips.Add("sellPress", Resources.Load("GUI/Sounds/SellPress") as AudioClip);
		
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

		// High Score Screen
		highScoreScreen = GameObject.Find ("HighScoreScreen");
		highScoreAnimator = highScoreScreen.GetComponent<Animator>();
		highScoreScreen.SetActive (false);

		// Turret Upgrade Menu
		upgradeMenu = GameObject.Find ("UpgradeMenu");
		upgradeAnimator = upgradeMenu.GetComponent<Animator>();


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
		PlayDefaultSound();
        objectManager.TurretFactory.TurretType = (TurretType)turretType;

		foreach (CanvasGroup uiSprite in canvasGroups)
        {
            uiSprite.alpha = .7f;
        }

		canvasGroups[turretType].alpha = 1f;   
    }

	public void SendWavePressed()
	{
		PlayDefaultSound();
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
		PlayDefaultSound();

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
		PlayDefaultSound();
		var objectManager = ObjectManager.GetInstance();
		objectManager.gameState.isMuted = !objectManager.gameState.isMuted;
	}

	public void DisplayGridPressed()
	{
		PlayDefaultSound();
		gridToggle = !gridToggle;
		objectManager.Map.SetGrid(gridToggle);
	}

	public void QuitPressed()
	{
		PlayDefaultSound();
		Application.Quit ();
	}

	public void MainMenuPressed()
	{
		PlayDefaultSound();
		objectManager.DestroySinglton();
		Application.LoadLevel("Main Menu");
	}

	public void HighScorePressed()
	{
		PlayDefaultSound();
		gameOverScreen.SetActive (false);

		submitScoreScreen.SetActive (true);
		submitScoreAnimator.SetTrigger("Fade In");
	}

	public void SubmitScorePressed()
	{
		PlayDefaultSound();

		if (string.IsNullOrEmpty(GameObject.Find("PlayerNameTextField").GetComponent<Text>().text))
		{
			return;
		}

		submitScoreAnimator.SetTrigger ("Fade Out");

		highScoreScreen.SetActive (true);
		highScoreAnimator.SetTrigger("Fade In");
		
		this.StartCoroutine(this.SendRequest());
		Text yourScoreLabel = GameObject.Find("HighScoreScore").GetComponent<Text>();
		yourScoreLabel.text = "Your Score: " + ObjectManager.GetInstance().gameState.score.ToString();

	}

	private IEnumerator SendRequest()
	{
		Text nameList = GameObject.Find ("HighScoreNames").GetComponent<Text>();
		Text scoreList = GameObject.Find ("HighScoreScores").GetComponent<Text>();

		string playerName = GameObject.Find("PlayerNameTextField").GetComponent<Text>().text;
		int score = ObjectManager.GetInstance().gameState.score;
		
		string leaderboardName = string.Format(
			"Aurora TD {0} {1} {2}",
			ObjectManager.GetInstance().gameState.MapType,
			ObjectManager.GetInstance().gameState.friendlyDifficulty,
			ObjectManager.GetInstance().gameState.numberOfWaves == 300
			? "Endless"
			: ObjectManager.GetInstance().gameState.numberOfWaves.ToString());
		
		string modifiedUrl = Url + string.Format("{0}?limit={1}&player_name={2}&score={3}", leaderboardName, 10, playerName, score);
		modifiedUrl = modifiedUrl.Replace(" ", "%20");
		Debug.Log(modifiedUrl);
		
		WWW www = new WWW(modifiedUrl);
		
		yield return www;
		
		JSONNode jsonNode = JSON.Parse(www.text);
		
		nameList.text = string.Empty;
		scoreList.text = string.Empty;
		int position = 1;
		for (int x = 0; x < jsonNode["competitors"].Count; x++)
		{
			//format list of names with numbers and names
			nameList.text += position + ":\t" + jsonNode["competitors"][x]["player_name"].Value + "\n";
			
			//dump list of scores
			scoreList.text += jsonNode["competitors"][x]["score"].AsInt + "\n";

			position++;
		}
	}

	public void UpgradeMenuBackPressed()
	{
		PlayDefaultSound();
		objectManager.TurretFocusMenu.SelectedTurret = null;
	}

	public void SpeedUp()
	{
		PlayDefaultSound();
		switch(objectManager.gameState.GameSpeed)
		{
		case GameSpeed.Paused:
			objectManager.gameState.GameSpeed = GameSpeed.X1;
			break;

		case GameSpeed.X1:
			objectManager.gameState.GameSpeed = GameSpeed.X2;
			break;

		case GameSpeed.X2:
			objectManager.gameState.GameSpeed = GameSpeed.X3;
			break;
		}
	}

	public void SlowDown()
	{
		PlayDefaultSound();
		switch(objectManager.gameState.GameSpeed)
		{
		case GameSpeed.X1:
			objectManager.gameState.GameSpeed = GameSpeed.Paused;
			break;
			
		case GameSpeed.X2:
			objectManager.gameState.GameSpeed = GameSpeed.X1;
			break;

		case GameSpeed.X3:
			objectManager.gameState.GameSpeed = GameSpeed.X2;
			break;
		}
	}

	public void PlaySellSound()
	{
		if(!objectManager.gameState.isMuted)
			audioSource.PlayOneShot(audioClips["sellPress"]);
	}

	public void PlayDefaultSound()
	{
		if(!objectManager.gameState.isMuted)
			audioSource.PlayOneShot(audioClips["defaultPress"]);
	}
}