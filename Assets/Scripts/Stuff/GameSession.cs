using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI highScoreUI;
    public GameObject startScreen;
    public GameObject gameOverScreen;

    static GameSession instance = null;
    int highScore = 500;

    float timer = 0;
    public static GameSession Instance
    {
        get
        {
            return instance;
        }
    }

    public enum eState
    {
        StartSession,
        Session,
        EndSession,
        GameOver
    }

    public eState state { get; set; } = eState.StartSession; 

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //GameController.Instance.transition.StartTransition(Color.clear, 1);
        EventManager.Instance.Subscribe("PlayerDead", OnPlayerDead);
    }

    void Update()
    {
        switch (state)
        {
            case eState.StartSession:
                Score = 0;
                EventManager.Instance.TriggerEvent("StartSession");
                GameController.Instance.transition.StartTransition(Color.clear, 1);
                state = eState.Session;
                break;
            case eState.Session:
                break;
            case eState.EndSession:
                timer -= Time.deltaTime;
                if(timer <= 0)
                {
                    GameObject gameObject = GameObject.FindGameObjectWithTag("PlayerPackage");
                    Destroy(gameObject);
                    state = eState.StartSession;
                }
                break;
            case eState.GameOver:
                break;
            default:
                break;
        }

        
    }



    public void AddPoints(int points)
    {
        Score += points;
        scoreUI.text = string.Format("{0:D4}", Score);
        
    }

    public void OnPlayerDead()
    {
        GameController.Instance.transition.StartTransition(Color.black, 1);
        timer = 2;
        state = eState.EndSession;
    }

    /*    public void ToTitle()
        {
            state = eState.Title;
        }*/
}
