using UnityEngine;

[CreateAssetMenu(fileName = "AtlasItemEffect", menuName = "ScriptableObjects/Inventory/Effects/AtlasItemEffect")]
public class AtlasItemEffect : ItemEffect
{
    public override void Use(GameObject user, ComponentCache userCache)
    {
        TimeSwitchManager timeSwitchMan = userCache.Get<TimeSwitchManager>();
        if (timeSwitchMan == null) return;

        timeSwitchMan.UseAtlas();
    }
}
