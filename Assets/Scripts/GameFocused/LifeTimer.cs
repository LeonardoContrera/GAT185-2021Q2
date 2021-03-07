using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTimer : MonoBehaviour
{
    [Range(0, 30)] public float lifetime = 5;

    void Start()
    {
        GameObject.Destroy(gameObject, lifetime);
    }


}
