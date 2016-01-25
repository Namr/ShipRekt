using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float speed;
    Rigidbody2D rb2d;
	void Start()
	{
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Quaternion rot = Quaternion.LookRotation (transform.position - mousePosition,Vector3.forward);
		transform.rotation = rot;
		transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z);
		rb2d.angularVelocity = 0;
		float inputV = Input.GetAxis ("Vertical");
		rb2d.AddForce (gameObject.transform.up * speed * inputV);
		float inputH = Input.GetAxis ("Horizontal");
		rb2d.AddForce (gameObject.transform.right * speed * inputH);
		if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
		{
			rb2d.velocity = new Vector2(0,0);
		}
	}
}
