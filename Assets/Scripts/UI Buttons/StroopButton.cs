using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StroopTest
{
    public class StroopButton : MonoBehaviour
    {
        #region Private Serialised Fields
        [SerializeField]
        private StroopColor buttonColor;

        [SerializeField]
        private TextMeshProUGUI _buttonText;
        private TextMeshProUGUI buttonText
        {
            get
            {
                if (_buttonText is null)
                {
                    throw new NullReferenceException(nameof(buttonText) +" is not set on" + gameObject.name);
                }
                return _buttonText;
            }
        }
        #endregion

        #region Private Variables
        [HideInInspector]
        private Button _button;
        #endregion

        #region Private Methods

        private void GetButton()
        {
            _button = gameObject.GetComponent<Button>();
            if (_button is null)
            {
                Debug.LogError("Failed to find expected button at: " + gameObject.name);
            }
        }

        private void UpdateText()
        {
            buttonText.text = buttonColor.ToString();
        }
    
        // Start is called before the first frame update
        private void Start()
        {
            GetButton();
            UpdateText();
        }
        #endregion

        #region Public Methods
        public void ButtonPressed()
        {
            OnButtonPressed?.Invoke(buttonColor);
            Debug.Log(buttonColor + " button pressed");
        }
        
        #endregion

        #region Events

        #region Delegates
        public delegate void ButtonPressedAction(StroopColor buttonColor);
        #endregion
        public static event ButtonPressedAction OnButtonPressed;

        #endregion

    }
}

