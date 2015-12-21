using UnityEngine;
using System.Collections;

public class Rudder : MonoBehaviour {
	public Block block;
	public float DefaultTilt;
	public float FudgeFactor;
	public float RudderTilt = 0;
	public float force;
	// Use this for initialization
	void Start () {
		block = transform.parent.GetComponent<Block>();
		DefaultTilt = Mathf.Deg2Rad * 45.0f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 Velocity3D = new Vector3 (block.Parent.rigidBody2D.velocity.x,block.Parent.rigidBody2D.velocity.y,0);
		Velocity3D = transform.parent.InverseTransformDirection(Velocity3D);
	
		RudderTilt = 0;
		if (Input.GetKey(KeyCode.A)) {
			//block.Parent.rigidBody2D.AddForceAtPosition(new Vector2(Vector3.Scale(Velocity3D,transform.up).y * FudgeFactor * Mathf.Sin(-DefaultTilt),0), transform.position);
			RudderTilt += DefaultTilt;
		}
		if (Input.GetKey(KeyCode.D)) {
			//block.Parent.rigidBody2D.AddForceAtPosition(new Vector2(transform.InverseTransformDirection(Velocity3D).y * FudgeFactor * Mathf.Sin(DefaultTilt),0), transform.position);
			RudderTilt -= DefaultTilt;
		}
		transform.localRotation = Quaternion.Euler (0,0,Mathf.Rad2Deg * -RudderTilt);
		//Vector3 forwardVel = Vector3.Scale(Velocity3D,transform.up);
		/*float*/ force = Velocity3D.y * FudgeFactor * Mathf.Sin(RudderTilt);
		block.Parent.rigidBody2D.AddForceAtPosition(new Vector2(Mathf.Sin((Mathf.Deg2Rad * transform.localRotation.eulerAngles.z) + Mathf.PI/2) * force,Mathf.Cos((Mathf.Deg2Rad * transform.localRotation.eulerAngles.z) + Mathf.PI/2) * force),transform.position);
		Debug.DrawRay(transform.position, new Vector2(Mathf.Sin((Mathf.Deg2Rad * transform.localRotation.eulerAngles.z) + Mathf.PI/2) * force,Mathf.Cos((Mathf.Deg2Rad * transform.localRotation.eulerAngles.z) + Mathf.PI/2) * force),Color.black,5.0f);
	}
}
