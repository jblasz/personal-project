using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    float aliveFor;

    // Start is called before the first frame update
    void Start()
    {
        aliveFor = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        aliveFor += Time.deltaTime;
        if (aliveFor > 4.0f)
        {
            Destroy(gameObject);
        }
    }
}
