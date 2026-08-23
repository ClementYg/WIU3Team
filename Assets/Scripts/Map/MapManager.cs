using UnityEngine;

public class MapManager : PersistentSingleton<MapManager>
{
    [Header("Map UI")]
    [SerializeField] GameObject mapUIPrefab; //map with canvas UI bg and icon 

    MapUI mapUI;
    GameObject mapUiInstance;

    protected override void Awake()
    {
        base.Awake();
        if (_instance != this) return;

        if (mapUIPrefab != null)
        {
            mapUiInstance = Instantiate(mapUIPrefab, transform);
            mapUI = mapUiInstance.GetComponentInChildren<MapUI>();
            mapUiInstance.SetActive(false);
        }
    }

    public void ToggleMap()
    {
        if (mapUiInstance == null) return;
        bool open = !mapUiInstance.activeSelf;
        mapUiInstance.SetActive(open);
        if (open)
        {
            mapUI.RefreshMap();
        }
    }

    public void OpenMap()
    {
        if (mapUiInstance == null) return;
        mapUiInstance.SetActive(true);
        mapUI.RefreshMap();
    }

    public void CloseMap()
    {
        if (mapUiInstance == null) return;
        mapUiInstance.SetActive(false);
    }
}
