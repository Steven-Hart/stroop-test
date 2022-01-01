using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StroopTotalScore : MonoBehaviour
{

    #region Private Serialised Fields

    [SerializeField]
    private TextMeshProUGUI _totalScoreText;
    private  TextMeshProUGUI totalScoreText
    {
        get
        {
            if (_totalScoreText is null)
            {
                throw new NullReferenceException(nameof(totalScoreText) + " is not set on " + gameObject.name);
            }
            return _totalScoreText;
        }
    }

    #endregion
    
    #region Private Variables

    private int _totalScore;
    private int TotalScore
    {
        set
        {
            _totalScore = value;
            totalScoreText.text = "Total Score: " + _totalScore.ToString();
        }
    }

    #endregion

    #region Private Methods
    private void OnEnable()
    {
        AddEventListeners();
    }

    private void OnDisable()
    {
        RemoveEventListeners();
    }

    private void SetTotalScore(int totalScore)
    {
        TotalScore = totalScore;
    }

    private void AddEventListeners()
    {
        StroopScore.SendTotalScore += SetTotalScore;
    }

    private void RemoveEventListeners()
    {
        StroopScore.SendTotalScore -= SetTotalScore;
    }
    #endregion
}
