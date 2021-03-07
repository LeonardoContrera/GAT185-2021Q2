using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreHolder : MonoBehaviour
{
    public int TotalScore { get; set; }
    public TextMeshProUGUI uiScore;
    //public int score = 200;


    static ScoreHolder instance = null;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        uiScore.text = string.Format("{0:D4}", TotalScore);
        //score = TotalScore;
    }

    public static ScoreHolder Instance
    {
        get
        {
            return instance;
        }
    }

    public void AddScore(int amount)
    {
        TotalScore += amount;
        uiScore.text = string.Format("{0:D4}", TotalScore);
    }
}
