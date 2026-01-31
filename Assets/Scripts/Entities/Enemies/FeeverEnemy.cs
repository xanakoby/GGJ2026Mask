using System.Collections;
using UnityEngine;

public class FeeverEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [Tooltip("distanza a sinistra rispetto al clown")]
    [SerializeField] private float leftDistance = -2;
    [Tooltip("distanza a sinistra rispetto al clown")]
    [SerializeField] private float rightDistance = 2;
    [SerializeField] private float speed = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] bool isMovingRight;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Vector2 pointA;
    [SerializeField] private Vector2 pointB;

    [SerializeField] private bool isEnemyInSight;
    public bool IsFacingRight;
    bool keepAttacking;
    [SerializeField] private Transform spawnBulletPoint;
    [SerializeField] private float chargeShotTime;
    [SerializeField] private float waitAfterShotTime;

    public Transform graphicsTransform;
    public float prospettiveRotationDif = 50f;

    bool elapsedFirstFrame;

    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;
    }
    private void OnDisable()
    {
        StopCoroutine(StartShooting());
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
        StartCoroutine(StartShooting());
    }
    private void PlayerExitRange()
    {
        isEnemyInSight = false;
    }
    IEnumerator StartShooting()
    {
        playerTransform = GameManager.Instance.player.transform;

        Debug.Log("Enemy started attacking!");
        //il nemico si ferma e carica l'attacco verso il giocatore
        rb.linearVelocity = Vector3.zero;

        keepAttacking = isEnemyInSight;
        while (keepAttacking)
        {
            //carico l'attacco
            yield return new WaitForSeconds(chargeShotTime);
            Debug.Log("Sparo il proiettile verso il player");
            CheckDirectionToFace(rb.position.x < playerTransform.position.x);
            Vector3 dir = (playerTransform.position - transform.position).normalized;
            GameObject g = GameManager.Instance.CreateSneezeBullet(spawnBulletPoint);
            //Debug.Log("oggetoooo" + g);
            Bullet b = g.GetComponent<Bullet>();
            //Debug.Log("bullettooooo" + b);
            b.ShootInDirection(dir);
            //mi ruoto verso il player e gli sparo
            yield return new WaitForSeconds(waitAfterShotTime);

            yield return null;

            keepAttacking = isEnemyInSight;
        }
        Debug.Log("Enemy started Walking!");

        yield return null;
    }
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
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
