using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Serializable classes
[System.Serializable]
public class EnemyWaves
{
    [Tooltip("Time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}
#endregion

public class LevelController : MonoBehaviour
{
    // Serializable classes
    public EnemyWaves[] enemyWaves;

    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    private List<GameObject> planetsList = new List<GameObject>();

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Start coroutines for enemy waves, power-ups, and planets
        foreach (var enemyWave in enemyWaves)
        {
            StartCoroutine(CreateEnemyWave(enemyWave.timeToStart, enemyWave.wave));
        }
        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
    }

    // Create a new wave after a delay
    private IEnumerator CreateEnemyWave(float delay, GameObject wave)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        Player player = FindObjectOfType<Player>(); // Find the player dynamically
        if (player != null)
        {
            Instantiate(wave); // Instantiate the wave if the player exists
        }
    }

    // Endless coroutine generating power-up bonuses
    private IEnumerator PowerupBonusCreation()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForNewPowerup);

            PlayerMoving playerMoving = FindObjectOfType<PlayerMoving>(); // Find PlayerMoving dynamically
            if (playerMoving != null)
            {
                Instantiate(
                    powerUp,
                    new Vector2(
                        Random.Range(playerMoving.borders.minX, playerMoving.borders.maxX), // X: Random position within the player's movement bounds
                        mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2 // Y: Just above the screen
                    ),
                    Quaternion.identity
                );
            }
        }
    }

    // Coroutine for creating planets
    private IEnumerator PlanetsCreation()
    {
        // Populate the list with all planets
        foreach (var planet in planets)
        {
            planetsList.Add(planet);
        }

        yield return new WaitForSeconds(10); // Initial delay before starting the loop

        while (true)
        {
            // Choose a random planet, instantiate it, and remove it from the list
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);

            // If the list is empty, refill it
            if (planetsList.Count == 0)
            {
                foreach (var planet in planets)
                {
                    planetsList.Add(planet);
                }
            }

            // Set the speed for the newly created planet
            var movingComponent = newPlanet.GetComponent<DirectMoving>();
            if (movingComponent != null)
            {
                movingComponent.speed = planetsSpeed;
            }

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
