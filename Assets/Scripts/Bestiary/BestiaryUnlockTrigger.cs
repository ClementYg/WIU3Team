using Unity.Cinemachine;
using UnityEngine; 

public class BestiaryUnlockTrigger : MonoBehaviour
{
    [SerializeField] ScriptableObject entryAsset;
    [SerializeField] string playerTag = "Player";
    [SerializeField] bool destroyAfterUnlock = false; //can be a one-time pick up trigger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(playerTag)) return; 
        if (entryAsset == null) return;

        if (entryAsset is BestiaryEntry entry)
        {
            BestiaryManager.Instance.Unlock(entry.EntryID);
            if (destroyAfterUnlock) Destroy(gameObject);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (entryAsset != null && entryAsset is not BestiaryEntry)
        {
            Debug.LogWarning($"(BUT) Trigger on '{name}': Assigned asset '{entryAsset.name}'"
                + $"does not have BestiaryEntry, so nothing happens", this);
        }
    }
#endif
}