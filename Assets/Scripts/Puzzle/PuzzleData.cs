using UnityEngine;

[CreateAssetMenu(fileName = "PuzzleData", menuName = "Scriptable Objects/Puzzles/PuzzleData")]
public class PuzzleData : ScriptableObject
{
    [Header("Identity")]
    public string puzzleID;
    public string puzzleName;
    [TextArea(2, 4)] public string puzzleDescription;

    [Header("Reward (optional)")]
    public ItemData rewardItem;
}   