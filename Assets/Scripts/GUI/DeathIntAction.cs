using UnityEngine;
using System.Collections;

public class DeathIntAction : MonoBehaviour {
	private static float timeToLive = .8f;
	private static int speed = 5;

	private float madeAt;
	private ObjectManager objectManager;

	// Use this for initialization
	void Start () {
		objectManager = ObjectManager.GetInstance();
		madeAt = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		if(Time.time > madeAt+timeToLive)
			Destroy(gameObject);

	}

	public void Move ()
	{
		// don't move in the Y direction.
		Vector3 moveVector = new Vector3 (-1, 0, -1).normalized;
		
		// update the position
		transform.position = new Vector3 (transform.position.x - moveVector.x * (speed * (int)objectManager.gameState.gameSpeed) * Time.deltaTime,
		                                  transform.position.y,
		                                  transform.position.z - moveVector.z * (speed * (int)objectManager.gameState.gameSpeed) * Time.deltaTime);
		
		// unit has reached the waypoint
		Vector3 position = transform.position;
	}
}
