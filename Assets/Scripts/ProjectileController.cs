using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float launchForce;
    float forceDeterioration = 1.0f;
    Vector3 currentVelocity;
    PlanetController[] planets;
    SystemController system;

    // Start is called before the first frame update
    void Start()
    {
        system = FindObjectOfType<SystemController>();
    }

    // Update is called once per frame
    void Update()
    {
        planets = FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        Debug.Log("planetCount:" + planets.Length);

        Vector3 compositeForce = transform.forward * launchForce;
        for (var i = 0; i < planets.Length; i++)
        {
            Debug.Log("planet " + i);
            DebVect(compositeForce, "composite");
            compositeForce += calculateGravity(planets[i]);
        }
        // foreach (var planet in planets)
        // {
        //     // Debug.Log("x: " + compositeForce.y + "y: " + compositeForce.y + "z: " + compositeForce.z);
        // }

        DebVect(compositeForce, "composite");
        var lookRotation = Quaternion.LookRotation(compositeForce);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * system.turnSpeed);

        var velocity = compositeForce * Time.deltaTime + currentVelocity;

        transform.Translate(Vector3.forward * velocity.magnitude * Time.deltaTime);

        launchForce *= forceDeterioration;

        // Vector3 compositeForce = launchForce;
        // planets = GameObject.FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        // foreach (var planet in planets)
        // {
        //     compositeForce += calculateGravity(planet);
        //     Debug.Log("x: " + compositeForce.y + "y: " + compositeForce.y + "z: " + compositeForce.z);
        // }
        // var lookRotation = Quaternion.LookRotation(compositeForce);
        // transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * system.turnSpeed);
        // transform.Translate(Vector3.forward * Time.deltaTime * compositeForce.magnitude);
        // launchForce = compositeForce * 0.5f;
    }

    Vector3 calculateGravity(PlanetController planet)
    {
        var vect = planet.transform.position - transform.position;
        // DebVect(vect, "vectToPlanet");
        var l = vect.magnitude;
        var normalVector = vect.normalized;
        // Debug.Log(l);
        // Debug.Log(normalVector);
        var g = system.gravityCoefficient;
        var force = g * planet.mass / (l * l);
        Debug.Log("f: " + force + " g: " + g + " m: " + planet.mass + " l: " + l);
        // DebVect(normalVector * g * force, "gravRes");
        return normalVector * g * force;
    }

    void DebVect(Vector3 v, String msg = "")
    {
        Debug.Log(msg + " x: " + v.x + " y: " + v.y + " z: " + v.z);
    }
}
