using TMPro;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UI;

public class EnterCodePuzzle : ScreenPuzzle
{
    //Note to self, need to make it work with more numbers and can check any 
    [SerializeField] int[] correctCodes;
    [SerializeField] NumberScroller[] digits;
    public void CheckCode()
    {
        if (correctCodes.Length != digits.Length)
        {
            Debug.Log($"(ECP) Number of Correct Codes [{correctCodes.Length}] does not match Number of Digits [{digits.Length}]");
        }

        int ansCorrect = 0;
        for (int i = 0; i < digits.Length; ++i)
        {
            if (digits[i] != null && digits[i].GetCurrentNumber() == correctCodes[i])
            {
                //Entered Correctly
                ansCorrect++;
            }
        }

        if (ansCorrect == correctCodes.Length)
        {
            Debug.Log("Correct");
            CompletePuzzle();
        }    
    }
}
