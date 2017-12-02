using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour {

    [SerializeField] private float speed;

    private string up;
    private string down;
    private string right;
    private string left;

    Rigidbody2D rb;
   
	void Awake () {
        //test values
        up = "w";
        down = "s";
        right = "d";
        left = "a";

        rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
        Vector3 moveVelocity = new Vector3();
        if (Input.GetKey(up)) { moveVelocity += new Vector3(0.0f, speed); }
        if (Input.GetKey(down)) { moveVelocity += new Vector3(0.0f, -speed); }
        if (Input.GetKey(right)) { moveVelocity += new Vector3(speed, 0.0f); }
        if (Input.GetKey(left)) { moveVelocity += new Vector3(-speed, 0.0f); }
        rb.AddForce(moveVelocity);
    }
}
