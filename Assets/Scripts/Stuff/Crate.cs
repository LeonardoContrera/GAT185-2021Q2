using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    AudioSource audioSource = null;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        //The ? is a null check
        audioSource?.Play();
        Debug.Log(collision.gameObject);
    }
}
