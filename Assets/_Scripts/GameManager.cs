using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameManager : MonoBehaviour
{
    public GameObject ship;
    public GameObject asteriodLarge;
    public GameObject asteriodMedium;
    public GameObject asteriodSmall;
    public GameObject shipParticles;
    public GameObject teleportBoxR;
    public GameObject teleportBoxL;
    public GameObject teleportBoxT;
    public GameObject teleportBoxB;
    public TextMeshProUGUI livesCountTMP;
    public TextMeshProUGUI scoreTMP;
    public int startingLives;
    public int large;
    public int medium;
    public int small;
    private int asteriodsLeft;
    private int score;
    private float zoneSizeX;
    private float zoneSizeZ = 25;
    // Start is called before the first frame update
    void Start()
    {
        zoneSizeX = (float) Screen.width /Screen.height * zoneSizeZ;
        //Debug.Log(Screen.width + " " + Screen.height + " " + zoneSizeX);
        moveTeleportBoxes();
        SpawnAllAsteriods();

    }

    // Update is called once per frame
    void Update()
    {
        livesCountTMP.text = $"Lives: {startingLives}";
        scoreTMP.text = $"Score: {score}";
    }
    
    public void TakeDamage()
    {
        startingLives--;
        ship.GetComponent<Ship>().damageTaken(startingLives);
        GameObject temp = Instantiate(shipParticles, ship.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        Destroy(temp, .5f);
        if (startingLives <= 0)
        {
            gameOver();
        }
    }

    public void SpawnAllAsteriods()
    {
        for (int i = 0; i < large; i++)
        {
            SpawnAsteroid(asteriodLarge);
        }
        for (int i = 0; i < medium; i++)
        {
            SpawnAsteroid(asteriodMedium);
        }
        for (int i = 0; i < small; i++)
        {
            SpawnAsteroid(asteriodSmall);
        }
    }

    public void gainPoints(int points)
    {
        score += points;
    }

    public void asteroidsSpawned(int amount)
    {
        asteriodsLeft += amount;
    }
    public void asteroidDestroyed()
    {
        asteriodsLeft--;
        if (asteriodsLeft <= 0 )
        {
            if (medium >= 2)
            {
                medium -= 2;
                large++;
            }
            else
            {
                medium++;
            }
            if (small >= 3)
            {
                small-= 2;
                medium++;
            }
            small++;

            destroyAll("Projectile");
            SpawnAllAsteriods();
            startingLives++;
        }
    }

    public void destroyAll(string theTag)
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(theTag);
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
    }

    private void SpawnAsteroid(GameObject asteroid)
    {
        int side = (Random.Range(0, 2) * 2) - 1;
        if (Random.Range(0, 2) == 1)
        {
            Instantiate(asteroid, new Vector3(Random.Range(-zoneSizeX, zoneSizeX), 0, zoneSizeZ * side), Quaternion.identity);
        }
        else
        {
            Instantiate(asteroid, new Vector3(zoneSizeX * side, 0, Random.Range(-zoneSizeZ, zoneSizeZ)), Quaternion.identity);
        }
        asteriodsLeft++;
    }

    private void moveTeleportBoxes()
    {
        teleportBoxR.transform.position = new Vector3(zoneSizeX + 10, 0, 0);
        teleportBoxL.transform.position = new Vector3(-(zoneSizeX + 10), 0, 0);
        teleportBoxR.transform.localScale = new Vector3(10, 25, (2 * zoneSizeZ) + 30);
        teleportBoxL.transform.localScale = new Vector3(10, 25, (2 * zoneSizeZ) + 30);
        teleportBoxT.transform.position = new Vector3(0, 0, zoneSizeZ + 10);
        teleportBoxB.transform.position = new Vector3(0, 0, -(zoneSizeZ + 10));
        teleportBoxT.transform.localScale = new Vector3((2 * zoneSizeX) + 30, 25, 10);
        teleportBoxB.transform.localScale = new Vector3((2 * zoneSizeX) + 30, 25, 10);
    }

    private void gameOver()
    {
        destroyAll("Projectile");
        destroyAll("Asteroid");
        livesCountTMP.GetComponent<GameObject>().SetActive(false);
        scoreTMP.GetComponent<GameObject>().SetActive(false);

    }
}
