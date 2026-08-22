using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Data/Player")]
public class PlayerData : EntityData
{
    [Header("Player Data")]
    public float baseMaxWalkSpeed = 3;
    public float baseMaxSprintSpeed = 7;
}
