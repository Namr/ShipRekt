using UnityEngine;
using System.Collections;

public class GhostBlock : MonoBehaviour {
	public Transform Block;
	Vector3 pos;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		pos = Input.mousePosition;
		pos.z = 10;
		pos = Camera.main.ScreenToWorldPoint (pos);
		if (transform.parent == null) {
			transform.position = pos;
		}
		else
		{
			transform.position = transform.rotation * new Vector3(Mathf.RoundToInt(pos.x),Mathf.RoundToInt(pos.y),Mathf.RoundToInt(pos.z));
		}
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag == "ShipBlock")
		{
			transform.parent = other.GetComponent<Block>().Parent;
			transform.localRotation = Quaternion.Euler(0,0,0);
		}
	}

}
