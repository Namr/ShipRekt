using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Connecter : MonoBehaviour {
	public Transform ConnectedBody;
	public HingeJoint2D[] joints = new HingeJoint2D[4];
	// Use this for initialization
	void Awake () {
		for (int i = 0; i < 4; i++) 
		{
			joints[i].enabled = false;
		}
		RaycastHit2D[] hits = Physics2D.RaycastAll (transform.position, transform.up,0.6f);
		//Debug.DrawRay (transform.position,transform.up,Color.red,50);
		foreach(RaycastHit2D hit in hits)
		{
			if(hit.transform != transform && hit.transform.tag == transform.tag)
			{
				joints[0].enabled = true;
				joints[0].connectedBody = hit.transform.GetComponent<Rigidbody2D>();
			}
		}
		hits = Physics2D.RaycastAll (transform.position, -transform.up,0.6f);
		//Debug.DrawRay (transform.position,-transform.up,Color.red,50);
		foreach(RaycastHit2D hit in hits)
		{
			if(hit.transform != transform && hit.transform.tag == transform.tag)
			{
				joints[1].enabled = true;
				joints[1].connectedBody = hit.transform.GetComponent<Rigidbody2D>();
			}
		}
		hits = Physics2D.RaycastAll (transform.position, transform.right,0.6f);
		//Debug.DrawRay (transform.position,transform.right,Color.red,50);
		foreach(RaycastHit2D hit in hits)
		{
			if(hit.transform != transform && hit.transform.tag == transform.tag)
			{
				joints[2].enabled = true;
				joints[2].connectedBody = hit.transform.GetComponent<Rigidbody2D>();
			}
		}
		hits = Physics2D.RaycastAll (transform.position, -transform.right,0.6f);
		//Debug.DrawRay (transform.position,-transform.right,Color.red,50);
		foreach(RaycastHit2D hit in hits)
		{
			if(hit.transform != transform && hit.transform.tag == transform.tag)
			{
				joints[3].enabled = true;
				joints[3].connectedBody = hit.transform.GetComponent<Rigidbody2D>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
