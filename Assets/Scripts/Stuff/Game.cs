using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game : MonoBehaviour
{
    public int Score { get; set; } = 0;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI timerUI;
    public TextMeshProUGUI highScoreUI;
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public AudioSource music;
    int highScore = 500;

    static Game instance = null;

    float timer = 12.0f;

    public enum eState
    {
        Title,
        StartGame,
        Game,
        GameOver
    }

    public eState state { get; set; } = eState.Title; 

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        switch (state)
        {
            case eState.Title:
                gameOverScreen.SetActive(false);
                startScreen.SetActive(true);
                highScoreUI.text = string.Format("{0:D4}", highScore);
                timer = 12;
                Score = 0;
                scoreUI.text = string.Format("{0:D4}", Score);
                break;
            case eState.StartGame:
                
                music.Play();
                startScreen.SetActive(false);
                state = eState.Game;
                break;
            case eState.Game:
                timer -= Time.deltaTime;
                timerUI.text = string.Format("{0:D2}", (int)timer);
                if(timer <= 0)
                {
                    music.Stop();
                    if(Score > highScore)
                    {
                        highScore = Score;
                        highScoreUI.text = string.Format("{0:D4}", highScore);
                    }
                    state = eState.GameOver;
                }
                break;
            case eState.GameOver:
                gameOverScreen.SetActive(true);
                break;
            default:
                break;
        }

        
    }

    public static Game Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddPoints(int points)
    {
        Score += points;
        scoreUI.text = string.Format("{0:D4}", Score);
        
    }


    public void StartGame()
    {
        state = eState.StartGame;
    }

    public void ToTitle()
    {
        state = eState.Title;
    }
}
