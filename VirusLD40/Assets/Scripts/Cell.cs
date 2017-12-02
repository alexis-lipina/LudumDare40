using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    [SerializeField] private GameObject testRightWall;
    [SerializeField] private GameObject testLeftWall;
    [SerializeField] private float angle;
    [SerializeField] private float flowPower;
   
    private Rigidbody2D rb;
    private Vector3 wall;
    private Vector3 flow;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        
    }
	
	void FixedUpdate () {
        wall = testRightWall.transform.TransformDirection(transform.up);
        flow = Quaternion.Euler(0.0f, 0.0f, angle) * wall * (1 / Vector3.Distance(transform.position, wall));
        //Debug.Log(Vector3.Distance(transform.position, wall));
        wall = testLeftWall.transform.TransformDirection(transform.up);
        flow += Quaternion.Euler(0.0f, 0.0f, -angle) * wall * (1 / Vector3.Distance(transform.position, wall));
        //Debug.Log(Vector3.Distance(transform.position, wall));
        rb.AddForce(flow*flowPower);
	}
}
