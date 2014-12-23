using UnityEngine;
using System.Collections;

public class SendWave : MonoBehaviour {

	private ObjectManager _ObjectManager;
	private bool firstClick = true;

	public GameObject border;

	void Start()
	{
		_ObjectManager = ObjectManager.GetInstance ();
	
	}

	void OnClick ()
	{
		if(firstClick)
        {
			firstClick = false;
			_ObjectManager.gameState.gameStarted = true;
			StartCoroutine(DestroyTween());

		}
        else if (_ObjectManager.Map.currentWaves.Count < 5)
        {
			_ObjectManager.Map.playerTriggeredWave = true;

			// only if you actually sent a wave and it isn't just almost the end of the game
			if(_ObjectManager.Map.upcomingWaves.Count > 0 && !_ObjectManager.gameState.gameOver)
			{
				_ObjectManager.gameState.playerMoney += _ObjectManager.gameState.nextWaveCountDown;
	            _ObjectManager.gameState.score += _ObjectManager.gameState.nextWaveCountDown;
			}
		}
	}

	IEnumerator DestroyTween(){
		TweenColor tween = border.GetComponent<TweenColor>();
		tween.to = tween.from;
		tween.duration = .001f;
		yield return new WaitForSeconds(.01f);
		Destroy (tween);
	}
}
