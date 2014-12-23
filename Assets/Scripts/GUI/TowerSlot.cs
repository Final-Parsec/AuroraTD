using UnityEngine;
using System.Collections;

public class TowerSlot {

	public string towerName;
	public Texture2D towerTexture;
	public Rect towerRect;
	public bool selected;
	// Use this for initialization
	void Start () {
		selected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void towerSelected(){
		Debug.Log ("tower Selected");
	}
}
