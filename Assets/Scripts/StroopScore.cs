using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StroopScore : MonoBehaviour
{
    private const int DEFAULT_SCORE = 1000;

    #region Private Serialised Fields
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private TextMeshProUGUI scoreText
    {
        get
        {
            if (_scoreText is null)
            {
                throw new NullReferenceException(nameof(scoreText) + " is not set on " + gameObject.name);
            }
            return _scoreText;
        }
    }
    

    #endregion

    #region Private Variables

    private int totalScore;
    
    private int _score;
    private int Score
    {
        get => _score;
        set
        {
            _score = value;
            scoreText.text = _score.ToString();
        }
    }

    private float timer;

    private bool isScoring;

    #endregion

    #region Private Methods

    private void ResetScore()
    {
        StopScoring();
        Score = DEFAULT_SCORE;
        timer = 0;
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Add score to total and start next score
    /// </summary>
    /// <param name="isCorrect"></param>
    public void AddScore(bool isCorrect)
    {
        if (isCorrect)
        {
            totalScore += Score;
        }
        ResetScore();
        StartScoring();
    }

    public void StartScoring()
    {
        isScoring = true;
    }

    /// <summary>
    /// Sends total score to UI and resets score.
    /// </summary>
    public void OnEndOfGame()
    {
        SendTotalScore?.Invoke(totalScore);
        totalScore = 0;
        ResetScore();
    }

    private void StopScoring()
    {
        isScoring = false;
    }

    public void Start()
    {
        ResetScore();
    }

    public void Update()
    {
        if (!isScoring) return;
        timer += Time.deltaTime;
        int numberOfSeconds = 4;
        if ((timer < 1f)) return;
        // Reduce score by fraction of total seconds allowed
        Score -= DEFAULT_SCORE/numberOfSeconds;
        timer = 0;
        if (Score > 0) return;
        Score = 0;
        StopScoring();
    }
    #endregion

    #region Events
    #region Delegates
    public delegate void SendTotalScoreAction(int totalScore);

    #endregion
    public static event SendTotalScoreAction SendTotalScore;
    #endregion

}
