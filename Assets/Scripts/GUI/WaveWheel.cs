using UnityEngine;
using System.Collections;

public class WaveWheel : MonoBehaviour {

	public GameObject waveAHolder;
	public GameObject waveBHolder;
	public GameObject waveCHolder;

	private GameObject[] waveImages = new GameObject[3];

	private string nextWaveImage = "";
	private string nextWaveLevel = "";

	private bool updateSpriteImages;
	public bool UpdateSpriteImages{
		get {
			return updateSpriteImages;
		}
		set {
			updateSpriteImages = value;
			nextWaveLevel = ""+(_ObjectManager.gameState.waveCount+3);
			if(_ObjectManager.Map.upcomingWaves.Count >= 3 && _ObjectManager.Map.upcomingWaves[2].enemyType != EnemyType.Max){
				nextWaveImage = _ObjectManager.Map.upcomingWaves[2].enemyType.ToString();

			}
			else if(_ObjectManager.Map.upcomingWaves.Count >= 3){
				nextWaveImage = _ObjectManager.Map.upcomingWaves[2].bossType.ToString();
			}
			else{
				nextWaveImage = "GameOver";
				nextWaveLevel = ""+000;
			}
		}
	}

	private ObjectManager _ObjectManager;


	// Use this for initialization
	void Start () {
		_ObjectManager = ObjectManager.GetInstance ();

		// Initial wave images
		(waveBHolder.GetComponentInChildren<UISprite>() as UISprite).spriteName = _ObjectManager.Map.upcomingWaves[0].enemyType.ToString();
		(waveBHolder.GetComponentInChildren<UILabel>() as UILabel).text = ""+(_ObjectManager.gameState.waveCount+1);

		(waveCHolder.GetComponentInChildren<UISprite>() as UISprite).spriteName = _ObjectManager.Map.upcomingWaves[1].enemyType.ToString();
		(waveCHolder.GetComponentInChildren<UILabel>() as UILabel).text = ""+(_ObjectManager.gameState.waveCount+2);

		waveImages[0] = waveAHolder;
		waveImages[1] = waveBHolder;
		waveImages[2] = waveCHolder;
	}
	
	// Update is called once per frame
	void Update () {

		float distanceRatio = (_ObjectManager.gameState.nextWaveCountDown+1) /_ObjectManager.Map.waveSpawnDelay;

		waveImages[0].transform.rotation = Quaternion.Euler(0,
		                                                	0 + 45*distanceRatio,
		                                                	0);

		waveImages[1].transform.rotation = Quaternion.Euler(0,
		                                                    45 + 45*distanceRatio,
		                                                    0);

		waveImages[2].transform.rotation = Quaternion.Euler(0,
		                                                    90 + 45*distanceRatio,
		                                                    0);

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
			
			(waveImages[2].GetComponentInChildren<UISprite>() as UISprite).spriteName = nextWaveImage;
			(waveImages[2].GetComponentInChildren<UILabel>() as UILabel).text = nextWaveLevel;
		}
	}
}
