using UnityEngine;
using System.Collections;

public class Parent : MonoBehaviour {
	public int ShipID;
	public BlockController bc;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
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
		bc.Reconnect ();
	}
}
