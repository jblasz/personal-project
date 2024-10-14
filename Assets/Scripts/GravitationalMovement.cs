using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GravitationalMovement : MonoBehaviour
{
    public Vector3 velVector;
    public Vector3 accelVector;
    public PlanetController[] planets;
    public SystemController system;

    // Start is called before the first frame update
    void Start()
    {
        velVector = new Vector3(0, 0, 0);
        accelVector = new Vector3(0, 0, 0);
        planets = GameObject.FindObjectsByType<PlanetController>(FindObjectsSortMode.InstanceID);
        system = GameObject.FindObjectOfType<SystemController>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Vector3> composites = new List<Vector3>();
        foreach (var planet in planets)
        {
            composites.Append(calculateGravity(planet));
        }
    }

    Vector3 calculateGravity(PlanetController planet)
    {
        var vect = planet.transform.position - gameObject.transform.position;
        var l = vect.magnitude;
        var normalVector = vect.normalized;
        Debug.Log(l);
        Debug.Log(normalVector);
        var g = system.gravityCoefficient;
        var force = g * planet.mass / (l * l);
        return normalVector * g * force;
    }
}
