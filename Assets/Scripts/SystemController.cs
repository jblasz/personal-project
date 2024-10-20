using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemController : MonoBehaviour
{
    public GameObject planetPrefab;
    public GameObject projectilePrefab;
    public GameObject parent = null;
    public GameState gameState;
    int currId;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindFirstObjectByType<GameState>();
    }

    public void InitializeSystem()
    {
        GenerateMoons(GeneratePlanet(1.5f));
        GenerateMoons(GeneratePlanet(3.0f));
        GenerateMoons(GeneratePlanet(4.5f));
        GenerateMoons(GeneratePlanet(6.0f));
        GenerateMoons(GeneratePlanet(7.5f));
        GenerateMoons(GeneratePlanet(9.0f));
    }

    void GenerateMoons(GameObject parent)
    {
        Debug.Log(gameState.currentDifficulty);
        Debug.Log("Generate" + gameState.moonCountMin + " " + gameState.moonCountMax);
        int count = Random.Range(gameState.moonCountMin, gameState.moonCountMax);
        for (var i = 0; i < count; i++)
        {
            GeneratePlanet(1.2f / count * (i + 1), parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    GameObject GeneratePlanet(
        float orbit,
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
            pcScript.ID = currId++;
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
            pcScript.ID = currId++;
            return planet;
        }

    }
}
