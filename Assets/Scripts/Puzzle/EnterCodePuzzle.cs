using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnterCodePuzzle : ScreenPuzzle
{
    [Header("Code")]
    [SerializeField] string correctCode = "1234";
    [SerializeField] TMP_InputField inputCodeField; 

    public void EnterCode()
    {
        if (inputCodeField.text == correctCode)
        {
            CompletePuzzle();
        }
        else
        {
            Debug.Log("[ECP] Incorrect Code\n");
            inputCodeField.text = ""; //reset input
        }
    }
}
