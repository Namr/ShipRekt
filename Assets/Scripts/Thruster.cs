using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {
	public Block block;
	public float Speed;
	// Use this for initialization
	void Start () {
		block = GetComponent<Block>();
	}
	
	// Update is called once per frame
	void Update () {
		//apply the force needed to the parent
		if (Input.GetKey(KeyCode.Space)) {
			block.Parent.rigidBody2D.AddForceAtPosition (transform.up * Speed, transform.position);
		}
	}
}
