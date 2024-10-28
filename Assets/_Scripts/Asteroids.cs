using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroids : MonoBehaviour
{
    public GameObject smallerAsteroid;
    public GameObject particles;
    public Boolean isSmallest;
    public float startSpeedMin;
    public float startSpeedMax;
    public float particleSize;
    public int pointsGained;
    private GameObject gameManager;
    
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");
        if (rb != null)
        {
            float angle = UnityEngine.Random.Range(0, 360);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
            rb.velocity = UnityEngine.Random.Range(startSpeedMin, startSpeedMax) * transform.forward;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            gameManager.GetComponent<GameManager>().gainPoints(pointsGained);
            if (!isSmallest)
            {
                Instantiate(smallerAsteroid, transform.position, Quaternion.identity);
                Instantiate(smallerAsteroid, transform.position, Quaternion.identity);
                gameManager.GetComponent<GameManager>().asteroidsSpawned(2);
                
            }
            destroyThis();
        }
        else if (other.gameObject.CompareTag("Ship"))
        {

            gameManager.GetComponent<GameManager>().TakeDamage();
            destroyThis();
        }

    }

    public void destroyThis()
    {
        GameObject temp = Instantiate(particles, transform.position, Quaternion.Euler(-90f, 0f, 0f));
        gameManager.GetComponent<GameManager>().asteroidDestroyed();
        Destroy(temp, .5f);
        Destroy(gameObject);
    }
}
