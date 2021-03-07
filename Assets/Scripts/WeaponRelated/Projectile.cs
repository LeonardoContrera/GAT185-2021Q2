using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))] 

public class Projectile : MonoBehaviour
{

    [Range(0, 5)] public float lifeSpan = 2;
    [Range(1, 100)] public float speed = 10.0f;
    public bool destroyOnHit = false;
    public GameObject destroyFX;

    private void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    private void OnDestroy()
    {

        if(destroyFX != null)
        {
                Instantiate(destroyFX, transform.position, transform.rotation);
        }
    }

    // Start is called before the first frame update
    public void Fire(Vector3 forward)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(forward * speed, ForceMode.VelocityChange);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(destroyOnHit)
        {
            Destroy(gameObject);
        }
    }

}
