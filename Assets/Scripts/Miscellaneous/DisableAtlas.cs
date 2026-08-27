using UnityEngine;

public class DisableAtlas : MonoBehaviour
{
    [SerializeField] TimeSwitch timeswitch; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timeswitch = collision.GetComponent<TimeSwitch>();
            timeswitch.isTimeSwitchEnabled = false;
            Destroy(this.gameObject);
        }
    }
}
