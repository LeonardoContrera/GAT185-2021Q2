using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public float rate;
    public float amplitude;

    float time;

    void Update()
    {
        time += Time.deltaTime * rate;

        Vector3 position = Vector3.zero;
        position.x = (Mathf.PerlinNoise(time, 0) * 2) - 1;
        position.y = (Mathf.PerlinNoise(0, time)* 2) - 1;
        position.z = (Mathf.PerlinNoise(time, time)* 2) - 1;

        transform.localPosition = position * amplitude;

        //transform.localPosition = Random.insideUnitSphere * amplitude;
    }
}
