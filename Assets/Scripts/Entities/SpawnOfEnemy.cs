using UnityEngine;
using System.Collections;

public class SpawnOfEnemy : EnemyBase
{
	// Runs when entity is Instantiated
	void Awake ()
	{
		_ObjectManager = ObjectManager.GetInstance ();
		_ObjectManager.AddEntity (this);
		onNode = _ObjectManager.Map.GetClosestNode (transform.position);
		InitAttributes();
	}

	void Start ()
	{


	}
}
