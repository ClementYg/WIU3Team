using UnityEngine;

[CreateAssetMenu(fileName = "DiaryItemEffect", menuName = "ScriptableObjects/Inventory/Effects/DiaryItemEffect")]
public class DiaryItemEffect : ItemEffect
{
    public override void Use(GameObject user, ComponentCache userCache)
    {
        Debug.Log("DiaryItemEffect: diary used");
    }
}
