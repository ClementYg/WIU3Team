using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [Header("Press Settings")]
    [SerializeField] private int minOccupant = 1;
    [SerializeField] private float pressDepth = 0.1f;
    [SerializeField] private float pressSpeed = 10f;
    [SerializeField] private int pressDirection = 0; // 0 for down, 1 for up, 2 for left, 3 for right

    [Header("Interactions")]
    [SerializeField] private List<Interaction> pressedInteractions;
    [SerializeField] private List<Interaction> releasedInteractions;
    [SerializeField] private UnityEvent unityEvents;

    private Vector2 restPosition;
    private Vector2 pressedPosition;
    private Rigidbody2D rb;
    private bool isPressed = false;
    private int occupantCount = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        restPosition = rb.position;
        Vector2 direction = pressDirection switch
        {
            0 => Vector2.down,
            1 => Vector2.up,
            2 => Vector2.left,
            3 => Vector2.right,
            _ => Vector2.down
        };

        pressedPosition = restPosition + direction * pressDepth;
    }

    void FixedUpdate()
    {
        Vector2 target = isPressed ? pressedPosition : restPosition;
        rb.MovePosition(Vector2.Lerp(rb.position, target, pressSpeed * Time.fixedDeltaTime));
    }

    public void Press()
    {
        occupantCount++;
        if (occupantCount >= minOccupant)
        {
            isPressed = true;
            foreach (Interaction interaction in pressedInteractions)
            {
                interaction.Do();
            }

            unityEvents.Invoke();
        }
    }

    public void Release()
    {
        occupantCount--;
        if (occupantCount < minOccupant)
        {
            isPressed = false;
            foreach (Interaction interaction in releasedInteractions)
            {
                interaction.Do();
            }
        }
    }
}
