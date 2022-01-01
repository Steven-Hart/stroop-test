using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StroopTest
{
    public class StroopTest : MonoBehaviour
    {
        
        private const int TotalGuessLimit = 8;

        #region Private Serialised Fields
        [SerializeField]
        private StroopPrompt _stroopPrompt;
        private StroopPrompt stroopPrompt
        {
            get
            {
                if (_stroopPrompt is null)
                {
                    throw new NullReferenceException(nameof(stroopPrompt) + "is not set on" + gameObject.name);
                }
                return _stroopPrompt;
            }
        }
        
        [SerializeField]
        private StroopScore _stroopScore;
        private StroopScore stroopScore
        {
            get
            {
                if (_stroopScore is null)
                {
                    throw new NullReferenceException(nameof(stroopScore) + "is not set on" + gameObject.name);
                }
                return _stroopScore;
            }
        }
        
        [SerializeField]
        private GameObject _gameScreen;

        private GameObject gameScreen
        {
            get
            {
                if (_gameScreen is null)
                {
                    throw new NullReferenceException(nameof(gameScreen) + "is not set on" + gameObject.name);
                }
                return _gameScreen;
            }
        }
        
        [SerializeField]
        private GameObject _resultScreen;

        private GameObject resultScreen
        {
            get
            {
                if (_stroopPrompt is null)
                {
                    throw new NullReferenceException(nameof(resultScreen) + "is not set on" + gameObject.name);
                }
                return _resultScreen;
            }
        }
        #endregion
        
        #region Private Variables

        private int totalGuesses = 0;
        private bool isPlaying { get; set; } = false;

        #endregion

        #region Private Methods
        // Start is called before the first frame update
        private void Start()
        {
            Reset();
            AddEventListeners();
        }

        private void StartGame()
        {
            if (isPlaying) return;
            isPlaying = true;
            stroopScore.StartScoring();
        }

        private void EndGame()
        {
            isPlaying = false;
            ShowResultScreen();
        }

        private void Reset()
        {
            isPlaying = false;
            totalGuesses = 0;
            ShowGameScreen();
        }

        private void ShowGameScreen()
        {
            gameScreen.SetActive(true);
            resultScreen.SetActive(false);
            stroopPrompt.GenerateNewPrompt();
        }

        private void ShowResultScreen()
        {
            gameScreen.SetActive(false);
            resultScreen.SetActive(true);
            
            stroopScore.OnEndOfGame();
        }

        private void AddEventListeners()
        {
            StroopButton.OnButtonPressed += OnButtonPress;
            StroopResetButton.ResetStroopTest += Reset;
        }

        private void RemoveEventListeners()
        {
            StroopButton.OnButtonPressed -= OnButtonPress;
            StroopResetButton.ResetStroopTest -= Reset;
        }

        private void IncrementTotalGuess()
        {
            totalGuesses++;
            if (totalGuesses >= TotalGuessLimit)
            {
                EndGame();
            }
        }

        /// <summary>
        /// Starts game or if the game is already started, adds score and counts guess.
        /// </summary>
        /// <param name="buttonColor"></param>
        private void OnButtonPress(StroopColor buttonColor)
        {
            if (isPlaying)
            {
                stroopScore.AddScore(stroopPrompt.IsCorrectGuess(buttonColor));
                IncrementTotalGuess();
            }
            else
            {
                StartGame();
            }
            stroopPrompt.GenerateNewPrompt();
        }
        #endregion
    }
}

