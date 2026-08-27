using UnityEngine;

public class LazerAttack : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        if (target.position.x < transform.position.x)
        {
            transform.rotation *= Quaternion.RotateTowards(target.rotation, Quaternion.FromToRotation(new Vector3(0, -1, 0), new Vector3(-1, 0, 0)), 17 * Time.deltaTime);
        }
        else
        {
            transform.rotation *= Quaternion.RotateTowards(target.rotation, Quaternion.FromToRotation(new Vector3(0, -1, 0), new Vector3(1, 0, 0)), 17 * Time.deltaTime);
        }

        RaycastHit2D hitres;
        if (hitres = Physics2D.Raycast(transform.position, transform.up, 30, LayerMask.GetMask("Player")))
        {
            hitres.transform.GetComponent<Lives>().Damage();
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawLine(transform.position, transform.position + (transform.up * 30));
    }
}
