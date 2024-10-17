using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public ParticleSystem explodeParticle;
    public float mass;
    public float planetRadius;
    public float rotationSpeed;
    public float orbitRadius;
    public float age;
    public float fullAge;
    public GameObject parent = null;
    public SystemController systemController;
    public bool orbitsCounterclockwise = false;
    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
        UpdatePosition();
        systemController = FindFirstObjectByType<SystemController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateAroundItself();
        UpdatePosition();
        if (orbitsCounterclockwise)
        {
            age += Time.deltaTime;
        }
        else
        {
            age -= Time.deltaTime;
        }
        while (age > fullAge)
        {
            age -= fullAge;
        }
        while (age < 0)
        {
            age += fullAge;
        }
    }

    void UpdatePosition()
    {
        float addX = parent == null ? 0 : parent.transform.position.x;
        float addZ = parent == null ? 0 : parent.transform.position.z;
        float currentAngle = age / fullAge * 2 * (float)Math.PI;
        float newX = addX + orbitRadius * (float)Math.Sin(currentAngle);
        float newZ = addZ + orbitRadius * (float)Math.Cos(currentAngle);
        transform.position = new Vector3(
            newX,
            transform.position.y,
            newZ
            );
    }

    // rotate around its own axis. purely visual.
    void RotateAroundItself()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    public void BlowUp()
    {
        var ep = Instantiate(explodeParticle);
        ep.transform.position = gameObject.transform.position;
        ep.Play();
        Destroy(ep, 5);
        Destroy(gameObject, 0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        var isProj = collision.gameObject.GetComponent<ProjectileController>();
        if (isProj != null)
        {
            var planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
            var firstPlanet = planets
                .Where(planet => !planet.parent)
                .OrderBy(planet => planet.orbitRadius)
                .First();

            if (firstPlanet.ID == ID || parent)
            {
                Destroy(collision.gameObject);
                systemController.UpdateProjectilesLeft(1);
                BlowUp();
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
