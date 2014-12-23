using UnityEngine;
using System.Collections;

public class MenuCameraLogic : MonoBehaviour {
	
	private Vector3? targetPosition;
	private Quaternion? targetRotation;
	public float speed;
	public float angularVelocity;
	
	[ExecuteInEditMode]
	// Use this for initialization
	void Start () {
		speed = 40;
		angularVelocity = .8f;
	}
	
	// Update is called once per frame
	void Update () {
		if (targetPosition.HasValue) {
			MoveTowardTargetPosition ();
			///check if destination is nigh
			if (Vector3.Distance (transform.position, new Vector3 (targetPosition.Value.x, transform.position.y, targetPosition.Value.z)) < .9) {
				transform.position = targetPosition.Value;
				targetPosition = null;
			}
		}
		
		if (targetRotation.HasValue) {
			MoveTowardTargetRotation();
		}
	}
	
	/// <summary>
	/// Moves the toward target position.
	/// </summary>
	void MoveTowardTargetPosition() {
		Vector3 moveVector = new Vector3(transform.position.x - targetPosition.Value.x,
		                                 transform.position.y - targetPosition.Value.y,
		                                 transform.position.z - targetPosition.Value.z).normalized;
		// update the position
		transform.position = new Vector3(transform.position.x - moveVector.x * speed * Time.deltaTime,
		                                 transform.position.y - moveVector.y * speed * Time.deltaTime,
		                                 transform.position.z - moveVector.z * speed * Time.deltaTime);
	}
	
	void MoveTowardTargetRotation() {
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation.Value, Time.deltaTime * angularVelocity);
		
	}
	
	/// <summary>
	/// Sets the target position.
	/// </summary>
	/// <param name="tp">Tp.</param>
	public void setTargetPosition(Vector3 tp){
		targetPosition = tp;
	}
	
	public void setTargetRotation(Quaternion tr){
		targetRotation = tr;
	}
	
}