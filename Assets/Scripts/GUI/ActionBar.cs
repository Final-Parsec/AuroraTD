using System;
using UnityEngine;
public class ActionBar : MonoBehaviour
{
	public Texture2D actionBar;
	public Rect position;
	public TowerSlot[] towers;
	public ObjectManager _ObjectManager;

	public UILabel sendWave;
	public UILabel waveCountDown;
	public UILabel moneyValue;
	public UILabel healthValue;
	
	public void ClickEvent(Vector3 mousePosition)
	{
	
	}

	void Start(){
		_ObjectManager = ObjectManager.GetInstance ();
	
	}

	void OnGUI(){

		if(!_ObjectManager.gameState.gameStarted)
			sendWave.text = "Start Game";
		else
			sendWave.text = "Send Wave";

		waveCountDown.text = _ObjectManager.gameState.nextWaveCountDown.ToString();
		moneyValue.text = _ObjectManager.gameState.playerMoney.ToString();
		healthValue.text = _ObjectManager.gameState.PlayerHealth.ToString();
	}
}