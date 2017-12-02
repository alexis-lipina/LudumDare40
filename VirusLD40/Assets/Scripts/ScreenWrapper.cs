using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] Vector3 positionToWrapTo;

    /// <summary>
    /// Warps a gameobject that enters this object's collider to the specified position
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.position = positionToWrapTo;
    }
}
