using UnityEngine;

//this is js a example of making a new itemData
[CreateAssetMenu(fileName = "QuestItemData", menuName = "ScriptableObjects/Items/QuestItemData")]
public class QuestItemData : ItemData
{
    [Header("Quest Info")]
    public string givenBy;
    //I just placed it here precautionarily but can be used 
    //for future quest system to track if this item is for that quest
    public int questID = -1;
    //we can add this to disable delete or self-drop by player
    //i.e, lock item 
    public bool isKeyItem = false; 

    private void Reset()
    {
        itemType = ItemType.QuestItem;
        hasDurability = false;
    }
}
