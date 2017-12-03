using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusMovement : MonoBehaviour
{

    [SerializeField] private float speed;


    private CustomKeyBinding controls;


    private KeyCode up;
    private KeyCode down;
    private KeyCode right;
    private KeyCode  left;

    protected Rigidbody2D rb;

    /// <summary>
    /// Gets the control scheme for this virus
    /// </summary>
    public CustomKeyBinding Controls { get { return controls; } set { controls = value; } }

    /// <summary>
    /// Sets up the controls for this virus
    /// </summary>
    /// <param name="control">The control scheme to use</param>
    public void Init(CustomKeyBinding control)
    {
        controls = control;

        up = controls.Up;
        down = controls.Down;
        left = controls.Left;
        right = controls.Right;
    }

    void Awake ()
    {
        //test values
        up = KeyCode.UpArrow;
        down = KeyCode.DownArrow;
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;

        rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate ()
    {
        Vector3 moveVelocity = new Vector3();
        if (Input.GetKey(up)) { moveVelocity += new Vector3(0.0f, speed); }
        if (Input.GetKey(down)) { moveVelocity += new Vector3(0.0f, -speed); }
        if (Input.GetKey(right)) { moveVelocity += new Vector3(speed, 0.0f); }
        if (Input.GetKey(left)) { moveVelocity += new Vector3(-speed, 0.0f); }
        rb.AddForce(moveVelocity);
    }

    /// <summary>
    /// Destroys red blood cells on collision and creates anew virus
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if not colliding with a red blood cell, do nothing
        if(collision.gameObject.tag != "RedBloodCell")
        {
            return;
        }

        //destroy the red blood cell and create a new virus
        Vector3 pos = collision.gameObject.transform.position;
        Destroy(collision.gameObject);
        VirusManager manager = GameObject.FindGameObjectWithTag("VirusManager").GetComponent<VirusManager>();
        manager.CreateVirus(pos);
    }
}
