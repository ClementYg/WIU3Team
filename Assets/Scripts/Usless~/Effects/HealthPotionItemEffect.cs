using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotionItemEffect", menuName = "Scriptable Objects/Inventory/Effects/HealthPotionItemEffect")]
public class HealthPotionItemEffect : ItemEffect
{
    [SerializeField] int healAmount = 0;

    public override void Use(GameObject user, AlertManager alertMan)
    {
        Damageable health = user.GetComponent<Damageable>();
        if (!health) return;

        health.Heal(healAmount);
        alertMan.ShowAlert("Healed " + healAmount + " health!");
    }
}
