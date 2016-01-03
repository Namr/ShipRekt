using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public float Mass;
	public int ShipID = 0;
	public int BlockID = 0;
	public BlockController bc;
	public Transform ParentTrans;
	public Parent Parent;

	//Deletes the block while making sure that the other blocks around it are uneffected
	public void Delete()
	{
		//run a loop for up down left right and check to see how many blocks are connected to this block
		//b is how many adjacent blocks are connected
		int b = 0;
		for(int i = 0;i < 4;i++)
		{
			if(CheckAdjacent(i) != null)
			{
				b++;
			}
		}
		//block removes its self from the database of stored blocks
		bc.Blocks.Remove (bc.Blocks [bc.Blocks.IndexOf (this.transform)]);
		//if there are no blocks attached to this one, destory the parent ship object
		if(b == 0)
		{
			Destroy(ParentTrans.gameObject);
		}
		//if there is one block attached tell the parent to recalculate its mass but dont worry about reconnecting
		if (b == 1)
		{
			Mass = 0;
			Parent.CheckBlocks();
			Destroy(this.gameObject);
		}
		//if this block has over 1 connection let the parent deal with deletion
	    if(b > 1)
		{
			Parent.DestroyBlock(this.transform);
		}

	}

	//InvokeAdjacent is a recursive function for the reconnection of adjacent blocks to current ship parent
	//(and the setup that comes with that)
	public void InvokeAdjacent(int NewShipID)
	{
		ShipID = NewShipID;
		GameObject tParent = GameObject.Find ("Parent " + NewShipID.ToString ());
		//if there is no parent with this ShipID, make one
		if (tParent == null) {
			ParentTrans = Instantiate (bc.Parent, transform.position, Quaternion.identity) as Transform;
			Parent = ParentTrans.GetComponent<Parent>();
			Parent.ShipID = NewShipID;
			Parent.bc = bc;
			ParentTrans.name = "Parent " + NewShipID.ToString ();
		}
		//join the ship with the ID
		else 
		{
			ParentTrans = tParent.transform;
			Parent = ParentTrans.GetComponent<Parent>();
		}
		transform.parent = ParentTrans;
		//Make the other blocks adjacent to this one do this process (recursive)
		for(int i = 0;i < 4;i++)
		{
			Block thisCheck = CheckAdjacent(i);
			if(thisCheck != null && thisCheck.ShipID != NewShipID)
			{
				thisCheck.InvokeAdjacent(NewShipID);
			}
		}
	}
	//Do a raycast to check for an adjacent block (0 = up,1 = down,2 = right,3 = left)
	public Block CheckAdjacent(int dir)
	{
		RaycastHit2D[] hits;
		//decide the direction to check for a block (0 = up,1 = down,2 = right,3 = left)
		switch (dir)
		{
			case 0:
				hits = Physics2D.RaycastAll (transform.position, transform.up,0.6f);
			break;
			case 1:
				hits = Physics2D.RaycastAll (transform.position, -transform.up,0.6f);
			break;
			case 2:
				hits = Physics2D.RaycastAll (transform.position, transform.right,0.6f);
			break;
			case 3:
				hits = Physics2D.RaycastAll (transform.position, -transform.right,0.6f);
			break;
			default:
				hits = Physics2D.RaycastAll (transform.position, transform.up,0.6f);
			break;
		}
		//make sure the block we hit was not 1. us 2. a shipblock 3. is not part of another ship
		if (hits != null) {
			foreach (RaycastHit2D hit in hits) {
				Debug.Log (RoundToX(Vector3.Distance(hit.transform.position,transform.position),0.5f));
				if (hit.collider.transform != transform && hit.collider.tag == transform.tag && 
					RoundToX(Vector3.Distance(hit.transform.position,transform.position),1.0f) % 1 == 0 && 
					RoundToX(hit.transform.rotation.eulerAngles.z - transform.rotation.eulerAngles.z,0.2f) % 90 == 0){
					return hit.collider.GetComponent<Block> ();
				}
			}
		} 
		return null;
	}

	//rounds a number to the nearest X value
	public float RoundToX(float f, float x)
	{
		return x * Mathf.RoundToInt(f / x);
	}
}
