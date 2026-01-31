using System.Collections;
using UnityEngine;

public class FeeverEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] bool isMovingRight;

    [SerializeField] private bool isEnemyInSight;
    bool keepAttacking;
    [SerializeField] private float chargeShotTime;
    [SerializeField] private float waitAfterShotTime;

    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;
    }
    private void FixedUpdate()
    {
        if (!keepAttacking)
            MoveThroughPatterns();
    }
    private void MoveThroughPatterns()
    {
        if (isMovingRight)
        {
            //mi muovo verso destra
            rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0);
            if (rb.position.x >= pointB.x)
            {
                isMovingRight = false;
            }
        }
        else
        {
            //mi muovo verso sinistra
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, 0);
            if (rb.position.x <= pointA.x)
            {
                isMovingRight = true;
            }
        }
    }
    private void PlayerEnterRange()
    {
        isEnemyInSight = true;
        if (keepAttacking)
            return;
        StartCoroutine(ClownStartAttacking());
    }
    private void PlayerExitRange()
    {
        isEnemyInSight = false;
    }
    IEnumerator ClownStartAttacking()
    {
        Debug.Log("Enemy started attacking!");
        //il nemico si ferma e carica l'attacco verso il giocatore
        rb.linearVelocity = Vector3.zero;

        keepAttacking = isEnemyInSight;
        while (keepAttacking)
        {
            //fa l'animazione di attacco
            yield return new WaitForSeconds(chargeShotTime);
            Debug.Log("Sparo il proiettile verso il player");
            yield return new WaitForSeconds(waitAfterShotTime);

            yield return null;

            keepAttacking = isEnemyInSight;
        }
        Debug.Log("Enemy started Walking!");

        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA, 0.2f);
        Gizmos.DrawWireSphere(pointB, 0.2f);
    }
}
