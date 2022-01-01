using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StroopResetButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        ResetStroopTest?.Invoke();
    }

    public delegate void ResetStroopTestAction();
    public static event ResetStroopTestAction ResetStroopTest;
}
