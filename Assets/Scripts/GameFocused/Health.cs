using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float hpMax = 100;
    public float decay;
    public Slider slider;
    public GameObject destroySpawnObject;
    public bool destroyOnDeath = false;
    public UnityEvent deathEvent;
    private ControllerGame.eState state;
    private int score = 100;

    public float hp;
    public bool isDead { get; set;} = false;

    void Start()
    {
        hp = hpMax;
        if(gameObject.tag == "Player")
        {
            slider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();

        }

    }

    // Update is called once per frame
    void Update()
    {
        state = ControllerGame.state;
        //if(ControllerGame.state == ControllerGame.eState.GameStart)
        //{
            if(slider != null)
            {
            slider.value = hp / hpMax;

            }
            AddHP(-Time.deltaTime * decay);
        //}

        if(hp <= 0 && !isDead)
        {
            isDead = true;
            deathEvent?.Invoke();
                if(gameObject.tag != "Player")
                {
                    ScoreHolder.Instance.AddScore(score);
                    Debug.Log("AddingScore");
                }
            if(destroySpawnObject != null)
            {
                Instantiate(destroySpawnObject, transform.position, transform.rotation);
            }
            if (destroyOnDeath) GameObject.Destroy(gameObject);
        }



    }

    public void AddHP(float amount)
    {
        hp += amount;
        hp = Mathf.Clamp(hp, 0, hpMax);
    }
}
