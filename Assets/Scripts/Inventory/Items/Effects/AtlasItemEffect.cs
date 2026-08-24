using UnityEngine;

[CreateAssetMenu(fileName = "AtlasItemEffect", menuName = "ScriptableObjects/Inventory/Effects/AtlasItemEffect")]
public class AtlasItemEffect : ItemEffect
{
    public override void Use(GameObject user, ComponentCache userCache)
    {
        TimeSwitch tmSwitch = userCache.Get<TimeSwitch>();
        if (tmSwitch == null) return;

        tmSwitch.UseAtlas();
    }
}
