using UnityEngine;

[CreateAssetMenu(fileName = "AttackPotionItemEffect", menuName = "Scriptable Objects/Inventory/Effects/AttackPotionItemEffect")]
public class AttackPotionItemEffect : ItemEffect
{
    [SerializeField] int attackBoostAmount = 10;

    public override void Use(GameObject user, AlertManager alertMan)
    {
        AttackChecker atkChecker = user.GetComponent<AttackChecker>();
        if (!atkChecker) return;

        atkChecker.damageAmount += attackBoostAmount;
        alertMan.ShowAlert("Increased damage to " + atkChecker.damageAmount + "!");
    }
}
