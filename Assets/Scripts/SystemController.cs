using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    public GameObject planetPrefab;
    public float gravityCoefficient;

    // Start is called before the first frame update
    void Start()
    {
        GeneratePlanet();
        GeneratePlanet();
        GeneratePlanet();
        GeneratePlanet();
        GeneratePlanet();
        gravityCoefficient = 1;
        // CreatePlanet(8.0f, 0);
        // CreatePlanet(9.0f, 0);
        // CreatePlanet(11.0f, 0);
        // CreatePlanet(13.0f, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GeneratePlanet(
    )
    {
        GameObject planet = Instantiate(
            planetPrefab,
            new Vector3(0, 0, 0),
            planetPrefab.transform.rotation
            );
        PlanetController pcScript = planet.GetComponent<PlanetController>();
        pcScript.planetRadius = Random.Range(0.1f, 2f);
        pcScript.rotationSpeed = Random.Range(3, 72) * 10;
        pcScript.orbitRadius = Random.Range(1, 10);
        pcScript.fullAge = Random.Range(5, 20);
        pcScript.age = Random.Range(0, pcScript.fullAge);
    }
}
