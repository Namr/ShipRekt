using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BlockController : MonoBehaviour {
	public List<Transform> Blocks = new List<Transform>();
	public Transform Parent;
	public Transform FloorPrefab;
	public Transform ShipCore;
	public int NextShipID = 1;
	// Use this for initialization
	void Start () {
		Blocks.Add (Instantiate(ShipCore,new Vector3(transform.position.x,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 1,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x - 1,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x,transform.position.y + 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x,transform.position.y - 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x - 1,transform.position.y - 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x - 1,transform.position.y + 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 1,transform.position.y - 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 1,transform.position.y + 1,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 2,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		Blocks.Add (Instantiate(FloorPrefab,new Vector3(transform.position.x + 3,transform.position.y,transform.position.z),Quaternion.identity) as Transform);
		foreach(Transform ship in Blocks)
		{
			ship.GetComponent<Block>().bc = this;
		}
		Reconnect();
		StartCoroutine("Destruction");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reconnect()
	{
		Block origin;
		foreach(Transform block in Blocks)
		{
			origin = block.GetComponent<Block>();
			if(origin != null && origin.ShipID == 0)
			{
				origin.InvokeAdjacent(NextShipID++);
			}
		}
	}

	IEnumerator Destruction()
	{
		yield return new WaitForSeconds (5f);
		Blocks[9].GetComponent<Block>().Delete();
	}
}
