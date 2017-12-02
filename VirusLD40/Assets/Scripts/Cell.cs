using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    [SerializeField] private GameObject testRightWall;
    [SerializeField] private GameObject testLeftWall;
    [SerializeField] private float angle;
    [SerializeField] private float flowPower;
   
    protected Rigidbody2D rb;
    private Vector3 wall;
    private Vector3 flow;

    void Start () {
        rb = GetComponent<Rigidbody2D>(); 
    }
	
	protected virtual void FixedUpdate () {
        wall = testRightWall.transform.TransformDirection(transform.up);
        float distance = 1 / Mathf.Pow(Vector3.Distance(transform.position, wall) /*- ((RectTransform)transform).rect.width - ((RectTransform)testRightWall.transform).rect.width*/ , 2) ;
        flow = Quaternion.Euler(0.0f, 0.0f, angle) * wall * distance;
        wall = testLeftWall.transform.TransformDirection(transform.up);
        distance = 1 / Mathf.Pow(Vector3.Distance(transform.position, wall) /*- ((RectTransform)transform).rect.width - ((RectTransform)testRightWall.transform).rect.width*/ , 2);
        flow += Quaternion.Euler(0.0f, 0.0f, angle * -1) * wall * distance;
        rb.AddForce(flow * flowPower);

	}
}
