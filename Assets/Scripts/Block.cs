using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public float Mass;
	public int ShipID = 0;
	public int BlockID = 0;
	public BlockController bc;
	public Transform ParentTrans;
	public Parent Parent;
	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Delete()
	{
		//run a for loop check to see how many blocks are connected to this block
		int b = 0;
		for(int i = 0;i < 4;i++)
		{
			if(CheckAdjacent(i) != null)
			{
				b++;
			}
		}
		bc.Blocks.Remove (bc.Blocks [bc.Blocks.IndexOf (this.transform)]);
		//print (b);
		if(b == 0)
		{
			Destroy(ParentTrans.gameObject);
		}
		if (b == 1)
		{
			Mass = 0;
			Parent.CheckBlocks();
			Destroy(this.gameObject);
		}
	    if(b > 1)
		{
			Parent.DestroyBlock(this.transform);
		}

	}

	public void InvokeAdjacent(int NewShipID)
	{
		ShipID = NewShipID;
		GameObject tParent = GameObject.Find ("Parent " + NewShipID.ToString ());
		if (tParent == null) {
			ParentTrans = Instantiate (bc.Parent, transform.position, Quaternion.identity) as Transform;
			Parent = ParentTrans.GetComponent<Parent>();
			Parent.ShipID = NewShipID;
			Parent.bc = bc;
			ParentTrans.name = "Parent " + NewShipID.ToString ();
		}
		else 
		{
			ParentTrans = tParent.transform;
			Parent = ParentTrans.GetComponent<Parent>();
		}
		transform.parent = ParentTrans;
		for(int i = 0;i < 4;i++)
		{
			Block thisCheck = CheckAdjacent(i);
			if(thisCheck != null && thisCheck.ShipID != NewShipID)
			{
				thisCheck.InvokeAdjacent(NewShipID);
			}
		}
	}

	public Block CheckAdjacent(int dir)
	{
		RaycastHit2D[] hits;
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
		//Debug.DrawRay (transform.position,-transform.up,Color.red,50);
		if (hits != null) {
			foreach (RaycastHit2D hit in hits) {
				if (hit.collider.transform != transform && hit.collider.tag == transform.tag) {
					return hit.collider.GetComponent<Block> ();
				}
			}
		} 
		return null;
	}

}
