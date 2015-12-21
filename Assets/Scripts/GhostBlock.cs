using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GhostBlock : MonoBehaviour {
	public bool ShouldCheck = true;
	public List<Transform> Blocks;
	Transform BlockTemplate;
	int CurBlock = 1;
	Vector3 pos;
	bool snapped;
	Block AttachedBlock;
	Block Current;
	Parent parent;
	// Use this for initialization
	void Start () 
	{
		BlockTemplate = Blocks[CurBlock];
		GetComponent<SpriteRenderer> ().sprite = BlockTemplate.GetComponent<SpriteRenderer> ().sprite;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			CurBlock++;
			//print(Blocks.Count);
			//print(CurBlock);
			if(CurBlock >= Blocks.Count)
			{
				CurBlock = 0;
			}
			BlockTemplate = Blocks[CurBlock];
			GetComponent<SpriteRenderer> ().sprite = BlockTemplate.GetComponent<SpriteRenderer> ().sprite;
		}
		pos = Input.mousePosition;
		pos.z = 10;
		pos = Camera.main.ScreenToWorldPoint (pos);
		int missed = 0;
		if(ShouldCheck)
		{
			for(int i = 0;i < 4;i++)
			{
				Current = CheckAdjacent(i);
				if(Current != null)
				{
					AttachedBlock = Current;
					snapped = true;
				}
				else
				{
					missed++;
				}
			}
		}
		if(missed == 4)
		{
			snapped = false;
		}
		if (!snapped) {
			transform.position = pos;
			transform.parent = null;
			transform.rotation = Quaternion.Euler(0,0,0);
			if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				RaycastHit2D[] hits = Physics2D.RaycastAll(pos,transform.forward);
				if (hits != null) {
					foreach (RaycastHit2D hit in hits) {
						if (hit.collider.GetComponent<Transform>() != transform && hit.collider.GetComponent<Transform>().tag == "ShipBlock") 
						{
							Block curBlock =  hit.collider.GetComponent<Block>();
							//print(curBlock.transform.position);
							if(curBlock != null)
							{
								curBlock.Delete();
								return;
							}
						}
					}
				}
			}
		}
		else
		{
			transform.rotation = AttachedBlock.transform.rotation;
			transform.parent = AttachedBlock.ParentTrans;
			parent = transform.parent.GetComponent<Parent>();
			transform.position = pos;
			transform.localPosition = new Vector3(Mathf.RoundToInt(transform.localPosition.x),Mathf.RoundToInt(transform.localPosition.y),transform.localPosition.z);
		}
		if(Input.GetKeyDown(KeyCode.Mouse0) && snapped)
		{
			snapped = false;
			Transform newBlockTrans;
			newBlockTrans = Instantiate(BlockTemplate,new Vector3(transform.position.x,transform.position.y,transform.position.z),transform.rotation) as Transform;
			Block newBlock = newBlockTrans.GetComponent<Block>();
			newBlock.ParentTrans = transform.parent;
			newBlock.transform.parent = transform.parent;
			newBlock.Parent = parent;
			newBlock.bc = parent.bc;
			newBlock.ShipID = parent.ShipID;
			parent.bc.Blocks.Add(newBlockTrans);
			newBlock.Parent.CheckBlocks();
		}
	}
	

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.attachedRigidbody.tag == "ShipBlock")
		{
			snapped = false;
			ShouldCheck = false;
		} 
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.transform.tag == "ShipBlock")
		{
			ShouldCheck = true;
		} 
	}

	void OnCollisionStay2D(Collision2D other) 
	{
		if (other.transform.tag == "ShipBlock")
		{
			snapped = false;
			ShouldCheck = false;
		} 
	}
	
	void OnCollisionExit2D(Collision2D other) 
	{
		if (other.transform.tag == "ShipBlock")
		{
			ShouldCheck = true;
		} 
	}


	public Block CheckAdjacent(int dir)
	{

		RaycastHit2D[] hits;
		switch (dir)
		{
		case 0:
			hits = Physics2D.RaycastAll (transform.position, transform.up,1f);
			break;
		case 1:
			hits = Physics2D.RaycastAll (transform.position, -transform.up,1f);
			break;
		case 2:
			hits = Physics2D.RaycastAll (transform.position, transform.right,1f);
			break;
		case 3:
			hits = Physics2D.RaycastAll (transform.position, -transform.right,1f);
			break;
			/*
		case 4:
			hits = Physics2D.RaycastAll (transform.position, transform.up + transform.right,0.7f);
			Debug.DrawRay(transform.position,transform.up + transform.right,Color.red,0.1f);
			break;
		case 5:
			hits = Physics2D.RaycastAll (transform.position, -transform.up - transform.right,0.7f);
			Debug.DrawRay(transform.position,-transform.up - transform.right,Color.red,0.1f);
			break;
		case 6:
			hits = Physics2D.RaycastAll (transform.position, transform.right - transform.up,0.7f);
			Debug.DrawRay(transform.position, transform.right - transform.up,Color.red,0.1f);
			break;
		case 7:
			hits = Physics2D.RaycastAll (transform.position, -transform.right + transform.up,0.7f);
			Debug.DrawRay(transform.position, -transform.right + transform.up,Color.red,0.1f);
			break;
			*/
		default:
			hits = Physics2D.RaycastAll (transform.position, transform.up,0.7f);
			break;
		}
		if (hits != null) {
			foreach (RaycastHit2D hit in hits) {
				if (hit.collider.transform != transform && hit.collider.tag == "ShipBlock") {
					return hit.collider.GetComponent<Block> ();
				}
			}
		} 
		return null;
	}	
}
