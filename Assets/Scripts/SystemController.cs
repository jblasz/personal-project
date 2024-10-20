using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemController : MonoBehaviour
{
    public GameObject earthPref;
    public GameObject jupiPref;
    public GameObject marsPref;
    public GameObject mercPref;
    public GameObject moonPref;
    public GameObject neptuPref;
    public GameObject plutoPref;
    public GameObject saturPref;
    public GameObject uranuPref;
    public GameObject venusPref;
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
        GenerateMoons(GeneratePlanet(1.0f, PlanetType.MERCURY));
        GenerateMoons(GeneratePlanet(2.5f, PlanetType.VENUS));
        GenerateMoons(GeneratePlanet(4.0f, PlanetType.EARTH));
        GenerateMoons(GeneratePlanet(5.5f, PlanetType.MARS));
        GenerateMoons(GeneratePlanet(7.0f, PlanetType.JUPITER));
        GenerateMoons(GeneratePlanet(8.5f, PlanetType.SATURN));
        GenerateMoons(GeneratePlanet(10.0f, PlanetType.URANUS));
    }

    void GenerateMoons(GameObject parent)
    {
        int count = Random.Range(gameState.moonCountMin, gameState.moonCountMax);
        for (var i = 0; i < count; i++)
        {
            GeneratePlanet(1.2f / count * (i + 1), PlanetType.MOON, parent);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GameObject GetPlanetPrefab(PlanetType pt)
    {
        switch (pt)
        {
            case PlanetType.MERCURY:
                return mercPref;
            case PlanetType.VENUS:
                return venusPref;
            case PlanetType.EARTH:
                return earthPref;
            case PlanetType.MARS:
                return marsPref;
            case PlanetType.JUPITER:
                return jupiPref;
            case PlanetType.SATURN:
                return saturPref;
            case PlanetType.URANUS:
                return uranuPref;
            case PlanetType.NEPTUNE:
                return neptuPref;
            case PlanetType.PLUTO:
                return plutoPref;
            case PlanetType.MOON:
                return moonPref;
        }
        return moonPref;
    }

    GameObject GeneratePlanet(
        float orbit,
        PlanetType pt,
        GameObject parentPlanet = null
    )
    {
        var pref = GetPlanetPrefab(pt);

        if (parentPlanet == null)
        {
            GameObject planet = Instantiate(
                pref,
                new Vector3(0, 0, 0),
                pref.transform.rotation
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
                pref,
                new Vector3(0, 0, 0),
                pref.transform.rotation,
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
