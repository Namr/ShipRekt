using UnityEngine;
using System.Collections;

public class Parent : MonoBehaviour {
	public int ShipID;
	public BlockController bc;
	public Rigidbody2D rigidBody2D;
	// Use this for initialization
	void Awake() {
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void CheckBlocks()
	{
		Block block;
		Vector2 SumLocation = new Vector2(0,0);
		rigidBody2D.mass = 0.0f;
		foreach (Transform child in transform)
		{
			if(child.GetComponent<Block>() != null)
			{
				block = child.GetComponent<Block>();
				rigidBody2D.mass += block.Mass;
				SumLocation.x += block.transform.position.x * block.Mass;
				SumLocation.y += block.transform.position.y * block.Mass;
			}

		}
		rigidBody2D.centerOfMass = new Vector2 (SumLocation.x / rigidBody2D.mass, SumLocation.y / rigidBody2D.mass);
		//print (rigidBody2D.centerOfMass);
	}

	public void DestroyBlock(Transform block)
	{
		Destroy(block.gameObject);
		ClearChildren();
	}
	public void ClearChildren()
	{
		foreach (Transform child in transform)
		{
			if(child.GetComponent<Block>() != null)
			{
				child.GetComponent<Block>().ShipID = 0;
			}
		}
		transform.GetChild (0).GetComponent<Block>().InvokeAdjacent(ShipID);
		bc.Reconnect();
		CheckBlocks ();
	}
}
