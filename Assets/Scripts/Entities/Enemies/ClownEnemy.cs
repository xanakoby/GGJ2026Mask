using System;
using System.Collections;
using UnityEngine;

public class ClownEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [Tooltip("distanza a sinistra rispetto al clown")]
    [SerializeField] private float leftDistance = -2;
    [Tooltip("distanza a sinistra rispetto al clown")]
    [SerializeField] private float rightDistance = 2;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] bool isMovingRight;

    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;

    [SerializeField] private bool isEnemyInSight;
    public bool IsFacingRight;
    bool keepAttacking;
    bool elapsedFirstFrame;

    Coroutine attackCoroutine;

    [Header("Clown Vars")]
    [SerializeField] float attackAnimation = 3f;
    public Transform graphicsTransform;
    public float prospettiveRotationDif = 50f;

    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;
    }
    //da fare il punto point a b da dove spawno per tutti gli altri
    //private void OnEnable()
    //{
    //    pointA = new Vector2(transform.position.x + leftDistance, 0);
    //    pointB = new Vector2(transform.position.x + rightDistance, 0);
    //}
    private void OnDisable()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(ClownStartAttacking());
            attackCoroutine = null;
        }
        elapsedFirstFrame = false;
    }

    private void FixedUpdate()
    {
        if (!elapsedFirstFrame)
        {
            pointA = new Vector2(transform.position.x + leftDistance, 0);
            pointB = new Vector2(transform.position.x + rightDistance, 0);
            elapsedFirstFrame = true;
        }
        if(!keepAttacking)
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
                Turn();
                isMovingRight = false;
            }
        }
        else
        {
            //mi muovo verso sinistra
            rb.linearVelocity = new Vector3(-speed, rb.linearVelocity.y, 0);
            if (rb.position.x <= pointA.x)
            {
                Turn();
                isMovingRight = true;
            }
        }
    }

    private void PlayerEnterRange()
    {
        isEnemyInSight = true;
        if (keepAttacking)
            return;
        attackCoroutine = StartCoroutine(ClownStartAttacking());
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
            yield return new WaitForSeconds(attackAnimation);
            yield return null;
            Debug.Log("Clown Attaccoooooo");
            keepAttacking = isEnemyInSight;
        }
        Debug.Log("Enemy started Walking!");

        yield return null;
    }
    public void Turn()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y += 180;
        transform.eulerAngles = rotation;

        Vector3 rotationGraphic = graphicsTransform.eulerAngles;
        if (IsFacingRight)
        {
            rotationGraphic.y -= prospettiveRotationDif * 2;
            graphicsTransform.eulerAngles = rotationGraphic;
        }
        else
        {
            rotationGraphic.y += prospettiveRotationDif * 2;
            graphicsTransform.eulerAngles = rotationGraphic;
        }

        IsFacingRight = !IsFacingRight;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA, 0.2f);
        Gizmos.DrawWireSphere(pointB, 0.2f);
    }
}
