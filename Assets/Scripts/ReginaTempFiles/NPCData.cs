using UnityEngine;

[CreateAssetMenu(fileName = "NPCData", menuName = "ScriptableObjects/NPCs/NPCData")]
public class NPCData : ScriptableObject
{
    [Header("Details")]
    [SerializeField] string npcName;
}
