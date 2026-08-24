using System.Collections.Generic;
using UnityEngine;

public class BestiaryManager : PersistentSingleton<BestiaryManager>
{
    //HashSet has a dictionary, with functions contains(), add(), remove().
    //looks through specifically, doesnt check one by one 
    HashSet<string> unlockedEntryIDs = new();

    public bool IsUnlocked(string entryID)
    {
        return unlockedEntryIDs.Contains(entryID);
    }

    public bool Unlock(string entryID)
    {
        return unlockedEntryIDs.Add(entryID);
    }

    public float GetCompletionPercent(BestiaryDatabase database)
    {
        List<BestiaryEntry> all = database.GetAllEntries();
        if (all.Count == 0) return 0f;

        int unlockedCount = all.FindAll(e => IsUnlocked(e.EntryID)).Count;
        return (float)unlockedCount / all.Count;
    }

    //if we ever want savce/load but not rlly important now
    public List<string> GetUnlockedIDs()
    {
        return new List<string>(unlockedEntryIDs);
    }

}