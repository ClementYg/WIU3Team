using UnityEngine;
using UnityEngine.InputSystem;

public class TimeStatue : MonoBehaviour
{
    [SerializeField] private Puzzle puzzleCondition;
    [SerializeField] private TimeSwitch playerTmSwitch;
    [SerializeField] private bool needPuzzle = true;

    private void Update()
    {
        if (needPuzzle && puzzleCondition != null && 
            InputSystem.actions["Interact"].WasPressedThisFrame() 
            && puzzleCondition.isCompleted)
        {
            playerTmSwitch.isTimeSwitchEnabled = true;
            playerTmSwitch.UseAtlas();
            playerTmSwitch.isTimeSwitchEnabled = false;
        }
        else if (InputSystem.actions["Interact"].WasPressedThisFrame() 
            && !needPuzzle)
        {
            playerTmSwitch.isTimeSwitchEnabled = true;
            playerTmSwitch.UseAtlas();
            playerTmSwitch.isTimeSwitchEnabled = false;
        }
    }
}
