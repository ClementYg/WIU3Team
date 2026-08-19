using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class Interactable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GameObject player;
    [SerializeField] private GameObject frame;
    [SerializeField] private GameObject leftDivider;
    [SerializeField] private GameObject rightDivider;
    [SerializeField] private TextMeshProUGUI text;
    private CanvasGroup frameCanvas;
    private RectTransform leftTransform, rightTransform;
    private Vector3 leftInitialPosition, rightInitialPosition;

    [Header("Interaction Distance")]
    [SerializeField] private float distance;
    [SerializeField] private bool useDistanceSquared = false;
    protected bool playerInRange = false;

    [Header("Frame Config")]
    [SerializeField] protected bool useDefaultValues = true;
    [SerializeField][Range(1f, 100f)] protected float fadeSpeed;
    [SerializeField][Range(1f, 100f)] protected float moveSpeed;
    [SerializeField] protected float initialDistanceFromCenter;
    [SerializeField][Range(0f, 2f)] protected float maxDividerDistance = 0.5f;
    [SerializeField] protected string textContent;
    [SerializeField] protected float fontSize;

    // Interactions need to be implemented according to type of interaction
    public abstract void Interact();

    protected virtual void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        frameCanvas = frame.GetComponent<CanvasGroup>();
        frameCanvas.alpha = 0f;

        leftTransform = leftDivider.GetComponent<RectTransform>();
        rightTransform = rightDivider.GetComponent<RectTransform>();

        leftInitialPosition = new Vector3(leftTransform.position.x - initialDistanceFromCenter, leftTransform.position.y, leftTransform.position.z);
        rightInitialPosition = new Vector3(rightTransform.position.x + initialDistanceFromCenter, rightTransform.position.y, rightTransform.position.z);

        leftTransform.position = leftInitialPosition;
        rightTransform.position = rightInitialPosition;

        text.text = textContent;
        text.fontSize = fontSize;
    }

    protected virtual void Update()
    {
        playerInRange = false;
        
        Vector2 toPlayer = player.transform.position - transform.position;
        if (toPlayer.magnitude <= (useDistanceSquared ? distance * distance : distance))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, toPlayer.normalized, distance);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    playerInRange = true;
                }
            }
        }

        frameCanvas.alpha = Mathf.MoveTowards(frameCanvas.alpha, playerInRange ? 1f : 0f, Time.deltaTime * fadeSpeed);

        float leftNewX = leftInitialPosition.x + (playerInRange ? -maxDividerDistance : 0f);
        Vector3 newLeftPosition = new Vector3(leftNewX, leftTransform.position.y, leftTransform.position.z);
        leftTransform.position = Vector3.MoveTowards(leftTransform.position, newLeftPosition, Time.deltaTime * moveSpeed);

        float rightNewX = rightInitialPosition.x + (playerInRange ? maxDividerDistance : 0f);
        Vector3 newRightPosition = new Vector3(rightNewX, rightTransform.position.y, rightTransform.position.z);
        rightTransform.position = Vector3.MoveTowards(rightTransform.position, newRightPosition, Time.deltaTime * moveSpeed);

        if (frameCanvas.alpha == 1f)
        {
            SetInteractable(true);
        }
        else
        {
            SetInteractable(false);
        }

        if (InputSystem.actions["Interact"].WasPressedThisFrame() && playerInRange)
        {
            Interact();
        }
    }

    private void SetInteractable(bool value)
    {
        frameCanvas.interactable = value;
        frameCanvas.blocksRaycasts = value;
    }
}
