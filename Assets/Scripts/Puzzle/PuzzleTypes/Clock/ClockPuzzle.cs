using UnityEngine;

public class ClockPuzzle : ScreenPuzzle
{
    //REMEMBER THE LAYOUT AND THE CLOCKS MUST MATCH IN ORDER WITHIN EACH OF THE CLOCKS
    //Index 0 Clock ==> Index 0 Layout Clock Item
    [SerializeField] ClockUnit[] clocks;       // drag however many clock prefab instances are in this canvas
    [SerializeField] ClockLayout layout; // defines each clock's correct time

    void Awake()
    {
        if (layout == null || layout.targets.Length != clocks.Length)
        {
            Debug.LogError($"(CP) Layout target count [{layout?.targets.Length}] doesn't match clock count [{clocks.Length}]");
            return;
        }

        for (int i = 0; i < clocks.Length; i++)
            clocks[i].SetTarget(layout.targets[i].hour, layout.targets[i].minute);
    }
#if UNITY_EDITOR
    void OnValidate()
    {
        if (layout != null && clocks != null && layout.targets.Length != clocks.Length)
        {
            Debug.LogWarning($"(CP) [{name}] Clock count [{clocks.Length}] doesn't match Layout target count [{layout.targets.Length}]. Fix before playing.", this);
        }
    }
#endif

    void OnEnable()
    {
        foreach (var c in clocks)
        {
            c.OnSolvedEvent?.Subscribe(HandleClockSolved);
        }
    }

    void OnDisable()
    {
        foreach (var c in clocks)
        {
            c.OnSolvedEvent?.Unsubscribe(HandleClockSolved);
        }
    }

    void HandleClockSolved(ClockUnit c) => CheckAll();

    void CheckAll()
    {
        if (isCompleted) return;
        foreach (var c in clocks)
            if (!c.IsSolved) return;

        CompletePuzzle();
    }
}