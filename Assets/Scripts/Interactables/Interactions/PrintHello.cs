using UnityEngine;

[CreateAssetMenu(fileName = "PrintHello", menuName = "ScriptableObjects/Interaction/PrintHello")]
public class PrintHello : Interaction
{
    public override void Do()
    {
        Debug.Log("Hello!");
    }
}
