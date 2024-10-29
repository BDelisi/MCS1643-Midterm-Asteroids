using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Ship : MonoBehaviour
{
    public GameObject trail;
    public KeyCode forward;
    public KeyCode turnLeft;
    public KeyCode turnRight;
    public KeyCode shoot;
    public float speed;
    public float rotationSpeed;
    public float invincibility;
    public GameObject projectile;
    public GameObject shipModel;
    public GameObject shield;

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(forward))
        {
            rb.velocity += speed * Time.deltaTime * transform.forward;
        }
        if (Input.GetKey(turnLeft))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(turnRight))
        {
            transform.Rotate(Vector3.up, rotationSpeed * -1 * Time.deltaTime);
        }
        if (Input.GetKeyDown(shoot))
        {
            {
                GetComponent<AudioSource>().Play();
                Instantiate(projectile, transform.position + transform.rotation * new Vector3(0, 0, 2.5f), transform.rotation);
            }
        }

        if (invincibility > 0)
        {
            shield.SetActive(true);
            invincibility -= Time.deltaTime;
            if (invincibility < 0)
            {
                invincibility = 0;
                shield.SetActive(false);
            }
        }
    }

    public void updateTrail()
    {
        if (trail.GetComponent<TrailRenderer>() != null)
        {
            TrailRenderer trailRenderer = trail.GetComponent<TrailRenderer>();
            trailRenderer.Clear();
        }
    }

    public void damageTaken(int lives)
    {
        invincibility = .5f;
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
    }

}
