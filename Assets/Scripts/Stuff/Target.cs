using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int points;
    public Material material;
    public GameObject destroyGameObject;


    private void Start()
    {
        GetComponent<Renderer>().material = material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject, 1);
            //Add Score to Game
            if(GetComponent<MeshRenderer>().material.ToString() == "TargetGreen")
            {
                points = 200;
            }
            Game.Instance.AddPoints(points);
            if(destroyGameObject != null)
            {
                Destroy(destroyGameObject);
            }
        }
    }

}
