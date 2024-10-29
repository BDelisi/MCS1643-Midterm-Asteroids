using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
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
    public TextMeshProUGUI gameOverTMP;
    public TextMeshProUGUI gameOverScoreTMP;
    public GameObject restartButton;
    public int startingLives;
    public int large;
    public int medium;
    public int small;
    public float spawnOffset;
    private int asteriodsLeft;
    private int score;
    private float zoneSizeX;
    private float zoneSizeZ = 25;

    void Start()
    {
        zoneSizeX = (float) Screen.width /Screen.height * zoneSizeZ;
        moveTeleportBoxes();
        SpawnAllAsteriods();

    }

    void Update()
    {
        livesCountTMP.text = $"Lives: {startingLives}";
        scoreTMP.text = $"Score: {score}";
    }

    public void TakeDamage()
    {
        if (ship.GetComponent<Ship>().invincibility <= 0) {
            startingLives--;
            ship.GetComponent<Ship>().damageTaken(startingLives);
            GameObject temp = Instantiate(shipParticles, ship.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(temp, .5f);
            if (startingLives <= 0)
            {
                gameOver();
            }
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
            Instantiate(asteroid, new Vector3(Random.Range(-zoneSizeX + spawnOffset, zoneSizeX - spawnOffset), 0, (zoneSizeZ - spawnOffset) * side), Quaternion.identity);
        }
        else
        {
            Instantiate(asteroid, new Vector3((zoneSizeX - spawnOffset) * side, 0, Random.Range(-zoneSizeZ + spawnOffset, zoneSizeZ - spawnOffset)), Quaternion.identity);
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
        gameOverScoreTMP.text = $"You Scored: {score}";
        livesCountTMP.gameObject.SetActive(false);
        scoreTMP.gameObject.SetActive(false);
        gameOverTMP.gameObject.SetActive(true);
        restartButton.SetActive(true);
        gameOverScoreTMP.gameObject.SetActive(true);
    }
}
