using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectile;
    public float speed;
    public float projectileDuration;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        projectileDuration -= Time.deltaTime;
        if (projectileDuration < 0)
        {
            Destroy(projectile);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ship"))
        {
            GameObject obj = GameObject.Find("GameManager");
            obj.GetComponent<GameManager>().TakeDamage();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Tbox"))
        {
            GetComponent<TrailRenderer>().Clear();
        }
    }
}
