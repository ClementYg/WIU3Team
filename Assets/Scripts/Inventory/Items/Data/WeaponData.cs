using UnityEngine;

//this is js a example of making a new itemData
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Inventory/Items/WeaponData")]
public class WeaponData : ItemData
{

    public GameObject weaponPrefab;
    public float attackCooldown = 0.5f;

    private void Reset()
    {
        itemType = ItemType.Weapon;
        hasDurability = true;
    }

    public override string GetToolTip()
    {
        string text = base.GetToolTip();
        text += $"\nAttack Speed: {1f / attackCooldown:0.0}/s";
        return text;
    }
}
