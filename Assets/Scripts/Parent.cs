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
	//gets all the blocks on the ship and calculates the center of mass of the ship
	public void CheckBlocks()
	{
		//reset all varibles
		Block block;
		Vector2 SumLocation = new Vector2(0,0);
		rigidBody2D.mass = 0.0f;
		//go through each block and add their mass to the ships and get data needed for center of mass
		foreach (Transform child in transform)
		{
			if(child.GetComponent<Block>() != null)
			{
				block = child.GetComponent<Block>();
				rigidBody2D.mass += block.Mass;
				SumLocation.x += block.transform.localPosition.x * block.Mass;
				SumLocation.y += block.transform.localPosition.y * block.Mass;
			}

		}
		//calculate the final center of mass
		rigidBody2D.centerOfMass = new Vector2 (SumLocation.x / rigidBody2D.mass, SumLocation.y / rigidBody2D.mass);
		//print (rigidBody2D.centerOfMass);
	}
	//deletes the block on the ships end
	public void DestroyBlock(Transform block)
	{
		Destroy(block.gameObject);
		ClearChildren();
	}
	//reset all blocks to make sure the ship has been recalculated
	public void ClearChildren()
	{
		//reset ship ID of all blocks
		foreach (Transform child in transform)
		{
			if(child.GetComponent<Block>() != null)
			{
				child.GetComponent<Block>().ShipID = 0;
			}
		}
		//reattach all the blocks to the ship
		transform.GetChild (0).GetComponent<Block>().InvokeAdjacent(ShipID);
		bc.Reconnect();
		CheckBlocks ();
	}
}
