using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndStatue : MonoBehaviour
{
    [SerializeField] UIFader fader;
    [SerializeField] CameraShaker shaker;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float fadeTime = 3f;

    private bool playerInTrigger = false;
    private bool ending = false;

    private void Start()
    {
        shaker = GameObject.Find("Player").GetComponent<CameraShaker>();
    }

    private void Update()
    {
        if (playerInTrigger &&
            !ending &&
            InputSystem.actions["Interact"].WasPressedThisFrame())
        {
            StartCoroutine(EndSequence());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private IEnumerator EndSequence()
    {
        ending = true;

        shaker.DoShake();

        yield return new WaitForSeconds(waitTime);

        fader.FadeIn();

        yield return new WaitForSeconds(fadeTime);

        QuitGame();
    }

    private void QuitGame()
    {
        Debug.Log("Ending game.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}