using UnityEngine;
using System.Collections.Generic;

public class TurretFactory : MonoBehaviour
{
	public List<GameObject> turretPrefabs;
	public List<int> turretCosts;
	
	// Set or check which type of turret the factory is currently creating.
	public TurretType TurretType { get; set; }
	
	private ObjectManager objectManager;
	
	public void PlaceOrSelectTurret (Vector3 mousePosition)
	{
		Node cursorOnNode = objectManager.Map.GetNodeFromLocation (Camera.main.ScreenToWorldPoint (mousePosition));
		if(cursorOnNode == null || objectManager.gameState.optionsOn)
			return;

		bool canBuild = objectManager.Map.BlockNode (cursorOnNode.unityPosition);
		
		if (turretCosts [(int)TurretType] <= objectManager.gameState.playerMoney && 
            objectManager.Pathfinding.CheckAndUpdatePaths () && 
            canBuild) 
        {
			Vector3 correctedPosition = cursorOnNode.unityPosition;
			correctedPosition.y = -((cursorOnNode.listIndex.z / objectManager.Map.size_z) + (cursorOnNode.listIndex.x / objectManager.Map.size_x));
			Turret turret = ((GameObject) Instantiate (turretPrefabs [(int)TurretType], correctedPosition, Quaternion.Euler (new Vector3 (90, 45, 0)))).GetComponent<Turret>();
			turret.Msrp = turretCosts [(int)TurretType];
            turret.TurretType = TurretType;
			cursorOnNode.turret = turret;
			objectManager.gameState.playerMoney -= turretCosts [(int)TurretType];
			return;
		} 
		
		if (!canBuild)
		{
			objectManager.TurretFocusMenu.SelectedTurret = cursorOnNode.turret;
			return;
		}
		
		Debug.Log ("Unable to place turret at this location.");
		objectManager.Map.UnBlockNode (cursorOnNode.unityPosition);
		objectManager.Pathfinding.CheckAndUpdatePaths ();
	}

	// Use this for initialization
	void Start ()
	{
		objectManager = ObjectManager.GetInstance ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}