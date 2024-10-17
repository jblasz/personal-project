using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemController : MonoBehaviour
{
    public GameObject planetPrefab;
    public GameObject projectilePrefab;
    public TextMeshProUGUI gameOverScreen;
    public TextMeshProUGUI scoreText;
    public PlayerController player;
    public float gravityCoefficient = 1.0f;
    public float turnSpeed = 1.0f;
    public GameObject parent = null;
    public int projectilesLeft;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gravityCoefficient = 3f;
        var id = 0;
        GeneratePlanet(2.0f, id++);
        GeneratePlanet(3.0f, id++);
        var p = GeneratePlanet(4.5f, id++);
        GeneratePlanet(0.6f, id++, p);
        GeneratePlanet(0.8f, id++, p);
        var p2 = GeneratePlanet(6.0f, id++);
        GeneratePlanet(0.5f, id++, p2);
        var p3 = GeneratePlanet(7.5f, id++);
        GeneratePlanet(0.7f, id++, p3);
        GeneratePlanet(9.0f, id++);
        projectilesLeft = 10;
        UpdateProjectilesLeft(0);
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }

        var planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        var projectiles = FindObjectsByType<ProjectileController>(FindObjectsSortMode.InstanceID);
        Debug.Log("planets:" + planets.Length + "projs" + projectiles.Length);
        if (planets.Length == 0)
        {
            gameOverScreen.text = "Victory";
            gameOver = true;
        }
        else if (projectilesLeft < 1 && projectiles.Length < 1)
        {
            gameOverScreen.text = "Game Over";
            foreach (var planet in planets)
            {
                planet.BlowUp();
                gameOver = true;
            }
        }
    }

    public void UpdateProjectilesLeft(int incr)
    {
        projectilesLeft = System.Math.Max(projectilesLeft + incr, 0);
        scoreText.text = "Projectiles left: " + projectilesLeft;
    }

    GameObject GeneratePlanet(
        float orbit,
        int id,
        GameObject parentPlanet = null
    )
    {
        if (parentPlanet == null)
        {
            GameObject planet = Instantiate(
                planetPrefab,
                new Vector3(0, 0, 0),
                planetPrefab.transform.rotation
                );
            PlanetController pcScript = planet.GetComponent<PlanetController>();
            pcScript.planetRadius = Random.Range(0.5f, 0.8f);
            pcScript.rotationSpeed = Random.Range(3, 72) * 10;
            pcScript.orbitRadius = orbit;
            pcScript.fullAge = Random.Range(5, 20);
            pcScript.age = Random.Range(0, pcScript.fullAge);
            pcScript.mass = pcScript.planetRadius * 50;
            pcScript.parent = parentPlanet;
            pcScript.ID = id;
            return planet;
        }
        else
        {
            GameObject planet = Instantiate(
                planetPrefab,
                new Vector3(0, 0, 0),
                planetPrefab.transform.rotation,
                parentPlanet.transform
                );
            PlanetController pcScript = planet.GetComponent<PlanetController>();
            pcScript.planetRadius = Random.Range(0.25f, 0.5f);
            pcScript.rotationSpeed = Random.Range(3, 72) * 10;
            pcScript.orbitRadius = orbit;
            pcScript.fullAge = Random.Range(1, 4);
            pcScript.age = Random.Range(0, pcScript.fullAge);
            pcScript.mass = pcScript.planetRadius * 15;
            pcScript.parent = parentPlanet;
            pcScript.orbitsCounterclockwise = true;
            pcScript.ID = id;
            return planet;
        }

    }
}
