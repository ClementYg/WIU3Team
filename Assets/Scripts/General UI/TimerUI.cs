using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [Header("Timer UI")]
    [SerializeField] Transform pivotTransform;
    [SerializeField] TextMeshProUGUI text;

    public void UpdateUI(float zRotation, string timerText)
    {
        pivotTransform.rotation = Quaternion.Euler(0f, 0f, zRotation);
        text.text = timerText;
    }
}
