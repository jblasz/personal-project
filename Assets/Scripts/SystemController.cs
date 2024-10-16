using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemController : MonoBehaviour
{
    public GameObject planetPrefab;
    public GameObject projectilePrefab;
    public PlayerController player;
    public float gravityCoefficient = 1.0f;
    public float turnSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        gravityCoefficient = 0.7f;
        // GeneratePlanet(3.0f);
        // GeneratePlanet(4.0f);
        // GeneratePlanet(5.0f);
        // GeneratePlanet(6.0f);
        // GeneratePlanet(7.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GeneratePlanet(
        float orbit = 0
    )
    {
        GameObject planet = Instantiate(
            planetPrefab,
            new Vector3(0, 0, 0),
            planetPrefab.transform.rotation
            );
        PlanetController pcScript = planet.GetComponent<PlanetController>();
        pcScript.planetRadius = Random.Range(0.5f, 1f);
        pcScript.rotationSpeed = Random.Range(3, 72) * 10;
        pcScript.orbitRadius = orbit == 0 ? Random.Range(3, 10) : orbit;
        pcScript.fullAge = Random.Range(5, 20);
        pcScript.age = Random.Range(0, pcScript.fullAge);
    }
}
