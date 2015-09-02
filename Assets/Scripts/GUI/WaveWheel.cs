using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveWheel : MonoBehaviour {

	private RawImage waveA;
	private RawImage waveB;
	private RawImage waveC;

	private Texture nextWaveTexture;
	private string nextWaveLevel = "";

	private Texture[] waveTextures = new Texture[5];
	private GameObject[] waveImages = new GameObject[3];

	private bool updateSpriteImages;
	public bool UpdateSpriteImages{
		get {
			return updateSpriteImages;
		}
		set {
			updateSpriteImages = value;
			nextWaveLevel = ""+(_ObjectManager.gameState.waveCount+3);
			if(_ObjectManager.Map.upcomingWaves.Count >= 3 && _ObjectManager.Map.upcomingWaves[2].enemyType != EnemyType.Max){
				nextWaveTexture = GetTexture((int)_ObjectManager.Map.upcomingWaves[2].enemyType);

			}
			else if(_ObjectManager.Map.upcomingWaves.Count >= 3){
				nextWaveTexture = GetTexture((int)_ObjectManager.Map.upcomingWaves[2].bossType);
			}
			else{
				nextWaveTexture = GetTexture(-1);
				nextWaveLevel = ""+000;
			}
		}
	}

	private ObjectManager _ObjectManager;


	// Use this for initialization
	void Start () {
		_ObjectManager = ObjectManager.GetInstance ();

		waveTextures[0] = Resources.Load ("GUI/Wave Images/Blue") as Texture;
		waveTextures[1] = Resources.Load ("GUI/Wave Images/Red") as Texture;
		waveTextures[2] = Resources.Load ("GUI/Wave Images/Purple") as Texture;
		waveTextures[3] = Resources.Load ("GUI/Wave Images/Green") as Texture;
		waveTextures[4] = Resources.Load ("GUI/Wave Images/Black") as Texture;

		GameObject waveA = GameObject.Find ("WaveA");
		GameObject waveB = GameObject.Find ("WaveB");
		GameObject waveC = GameObject.Find ("WaveC");

		// Initial wave images
		waveB.GetComponent<RawImage>().texture = GetTexture((int)_ObjectManager.Map.upcomingWaves[0].enemyType);
		waveB.transform.FindChild("Number").GetComponent<Text>().text = ""+(_ObjectManager.gameState.waveCount+1);
		waveB.transform.FindChild("Name").GetComponent<Text>().text = GetWaveName(_ObjectManager.Map.upcomingWaves[0], waveB);

		waveC.GetComponent<RawImage>().texture = GetTexture((int)_ObjectManager.Map.upcomingWaves[1].enemyType);
		waveC.transform.FindChild("Number").GetComponent<Text>().text = ""+(_ObjectManager.gameState.waveCount+2);
		waveC.transform.FindChild("Name").GetComponent<Text>().text = GetWaveName(_ObjectManager.Map.upcomingWaves[1], waveC);

		waveImages[0] = waveA;
		waveImages[1] = waveB;
		waveImages[2] = waveC;
	}
	
	// Update is called once per frame
	void Update () {

		if (_ObjectManager.gameState.gameOver)
			return;

		float distanceRatio = -1 * (_ObjectManager.gameState.nextWaveCountDown+1) /_ObjectManager.Map.waveSpawnDelay;

		waveImages[0].transform.rotation = Quaternion.Euler(0,
		                                                	0,
		                                                    45 + 45*distanceRatio);

		waveImages[1].transform.rotation = Quaternion.Euler(0,
		                                                    0,
		                                                    0 + 45*distanceRatio);

		waveImages[2].transform.rotation = Quaternion.Euler(0,
		                                                    0,
		                                                    -45 + 45*distanceRatio);

		ChangeSpriteImages();
	}

	private void ChangeSpriteImages(){
		if(updateSpriteImages){
			updateSpriteImages = false;
			
			GameObject temp;
			temp = waveImages[0];
			waveImages[0] = waveImages[1];
			waveImages[1] = waveImages[2];
			waveImages[2] = temp;
			
			waveImages[2].GetComponent<RawImage>().texture = nextWaveTexture;
			waveImages[2].transform.FindChild("Number").GetComponent<Text>().text = nextWaveLevel;
			waveImages[2].transform.FindChild("Name").GetComponent<Text>().text = GetWaveName(_ObjectManager.Map.upcomingWaves[1], waveImages[2]);
		}
	}

	private Texture GetTexture(int enemyType)
	{
		if(enemyType == 0 || enemyType == 5)
			return waveTextures[0]; // storm

		if(enemyType == 3 || enemyType == 4)
			return waveTextures[1]; // fire

		if(enemyType == 1)
			return waveTextures[2]; // storm

		if(enemyType == 2)
			return waveTextures[3]; // storm

		return waveTextures[4]; // game over
	}

	private string GetWaveName(Wave wave, GameObject waveX)
	{
		int waveType = (int)wave.enemyType;
		Text bossDisplay = waveX.transform.FindChild ("BossName").GetComponent<Text> ();

		if (wave.enemyType == EnemyType.Max) 
		{
			waveType = (int)wave.bossType;
			bossDisplay.color = new Color (bossDisplay.color.r,
			                              bossDisplay.color.g,
			                              bossDisplay.color.b,
			                              1f);
		} 
		else 
		{
			bossDisplay.color = new Color(bossDisplay.color.r,
			                              bossDisplay.color.g,
			                              bossDisplay.color.b,
			                              0f);
		}

		if(waveType == 0 || waveType == 1)
			return "Fast"; // fast
		
		if(waveType == 2 || waveType == 3)
			return "Strong"; // strong
		
		if(waveType == 4 || waveType == 5)
			return "Spawner"; // spawner
		
		return "Game Over";
	}
}
