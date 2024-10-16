using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;

    float currentForce = 1.0f;

    float forceIncrement = 0.5f;
    float minForce = 0.0f;
    float maxForce = 1.0f;

    float playerLaunchOrbit = 8.0f;

    float currentAngle = 0.0f;
    float angleIncrement = 40.0f;
    float maxAngle = 360;

    // Start is called before the first frame update
    void Start()
    {
        LaunchProjectile();
    }

    void LaunchProjectile()
    {
        var go = Instantiate(
            projectilePrefab,
            GetLineStart(),
            projectilePrefab.transform.rotation
        );
        var pc = go.GetComponent<ProjectileController>();
        var start = GetLineStart();
        var end = GetLineEnd();
        pc.launchForce = (end - start).normalized * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            currentForce = Math.Min(currentForce + Time.deltaTime * forceIncrement, maxForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentForce = Math.Max(currentForce - Time.deltaTime * forceIncrement, minForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            currentAngle += Time.deltaTime * angleIncrement;
            while (currentAngle > 0)
            {
                currentAngle -= maxAngle;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentAngle -= Time.deltaTime * angleIncrement;
            while (currentAngle < 0)
            {
                currentAngle += maxAngle;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
        DrawForceLine();
    }

    Vector3 GetPointOnCircle(float radius)
    {
        float angle = currentAngle / maxAngle * 2 * (float)System.Math.PI;
        float x = radius * (float)System.Math.Sin(angle);
        float z = radius * (float)System.Math.Cos(angle);
        return new Vector3(x, 0, z);
    }

    Vector3 GetLineStart()
    {
        return GetPointOnCircle(playerLaunchOrbit);
    }

    Vector3 GetLineEnd()
    {
        return GetPointOnCircle(playerLaunchOrbit - currentForce);
    }

    void DrawForceLine()
    {
        var line = FindObjectOfType<LineRenderer>();
        // line.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        float angle = currentAngle / maxAngle * 2 * (float)System.Math.PI;
        var start = GetLineStart();
        var end = GetLineEnd();

        line.startColor = Color.red;
        line.endColor = Color.cyan;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        // Debug.Log("x:" + startX + "z:" + startZ + "angle:" + angle + "force:" + currentForce);
        line.SetPosition(0, new Vector3(
            start.x,
            line.transform.position.y,
            start.z
            ));
        line.SetPosition(1, new Vector3(
            end.x,
            line.transform.position.y,
            end.z
        ));
    }
}
