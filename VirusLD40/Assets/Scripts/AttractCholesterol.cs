using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractCholesterol : MonoBehaviour
{
    [SerializeField] private float attractiveForce;
    private Rigidbody2D rb;
    private Vector3 attraction = Vector3.zero;

    //for other cholesteral cells to check
    public Vector3 Attraction { get {return attraction; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "RightWall":
                attraction = coll.transform.right;
                break;
            case "LeftWall":
                attraction = coll.transform.right * -1;
                
                break;
            case "Cholesterol":
                AttractCholesterol script = coll.gameObject.GetComponent<AttractCholesterol>();
                attraction = script.Attraction;
                break;
        }
    }
    private void FixedUpdate()
    {
        if(attraction != Vector3.zero)
        {
            rb.AddForce(attraction * attractiveForce);
        }
    }
}
