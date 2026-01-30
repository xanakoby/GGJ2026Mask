using System.Collections;
using UnityEngine;

public enum EnemyType
{
    Pagliaccio,
    MostroLetto,
    Ragno,
    Temporale,
    Fantasma,
    Febbre
}
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] private GenericSight extraSight1;
    [SerializeField] private GenericSight extraSight2;

    [Header("Enemies Vars")]
    [Header("Clown Vars")]
    [Tooltip("quando il clown manda indietro la testa")]
    [SerializeField] float chargeAttackTime = 3f;
    [Tooltip("quando il clown manda in avanti la testa")]
    [SerializeField] float attackInTime = 1f;
    [Tooltip("quando il clown manda in riposo la testa")]
    [SerializeField] float Chill = 2f;

    [Header("BedMonster Vars")]
    [SerializeField] float runSpeed = 5f;
    [Tooltip("la forza di salto per il piccolo saltino mentre attacca")]
    [SerializeField] float jumpAttackForce = 2f; 



    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;
    }

    private void FixedUpdate()
    {
        MoveThroughPatterns();
    }
    private void PlayerEnterRange()
    {
        switch(enemyType)
        {
            case EnemyType.Pagliaccio:
                StartCoroutine(ClownStartAttacking());
                break;
            case EnemyType.MostroLetto:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Ragno:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Temporale:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Fantasma:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Febbre:
                //StartCoroutine(StartAttacking());
                break;
            default:
                break;
        }
    }
    private void PlayerExitRange()
    {
        switch (enemyType)
        {
            case EnemyType.Pagliaccio:

                StopCoroutine(ClownStartAttacking());
                //e torna al patrolling
                break;
            case EnemyType.MostroLetto:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Ragno:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Temporale:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Fantasma:
                //StartCoroutine(StartAttacking());
                break;
            case EnemyType.Febbre:
                //StartCoroutine(StartAttacking());
                break;
            default:
                break;
        }
    }
    private void MoveThroughPatterns()
    {
        float time = Mathf.PingPong(Time.time * speed, 1f);
        Vector2 newPosition = Vector2.Lerp(pointA, pointB, time);
        rb.linearVelocity = new Vector3(newPosition.x - rb.position.x, rb.linearVelocity.y, 0);
    }

    IEnumerator ClownStartAttacking()
    {
               Debug.Log("Enemy started attacking!");
        yield return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA, 0.1f);
        Gizmos.DrawWireSphere(pointB, 0.1f);
    }
}
