using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBloodCell : MonoBehaviour
{

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private GameObject tracking;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Virus")
        {
            //first virus tracking
            if (tracking == null) { tracking = coll.gameObject; }
            Debug.Log("VIRUS DETECTED");
            //if another virus gets closer, track that one instead
            if (Distance(tracking.transform.position, transform.position) < Distance(coll.gameObject.transform.position, transform.position))
            {
                tracking = coll.gameObject;
            }
        }
    }

    private void FixedUpdate()
    {
        if(tracking != null)
        {
            //gets the direction of the vector to move towards player
            Vector2 force = tracking.transform.position - transform.position;
            //set the magnitude of vector to one
            float magnitude = force.magnitude;
            force.x = force.x / magnitude;
            force.y = force.y / magnitude;
            //apply force
            rb.AddForce(force * speed);
        }
    }

    public float Distance(Vector3 virus, Vector3 self)
    {
        return Mathf.Sqrt(Mathf.Pow(virus.x - self.x, 2) + Mathf.Pow(virus.y - self.y, 2));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if not colliding with a virus, do nothing
        if(collision.gameObject.tag != "Virus")
        {
            return;
        }

        //destroy the virus and tell the virus manager about it
        VirusMovement killedCell = collision.gameObject.GetComponent<VirusMovement>();
        VirusManager manager = GameObject.FindGameObjectWithTag("VirusManager").GetComponent<VirusManager>();
        manager.VirusDestroyed(killedCell);
    }
}