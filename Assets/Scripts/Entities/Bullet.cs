using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ItemSetupper itemSetupper;
    [SerializeField] float speed = 3f;
    [SerializeField] float lifeDuration = 3f;
    [SerializeField] Rigidbody rb;

    public void ShootInDirection(Vector2 shootDir)
    {
        rb.linearVelocity = shootDir.normalized;

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        StartCoroutine(StartLifeTime());
    }
    IEnumerator StartLifeTime()
    {
        yield return new WaitForSeconds(lifeDuration);
        itemSetupper.onDestroyTriggered?.Invoke();
    }
    private void OnCollisionEnter(Collision col)
    {
        StopCoroutine(StartLifeTime());

        itemSetupper.onDestroyTriggered?.Invoke();
    }
}
