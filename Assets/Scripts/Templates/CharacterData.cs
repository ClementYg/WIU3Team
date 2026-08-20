using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Dialogue/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Character Details")]
    public string characterName;
    public Color textColor = Color.white;

    [Header("Blip Sound")]
    public AudioClip blipSound;
    public Vector2 pitchRange = new Vector2(0.95f, 1.05f);
    [Range(1, 4)] public int blipEveryNCharacters = 2;
}
