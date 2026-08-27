using UnityEngine;
using UnityEngine.InputSystem;

public class TransitionToEaster : MonoBehaviour
{
    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside && InputSystem.actions["Interact"].WasPressedThisFrame())
        {
            SceneTransitionManager.Instance.TransitionToScene("EasterIslands");
        }
    }
}
