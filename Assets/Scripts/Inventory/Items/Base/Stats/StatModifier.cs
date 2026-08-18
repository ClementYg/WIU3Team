
//System Serializable struct, similar to S.S class

[System.Serializable]
public struct StatModifier
{
    public StatType statType;
    public float value;
    //just show the stat Type and what value it is.
    //e.g +8 Health or +10 WeaponDmg
    public string GetDisplayText()
    {
        string sign = value >= 0 ? "+" : "";
        return $"{sign}{value} {statType}";
    }
}
