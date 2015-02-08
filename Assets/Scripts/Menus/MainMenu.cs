using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	MenuCameraLogic _camera;
	public GUITexture loadScreen;
	public GUITexture loadIcon;

	public GameState gameState =  new GameState();

	int menuNum;

	// Use this for initialization
	void Start () {
		_camera = GameObject.Find("Main Camera").GetComponent<MenuCameraLogic>();
		menuNum = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && menuNum == 0) {
			GoToMainMenu ();
		}

        // Escape (Back button on Android)
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
	
	public void GoToMainMenu(){
		_camera.setTargetPosition (new Vector3 (-12.5f, -1, -5));
		menuNum = 1;
	}

	/// <summary>
	/// Goes to main menu instantly without panning the camera slowly
	/// </summary>
	public void GoToMainMenuFast(){
		//fade?
		_camera.transform.position = new Vector3 (-12.5f, -1, -5);
	}

	public void GoToMapSelection(){
		_camera.setTargetPosition (new Vector3 (20, 15, -5));
		menuNum = 3;
	}

	public void GoToDifficultySelection(){
		_camera.setTargetPosition (new Vector3 (39, 29, -5));
		menuNum = 3;
	}

    public void GoToWaveSelection()
    {
        _camera.setTargetPosition(new Vector3(55, 15, -5));
        menuNum = 4;
    }

	public void GoToGame(){
		ObjectManager objectManager = ObjectManager.GetInstance();
		objectManager.gameState = gameState;
		Application.LoadLevel ("Scene");
		loadScreen.color = new Color(0,0,0,1);
        loadIcon.color = new Color(0.2265625f, 0.390625f, 0f, 1f);
	}

	public void GoToTutorialOne(){
		//fade?
		_camera.transform.position = new Vector3(-8.73f, 1.18f, 201.09f);
	}

	public void GoToTutorialTwo(){
		_camera.transform.position = new Vector3 (22.1f, 1.18f, 201.09f);
	}

	public void GoToTutorialThree(){
		_camera.transform.position = new Vector3 (46.84f, 1.18f, 201.09f);
	}

	public void GoToTutorialFour(){
		_camera.transform.position = new Vector3 (77.51f, 1.18f, 201.09f);
	}

	public void Quit(){
		Application.Quit ();
	}

	public void Options(){
		_camera.setTargetPosition (new Vector3 (46.5f, 2.8f, -6));
		menuNum = 2;
	}
}
