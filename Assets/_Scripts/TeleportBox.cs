using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBox : MonoBehaviour
{
    public Vector3 teleportPosition;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            other.transform.position = Vector3.Scale(teleportPosition, other.transform.position);

            other.GetComponent<Ship>().updateTrail();

        } else
        {
            other.transform.position = Vector3.Scale(teleportPosition, other.transform.position);
        }
    }
}
