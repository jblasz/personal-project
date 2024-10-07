using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    // public float mass = 1.0f;

    public float planetRadius;
    public float rotationSpeed;
    public float orbitRadius;
    public float age;
    public float fullAge;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(planetRadius, planetRadius, planetRadius);
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        RotateAroundItself();
        UpdatePosition();
        age += Time.deltaTime;
        if (age > fullAge)
        {
            age -= fullAge;
        }
    }

    void UpdatePosition()
    {
        Debug.Log(gameObject);
        float currentAngle = age / fullAge * 2 * (float)Math.PI;
        float newX = orbitRadius * (float)Math.Sin(currentAngle);
        float newZ = orbitRadius * (float)Math.Cos(currentAngle);
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
}
