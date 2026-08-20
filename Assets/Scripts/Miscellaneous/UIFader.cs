using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class UIFader : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float fadeAlpha = 1f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private bool startVisible = false;

    private CanvasGroup canvasGroup;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = startVisible ? fadeAlpha : 0f;
        SetInteractable(startVisible);
    }

    public void FadeIn(System.Action onComplete = null) => StartFade(fadeAlpha, onComplete);
    public void FadeOut(System.Action onComplete = null) => StartFade(0f, onComplete);

    private void StartFade(float targetAlpha, System.Action onComplete)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha, onComplete));
    }

    private IEnumerator FadeRoutine(float targetAlpha, System.Action onComplete)
    {
        float startAlpha = canvasGroup.alpha;
        float elapsed = 0f;

        if (targetAlpha > startAlpha) SetInteractable(true);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (targetAlpha < startAlpha) SetInteractable(false);

        fadeRoutine = null;
        onComplete?.Invoke();
    }

    private void SetInteractable(bool value)
    {
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }
}
