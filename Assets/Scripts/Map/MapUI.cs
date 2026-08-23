using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] BestiaryDatabase database; //reuse for area

    [Header("Map")]
    [SerializeField] Transform mapContent; 
    [SerializeField] MapAreaIcon iconPrefab;

    List<GameObject> spawnedIcons = new();

    public void RefreshMap()
    {
        //destroy existing icons etc
        foreach (GameObject icon in spawnedIcons) Destroy(icon);
        spawnedIcons.Clear();

        //rebuild it here by checking if its unlocked
        foreach (AreaEntryData area in database.areaEntries)
        {
            bool discovered = BestiaryManager.Instance.IsUnlocked(area.EntryID);

            MapAreaIcon icon = Instantiate(iconPrefab, mapContent);
            icon.Setup(area, discovered);
            spawnedIcons.Add(icon.gameObject);
        }
    }
}
