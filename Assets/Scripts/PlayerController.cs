using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    GameState gameState;

    float currentGauge = 0.5f;

    float gaugeIncrement = 0.5f;
    float minGauge = 0.2f;
    float maxGauge = 1.0f;
    float maxForce = 600;

    float playerLaunchOrbit = 11.0f;

    float currentAngle = 0.0f;
    float angleIncrement = 60.0f;
    float maxAngle = 360;

    // Start is called before the first frame update
    void Start()
    {
        gameState = FindFirstObjectByType<GameState>();
    }

    void LaunchProjectile()
    {
        if (gameState.projectilesLeft > 0)
        {
            var start = GetLineStart();
            var end = GetLineEnd();
            var go = Instantiate(
                projectilePrefab,
                GetLineStart(),
                Quaternion.LookRotation((end - start).normalized, Vector3.up)
            );
            var pc = go.GetComponent<ProjectileController>();
            pc.launchForce = currentGauge * maxForce;
            gameState.UpdateProjectilesLeft(-1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.gameStage != GameState.GameStage.GAMEPLAY)
        {
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            currentGauge = Math.Min(currentGauge + Time.deltaTime * gaugeIncrement, maxGauge);
        }
        if (Input.GetKey(KeyCode.S))
        {
            currentGauge = Math.Max(currentGauge - Time.deltaTime * gaugeIncrement, minGauge);
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
        float x = radius * (float)Math.Sin(angle);
        float z = radius * (float)Math.Cos(angle);
        return new Vector3(x, 0, z);
    }

    Vector3 GetLineStart()
    {
        return GetPointOnCircle(playerLaunchOrbit);
    }

    Vector3 GetLineEnd()
    {
        return GetPointOnCircle(playerLaunchOrbit - currentGauge);
    }

    void DrawForceLine()
    {
        var line = gameState.playerLine;
        var start = GetLineStart();
        var end = GetLineEnd();

        line.startColor = Color.red;
        line.endColor = Color.cyan;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
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
