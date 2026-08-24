using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public enum ButtonType
    {
        GROUND,
        WALL
    }

    [Header("Dependencies")]
    [SerializeField] private GameObject buttonTop;

    [Header("Event Channels")]
    [SerializeField] private EventGameObject onButtonPressedEvent;
    
    [Header("Press Settings")]
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private float pressDepth = 0.1f;
    [SerializeField] private float pressSpeed = 10f;
    [SerializeField] private int pressDirection = 0; // 0 for down, 1 for up, 2 for left, 3 for right

    private Vector2 restPosition;
    private Vector2 pressedPosition;
    private Rigidbody2D rb;
    private bool pressedOnce = false;
    private Vector3 initialScale;
    private Vector3 finalScale;

    private void OnEnable()
    {
        onButtonPressedEvent.Subscribe(OnButtonPressed);
    }

    private void OnDisable()
    {
        onButtonPressedEvent.Unsubscribe(OnButtonPressed);
    }

    private void Awake()
    {
        if (buttonType == ButtonType.GROUND)
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
        else if (buttonType == ButtonType.WALL)
        {
            initialScale = buttonTop.transform.localScale;
            finalScale = new Vector3(initialScale.x - 0.2f, initialScale.y - 0.2f, initialScale.z);
        }
    }

    private void Update()
    {
        if (buttonType != ButtonType.WALL) return;
        
        Vector3 currentScale = buttonTop.transform.localScale;
        if (pressedOnce)
        {
            buttonTop.transform.localScale = new Vector3(currentScale.x - 0.01f, currentScale.y - 0.01f, currentScale.z);
            if (buttonTop.transform.localScale.sqrMagnitude <= finalScale.sqrMagnitude)
            {
                buttonTop.transform.localScale = finalScale;
                pressedOnce = false;
            }
        }
        else
        {
            buttonTop.transform.localScale = new Vector3(currentScale.x + 0.01f, currentScale.y + 0.01f, currentScale.z);
            if (buttonTop.transform.localScale.sqrMagnitude >= initialScale.sqrMagnitude)
            {
                buttonTop.transform.localScale = initialScale;
            }
        }
    }

    private void FixedUpdate()
    {
        if (buttonType != ButtonType.GROUND) return;
        if (pressedOnce)
        {
            rb.MovePosition(Vector2.Lerp(rb.position, pressedPosition, pressSpeed * Time.fixedDeltaTime));
            if (rb.position == pressedPosition)
            {
                pressedOnce = false;
            }
        }
        else
        {
            rb.MovePosition(Vector2.Lerp(rb.position, restPosition, pressSpeed * Time.fixedDeltaTime));
        }
    }

    private void OnButtonPressed(GameObject button)
    {
        if (button != this.gameObject) return;
        pressedOnce = true;
    }
}
