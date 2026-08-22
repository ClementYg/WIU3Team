using UnityEngine;

[CreateAssetMenu(menuName = "Puzzles/Clock Layout")]
public class ClockLayout : ScriptableObject
{
    [System.Serializable]
    public struct ClockTarget
    {
        public int hour;
        public int minute;
    }

    public ClockTarget[] targets; //can change amt of clocks
}