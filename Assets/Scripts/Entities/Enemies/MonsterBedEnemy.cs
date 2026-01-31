using System.Collections;
using UnityEngine;

public class MonsterBedEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float runSpeed = 5f;
    private float currentSpeed;
    [SerializeField] private float jumpForceVertical = 2f;
    [SerializeField] private float jumpForceHorizontal = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] private GenericSight sight2;
    [SerializeField] private Transform playerTransform;

    [SerializeField] bool isMovingRight;

    [SerializeField] private bool isEnemyInSight;
    [SerializeField] private bool isEnemyClose;

    [SerializeField] private float attackCooldown;
    bool keepAttacking;

    private void Start()
    {
        currentSpeed = speed;

        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;

        sight2.enteredSight += PlayerCloseRange;
        sight2.exitedSight += PlayerNoMoreCloseRange;
    }
    private void FixedUpdate()
    {
        if (!isEnemyClose && !keepAttacking)
            Move();
    }
    private void Move()
    {
        if (!isEnemyInSight)
        {
            if (isMovingRight)
            {
                //mi muovo verso destra
                rb.linearVelocity = new Vector3(currentSpeed, rb.linearVelocity.y, 0);
                if (rb.position.x >= pointB.x)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                //mi muovo verso sinistra
                rb.linearVelocity = new Vector3(-currentSpeed, rb.linearVelocity.y, 0);
                if (rb.position.x <= pointA.x)
                {
                    isMovingRight = true;
                }
            }
        }
        else if(isEnemyInSight)
        {
            //insegue il player
            playerTransform = GameManager.Instance.player.transform;

                if (rb.position.x < playerTransform.position.x)
                {
                    //muovi a destra
                    rb.linearVelocity = new Vector3(currentSpeed, rb.linearVelocity.y, 0);
                }
                else
                {
                    //muovi a sinistra
                    rb.linearVelocity = new Vector3(-currentSpeed, rb.linearVelocity.y, 0);
                }
            
        }
    }

    private void PlayerEnterRange()
    {
        isEnemyInSight = true;
        currentSpeed = runSpeed;

        Debug.Log("ho visto il nemico");
    }
    private void PlayerExitRange()
    {
        isEnemyInSight = false;
        currentSpeed = speed;

        Debug.Log("non vedo più il nemico");
    }
    private void PlayerCloseRange()
    {
        isEnemyClose = true;
        //fa il salto verso il player
        StartCoroutine(JumpAttack());

        Debug.Log("nemico vicino, salto!");
    }
    private void PlayerNoMoreCloseRange()
    {
        isEnemyClose = false;

        Debug.Log("nemico non più vicino");
    }
    IEnumerator JumpAttack()
    {
        keepAttacking = isEnemyClose;
        while (keepAttacking)
        {
            rb.linearVelocity = Vector3.zero;
            Vector3 direction = (playerTransform.position - rb.position).normalized;
            rb.AddForce(new Vector3(direction.x * jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);

            Debug.Log("saltooooo attaccoooo");

            yield return new WaitForSeconds(attackCooldown);
            keepAttacking = isEnemyClose;
        }

        Debug.Log("basta saltare attaccare");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointA, 0.2f);
        Gizmos.DrawWireSphere(pointB, 0.2f);
    }
}
