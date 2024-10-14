using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitationalMovement : MonoBehaviour
{
    public Vector3 engineForwardVector;
    public PlanetController[] planets;
    public SystemController system;

    // Start is called before the first frame update
    void Start()
    {
        engineForwardVector = Vector3.forward * 1;

        system = GameObject.FindObjectOfType<SystemController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 compositeForce = engineForwardVector;
        planets = GameObject.FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        foreach (var planet in planets)
        {
            compositeForce += calculateGravity(planet);
            Debug.Log("x: " + compositeForce.y + "y: " + compositeForce.y + "z: " + compositeForce.z);
        }

        var lookRotation = Quaternion.LookRotation(compositeForce);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * system.turnSpeed);

        transform.Translate(Vector3.forward * Time.deltaTime * compositeForce.magnitude);

        engineForwardVector = compositeForce * 0.1f;
    }

    Vector3 calculateGravity(PlanetController planet)
    {
        var vect = planet.transform.position - transform.position;
        var l = vect.magnitude;
        var normalVector = vect.normalized;
        Debug.Log(l);
        Debug.Log(normalVector);
        var g = system.gravityCoefficient;
        var force = g * planet.mass / (l * l);
        return normalVector * g * force;
    }
}
