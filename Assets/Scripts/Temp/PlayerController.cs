using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] Rigidbody2D rb;

    [Header("Modifiers")]
    [SerializeField] float moveSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        float finalVelX = 0f;

        bool isRightPressed = Keyboard.current.dKey.isPressed;
        bool isLeftPressed = Keyboard.current.aKey.isPressed;

        if (isRightPressed) finalVelX += moveSpeed;
        if (isLeftPressed) finalVelX -= moveSpeed;

        rb.linearVelocityX = finalVelX;
    }
}
