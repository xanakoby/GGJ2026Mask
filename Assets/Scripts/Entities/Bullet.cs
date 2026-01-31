using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ItemSetupper itemSetupper;
    [SerializeField] float speed = 3f;
    [SerializeField] Rigidbody rb;

    public void ShootInDirection(Vector2 shootDir)
    {
        rb.linearVelocity = shootDir.normalized;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void OnCollisionEnter(Collision col)
    {
        itemSetupper.onDestroyTriggered?.Invoke();
    }
}
