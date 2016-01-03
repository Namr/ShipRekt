using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlockController : MonoBehaviour {
	//Data array of all blocks
	public List<Transform> Blocks = new List<Transform>();
	public Transform Parent;
	public Transform FloorPrefab;
	public Transform ShipCore;
	public int NextShipID = 1;
	// Used on startup for initialization 
	void Start () {
		//adds basic ship (test)
		//adds blocks to data array
		Blocks.Add (Instantiate(ShipCore,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 1,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x - 1,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x,transform.position.y + 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x,transform.position.y - 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x - 1,transform.position.y - 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x - 1,transform.position.y + 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 1,transform.position.y - 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 1,transform.position.y + 1,transform.position.z),Quaternion.identity) as Transform);
		//go through each block and tell them this is the BlockControler to reference.
		foreach(Transform block in Blocks)
		{
			block.GetComponent<Block>().bc = this;
		}
		//Make sure these blocks get connected to one another
		Reconnect();
		Blocks[1].GetComponent<Block>().Parent.CheckBlocks();
	}

	//Takes all the blocks in the Data to checks if they are connected to a ship and connect them to each other
	public void Reconnect()
	{
		//goes through all the blocks
		Block origin;
		foreach(Transform block in Blocks)
		{
			origin = block.GetComponent<Block>();
			if(origin != null && origin.ShipID == 0)
			{
				//lets the blocks recusively reconnect
				origin.InvokeAdjacent(NextShipID++);
			}
		}
	}
}
