using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public EventInt OnPlayerHPChange;
    public Image HP;
    public int HPThreshold;

    private void OnEnable()
    {
        OnPlayerHPChange.Subscribe(UpdateHP);
    }

    private void OnDisable()
    {
        OnPlayerHPChange.Unsubscribe(UpdateHP);
    }

    void UpdateHP(int newHP)
    {
        Debug.Log(newHP);
        if (newHP >= HPThreshold)
        {
            HP.color = Color.white;
        }
        else
        {
            HP.color = Color.black;
        }
    }
}
