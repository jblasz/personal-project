using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float launchForce;
    public ParticleSystem explodeParticle;
    Vector3 currentVelocity;
    PlanetController[] planets;
    SystemController systemController;

    // Start is called before the first frame update
    void Start()
    {
        systemController = FindObjectOfType<SystemController>();
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutOfBound();
        if (gameObject == null)
        {
            return;
        }

        planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);

        Vector3 compositeForce = transform.forward * launchForce;

        foreach (var planet in planets)
        {
            compositeForce += calculateGravity(planet);
        }

        var lookRotation = Quaternion.LookRotation(compositeForce);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * systemController.turnSpeed);

        var velocity = compositeForce * Time.deltaTime + currentVelocity;
        transform.Translate(Vector3.forward * velocity.magnitude * Time.deltaTime);
    }

    Vector3 calculateGravity(PlanetController planet)
    {
        var vect = planet.transform.position - transform.position;
        var l = vect.magnitude;
        var normalVector = vect.normalized;
        var g = systemController.gravityCoefficient;
        var force = g * planet.mass / (l * l);
        return normalVector * g * force;
    }

    void DestroyOutOfBound()
    {
        if (gameObject.transform.position.magnitude > 12)
        {
            systemController.UpdateProjectilesLeft(0);
            Destroy(gameObject, 0.2f);
        }
    }
}
