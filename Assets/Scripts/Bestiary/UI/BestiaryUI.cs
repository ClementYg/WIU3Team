using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] BestiaryDatabase database;

    [Header("Grid")]
    [SerializeField] Transform gridContent; //where to put 
    [SerializeField] BestiaryGrid gridPrefab;

    [Header("Details")]
    [SerializeField] RectTransform detailPanelRect;
    [SerializeField] Image detailIcon;
    [SerializeField] TMP_Text detailName;
    [SerializeField] TMP_Text detailDesc;
    [SerializeField] TMP_Text detailCategory;
    [SerializeField] GameObject detailLockedState;

    List<GameObject> newCells = new();

    bool initialised = false;

    [Header("Detail Panel Slide")]
    [SerializeField] float animDuration = 0.3f; 
    bool isPanelShown = false;
    Coroutine animCoroutine;

    [Header("Event Channels")]
    [SerializeField] private EventBool OnBestiaryToggledEvent;

    private void Awake()
    {
        if (detailPanelRect != null)
        {
            detailPanelRect.localScale = new Vector3(0f, 1f, 1f);
        }

    }
    private void Start()
    {
        ShowAll();
        initialised = true;
    }
    //Everytime bring up the bestiary, show everything first (ALL tab)
    private void OnEnable()
    {
        if (detailPanelRect != null)
        {
            if (animCoroutine != null) StopCoroutine(animCoroutine);
            detailPanelRect.localScale = new Vector3(0f, 1f, 1f);
            isPanelShown = false;
        }

        OnBestiaryToggledEvent.Subscribe(OnToggledBestiary);
        if (initialised) ShowAll();
    }
    private void OnDisable()
    {
        OnBestiaryToggledEvent.Unsubscribe(OnToggledBestiary);
    }
    //This ? just allows category to be null incase no category
    void ShowCategory(BestiaryCategory? category)
    {
        ClearGrid();

        List<BestiaryEntry> entries = category.HasValue ? database.GetByCategory(category.Value) : database.GetAllEntries();
        foreach (BestiaryEntry entry in entries)
        {
            BestiaryGrid cell = Instantiate(gridPrefab, gridContent);
            bool unlocked = BestiaryManager.Instance.IsUnlocked(entry.EntryID);
            cell.Setup(entry, unlocked, () => ShowDetails(entry, unlocked));
            newCells.Add(cell.gameObject);
        }
    }

    public void ShowAll() => ShowCategory(null);
    public void ShowAreas() => ShowCategory(BestiaryCategory.Area);
    public void ShowItems() => ShowCategory(BestiaryCategory.Item);
    public void ShowEnemies() => ShowCategory(BestiaryCategory.Enemy);
    public void ShowLore() => ShowCategory(BestiaryCategory.Lore);

    void ClearGrid()
    {
        foreach (GameObject cell in newCells)
        {
            Destroy(cell);
        }
        newCells.Clear();
    }

    private void OnToggledBestiary(bool isEnabled)
    {
        if (isEnabled)
        {
            ShowAll();
        }
    }
    void ShowDetails(BestiaryEntry entry, bool unlocked)
    {
        if (detailLockedState != null) detailLockedState.SetActive(!unlocked);
        detailIcon.gameObject.SetActive(unlocked);
        detailName.gameObject.SetActive(unlocked);
        detailDesc.gameObject.SetActive(unlocked);
        detailCategory.gameObject.SetActive(unlocked);

        if (!unlocked) return;

        detailIcon.sprite = entry.Icon;
        detailName.text = entry.DisplayName;
        detailDesc.text = entry.Description;
        detailCategory.text = entry.Category.ToString();
        if (!isPanelShown) OpenPanel();
    }

    void OpenPanel()
    {
        isPanelShown = true;
        if (animCoroutine != null) StopCoroutine(animCoroutine);
        animCoroutine = StartCoroutine(ScalePanel(0f, 1f));
    }

    // Wire this to the X (close) button's OnClick, and to a backdrop button behind
    // everything else for "click outside the panel closes it."
    public void CloseDetailPanel()
    {
        if (!isPanelShown) return;

        isPanelShown = false;
        if (animCoroutine != null) StopCoroutine(animCoroutine);
        animCoroutine = StartCoroutine(ScalePanel(1f, 0f));
    }


    IEnumerator ScalePanel(float fromX, float toX)
    {
        float elapsed = 0f;
        while (elapsed < animDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / animDuration);
            float x = Mathf.Lerp(fromX, toX, t);
            detailPanelRect.localScale = new Vector3(x, 1f, 1f);
            yield return null;
        }
        detailPanelRect.localScale = new Vector3(toX, 1f, 1f);
    }


}