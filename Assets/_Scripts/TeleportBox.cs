using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBox : MonoBehaviour
{
    public Vector3 teleportSide;
    public Vector3 offsetSide;
    public float offset;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ship"))
        {
            other.transform.position = Vector3.Scale(teleportSide, other.transform.position) + (offset * offsetSide);

            other.GetComponent<Ship>().UpdateTrail();

        } else
        {
            other.transform.position = Vector3.Scale(teleportSide, other.transform.position) + (offset * offsetSide);
        }
    }
}
