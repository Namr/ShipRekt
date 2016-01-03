using UnityEngine;
using System.Collections;

public class Rudder : MonoBehaviour {
	public Block block;
	public float DefaultTilt;
	//the constant mutliplier of the force the rudder is outputting (before speed calculations)
	public float FudgeFactor;
	public float RudderTilt = 0;
	//the final amount of force the rudder is outputting
	public float force;
	// sets up parent block and the defualt tilt of the block
	void Start () {
		block = transform.parent.GetComponent<Block>();
		DefaultTilt = Mathf.Deg2Rad * 45.0f;
	}
	
	// Update is called once per frame
	//Gets player input and does the math to make the rudder work
	void FixedUpdate () {
		//Get current velocity
		Vector3 Velocity3D = new Vector3 (block.Parent.rigidBody2D.velocity.x,block.Parent.rigidBody2D.velocity.y,0);
		//change it into local space
		Velocity3D = transform.parent.InverseTransformDirection(Velocity3D);
		//get user input and change the rudder tilt
		RudderTilt = 0;
		if (Input.GetKey(KeyCode.A)) {
			RudderTilt += DefaultTilt;
		}
		if (Input.GetKey(KeyCode.D)) {
			RudderTilt -= DefaultTilt;
		}
		//change the objects rotation to match the tilt of the rudder
		transform.localRotation = Quaternion.Euler (0,0,Mathf.Rad2Deg * -RudderTilt);
		//calculate the force that the rudder will output based on tilt and speed (and the constant speed rate) 
		force = Velocity3D.y * FudgeFactor * Mathf.Sin(RudderTilt);
		//Get the angle that the force needs to travel by
		float ForceAngle = (Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z - transform.rotation.eulerAngles.z)) + Mathf.PI / 2;
		//give these calculations to the physics engine and apply the forces
		block.Parent.rigidBody2D.AddForceAtPosition(new Vector2(Mathf.Sin(ForceAngle) * force,
			Mathf.Cos(ForceAngle) * force),transform.position);
		//Debug.DrawRay(transform.position, new Vector2(Mathf.Sin((Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z - transform.rotation.eulerAngles.z)) + Mathf.PI/2) * force,Mathf.Cos((Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z - transform.rotation.eulerAngles.z)) + Mathf.PI/2) * force),Color.black,0.2f);
	}
}
