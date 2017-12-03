using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WrapperDirection { Up, Down, Left, Right}

public class ScreenWrapper : MonoBehaviour
{
    [SerializeField] Vector3 positionToWrapTo;
    [SerializeField] WrapperDirection screenSide;

    /// <summary>
    /// Warps a gameobject that enters this object's collider to the specified position
    /// </summary>
    private void OnTriggerExit2D(Collider2D other)
    {
        float x = other.gameObject.transform.position.x;
        float y = other.gameObject.transform.position.y;
        float z = other.gameObject.transform.position.z;

        switch (screenSide)
        {
            case WrapperDirection.Up:
                if (other.GetComponent<Rigidbody2D>().velocity.y > 0)
                {
                    other.gameObject.transform.position = new Vector3(x + positionToWrapTo.x, y + positionToWrapTo.y, z + positionToWrapTo.z);
                }
                break;
            case WrapperDirection.Down:
                if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
                {
                    other.gameObject.transform.position = new Vector3(x + positionToWrapTo.x, y + positionToWrapTo.y, z + positionToWrapTo.z);
                }
                break;
            case WrapperDirection.Left:
                if (other.GetComponent<Rigidbody2D>().velocity.x < 0)
                {
                    other.gameObject.transform.position = new Vector3(x + positionToWrapTo.x, y + positionToWrapTo.y, z + positionToWrapTo.z);
                }
                break;
            case WrapperDirection.Right:
                if (other.GetComponent<Rigidbody2D>().velocity.x > 0)
                {
                    other.gameObject.transform.position = new Vector3(x + positionToWrapTo.x, y + positionToWrapTo.y, z + positionToWrapTo.z);
                }
                break;
        }
    }
}
