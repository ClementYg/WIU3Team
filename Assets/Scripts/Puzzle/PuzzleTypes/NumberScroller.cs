using TMPro;
using UnityEngine;

public class NumberScroller : MonoBehaviour
{
    [Header("Number Settings")]
    [SerializeField] private int maxNumber = 10;
    [SerializeField] private int minNumber = 0;

    [SerializeField] TextMeshProUGUI textGUI;
    private int currentNumber = 0;
    private void Start()
    {
        UpdateUI();
    }
    public void ScrollNumber()
    {
        if (currentNumber < maxNumber)
        {
            currentNumber++;
        }
        else
        {
            currentNumber = minNumber;
        }
        UpdateUI();
    }

    public int GetCurrentNumber()
    {
        return currentNumber; 
    }
    void UpdateUI()
    {
        textGUI.text = currentNumber.ToString();
    }
}
