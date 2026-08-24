using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Puzzles/Clock Layout")]
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