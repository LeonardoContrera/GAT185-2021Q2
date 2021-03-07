using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerGame : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public TextMeshProUGUI scoreUI;
    public GameObject titleScreen;
    public GameObject gameoverScreen;
/*    public AudioClip bgm;
    public AudioClip ambient;*/
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public Character character;
    private float decScore;
    private int adScore;
    public int rate;

    public enum eState
    {
        Title,
        GameStart,
        GameOver
    }
    public static eState state { get; set; } = eState.Title;

    private void Start()
    {
        //health = GetComponent<Health>();
        //audioSource = gameObject.GetComponent<AudioSource>();
    }
    void Update()
    {
        switch (state)
        {
            case eState.Title:
                gameoverScreen.SetActive(false);
                titleScreen.SetActive(true);
                Score = 0;
               // audioSource1.Stop();
                //audioSource1.Play();
                character.health.hp = character.health.hpMax;
                //character.health = 
                scoreUI.text = string.Format("{0:D4}", Score);
                
                break;
            case eState.GameStart:
                titleScreen.SetActive(false);
               // audioSource1.Stop();
               // audioSource2.Play();
                decScore += (1 * Time.deltaTime) / 4;
                adScore = Mathf.RoundToInt(decScore);
                AddScore(adScore );
                if(character.health.hp <= 0)
                {
                    state = eState.GameOver;
                }
                break;
            case eState.GameOver:
                gameoverScreen.SetActive(true);
               // audioSource2.Stop();
               // audioSource1.Play();
                break;
            default:
                break;
        }


    }

    public void AddScore(int amount)
    {
        Score += amount;
        scoreUI.text = string.Format("{0:D4}", Score);
    }

    public void StartGame()
    {
        state = eState.GameStart;
    }

    public void ToTitle()
    {
        state = eState.Title;
    }


/*    public void ChangeBGM(AudioClip audioClip)
    {
        audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }*/


}
