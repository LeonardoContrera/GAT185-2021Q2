using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if(health != null)
        {
            health.hp = 0;
        }
    }
}
