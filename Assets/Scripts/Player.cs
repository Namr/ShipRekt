using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public int speed;
    Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
	    rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce(new Vector2(0,1));
        }
        else if(Input.GetKey(KeyCode.S))
        {
            rb2d.AddForce(new Vector2(0, -1));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb2d.AddForce(new Vector2(-1, 0));
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb2d.AddForce(new Vector2(1, 0));
        }
	}
}
