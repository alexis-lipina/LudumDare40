using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {

    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
	
	void OnCollisionEnter2D(Collision2D coll)
    {
        
        //find the distance between virus position and wall
        if (coll.gameObject.tag == "RedBloodCell")
        {
            
        }

        //increase the Force Magnitude as viruis gets closer to the wall


        /*
        wall = testRightWall.transform.TransformDirection(transform.up);
        float distance = 1  Vector3.Distance(transform.position, wall) - ((RectTransform)transform).rect.width - ((RectTransform)testRightWall.transform).rect.width;
        flow = Quaternion.Euler(0.0f, 0.0f, angle) * wall * distance;
        wall = testLeftWall.transform.TransformDirection(transform.up);
        distance = 1 / Vector3.Distance(transform.position, wall) - ((RectTransform)transform).rect.width - ((RectTransform)testRightWall.transform).rect.width;
        flow += Quaternion.Euler(0.0f, 0.0f, angle * -1) * wall * distance;
        rb.AddForce(flow * flowPower);*/
    }
}
