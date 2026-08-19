using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/Data/Entity")]
public class EntityData : ScriptableObject
{
    // The purpose of this ScriptableObject is to provide a base template
    // for what data/stats an entity (e.g. Player, Enemy) might want in common

    [Header("General Entity Data")]
    public string entityName;
    public float maxHP;
    public float baseDamage;
}
