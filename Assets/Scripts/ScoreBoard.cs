using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;
    public int Health { get; private set; } = 100;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score:0 | Health:100";
    }
    public void IncreaseScore(int points)
    {
        score += points;
        UpdateBoard();
    }

    void UpdateBoard()
    {
        scoreText.text = $"Score:{score.ToString()} | Health:{Health.ToString()}";
    }

    public void DecreaseHealth(int damage)
    {
        Health -= damage;
        UpdateBoard();
    }
}
