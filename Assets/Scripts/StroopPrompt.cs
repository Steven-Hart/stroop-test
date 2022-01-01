using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

namespace StroopTest
{
    public class StroopPrompt : MonoBehaviour
    {
        #region Private Serialised Fields

        [SerializeField] 
        private TextMeshProUGUI _promptText;

        private TextMeshProUGUI promptText
        {
            get
            {
                if (_promptText is null)
                {
                    throw new NullReferenceException(nameof(promptText) +" is not set on: " + gameObject.name);
                }
                return _promptText;
            }
        }

        #endregion

        #region Private Variables
        
        private StroopColor _currentColor;
        private StroopColor _currentWord;

        private readonly Dictionary<StroopColor, Color> _colorDictionary = new Dictionary<StroopColor, Color>()
        {
            {StroopColor.Red, Color.red},
            {StroopColor.Blue, Color.blue},
            {StroopColor.Yellow, Color.yellow},
            {StroopColor.Pink , Color.magenta}
        };
        
        #endregion

        #region Public Methods

        /// <summary>
        /// Generate new prompt word and color different to the previous
        /// </summary>
        public void GenerateNewPrompt()
        {
            StroopColor[] values = (StroopColor[])Enum.GetValues(typeof(StroopColor));
            Random random = new Random();

            // Pass in same random to not reset the random
            GetNewWord(values, random);
            GetNewColor(values, random);

            if (_currentWord == _currentColor)
            {
                Debug.LogError("Prompt failed to generate a different color and word");
            }
            SetPrompt();
        }
        
        public bool IsCorrectGuess(StroopColor guess)
        {
            return guess == _currentColor;
        }

        #endregion

        #region Private Methods
        
        /// <summary>
        /// Get new word excluding previous word
        /// </summary>
        /// <param name="values"></param>
        /// <param name="random"></param>
        private void GetNewWord(StroopColor[] values, Random random)
        {
            var unusableWords = new[] {_currentWord};
            var usableWords = values.Except(unusableWords).ToArray();
            var newWord = (StroopColor)usableWords.GetValue(random.Next(usableWords.Length));
            _currentWord = newWord;
        }

        /// <summary>
        /// Get new color excluding previous color and current word
        /// </summary>
        /// <param name="values"></param>
        /// <param name="random"></param>
        private void GetNewColor(StroopColor[] values, Random random)
        {
            var unusableWords = new[] {_currentColor, _currentWord};
            var usableWords = values.Except(unusableWords).ToArray();
            var newColor = (StroopColor)usableWords.GetValue(random.Next(usableWords.Length));
            _currentColor = newColor;
        }

       
        private void SetPrompt()
        {
            promptText.text = _currentWord.ToString();
            promptText.color = _colorDictionary[_currentColor];
        }
        #endregion
    }
}

