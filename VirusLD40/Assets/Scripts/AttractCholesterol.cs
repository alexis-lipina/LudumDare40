using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractCholesterol : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float damping;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Cholesterol" || collision.gameObject.tag == "Wall")
        {
            FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = collision.rigidbody;
            joint.dampingRatio = damping;
            joint.frequency = frequency;
        }
    }
}
