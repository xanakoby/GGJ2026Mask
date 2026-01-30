using System;
using System.Collections;
using UnityEngine;

public class ClownEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] bool isMovingRight;

    [SerializeField] private bool isEnemyInSight;

    [Header("Clown Vars")]
    [SerializeField] float attackAnimation = 3f;

    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;
    }

    private void FixedUpdate()
    {
        if(!isEnemyInSight)
        MoveThroughPatterns();
    }
    private void MoveThroughPatterns()
    {
        if (isMovingRight)
        {
            //mi muovo verso destra
            rb.linearVelocity = new Vector3(speed, rb.linearVelocity.y, 0);
            if(rb.position.x >= pointB.x)
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

        while (isEnemyInSight)
        {
            //fa l'animazione di attacco
            yield return new WaitForSeconds(attackAnimation);
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
