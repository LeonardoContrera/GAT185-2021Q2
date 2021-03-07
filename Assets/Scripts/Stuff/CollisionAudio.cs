using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    public AudioSource audio;
    private void OnCollisionEnter(Collision collision)
    {
        audio?.Play();
        Debug.Log(collision.gameObject);
    }
}
