using UnityEngine;

// Attach to a 2D trigger collider on the exit hole.
public class MazeExitTrigger : MonoBehaviour
{
    [SerializeField] RollingMazePuzzle puzzle;
    [SerializeField] string ballTag = "Ball";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(ballTag))
        {
            Debug.Log("detected ball\n");
            puzzle.OnBallReachedExit();
        }
    }
}