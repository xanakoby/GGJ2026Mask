using System.Collections;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    [SerializeField] LayerMask groundMask;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForceVertical = 2f;
    [SerializeField] private float jumpForceHorizontal = 2f;
    [SerializeField] private GenericSight sight;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private bool isMovingRight;
    [SerializeField] private float jumpCooldown = 1f;
    [SerializeField] private bool endlessJump;


    [SerializeField] private bool isPlayerInSight;
    public bool IsFacingRight;
    [SerializeField] private bool checkCollision;
    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;

        playerTransform = GameManager.Instance.player.transform;
    }
    private void OnEnable()
    {
        StartCoroutine(Jump());
    }
    private void OnDisable()
    {
        StopCoroutine(Jump());
    }
    private void PlayerEnterRange()
    {
        Debug.Log("ho visto il nemico");
        isPlayerInSight = true;
    }
    private void PlayerExitRange()
    {
        Debug.Log("non vedo più il nemico");
        isPlayerInSight = false;
    }

    IEnumerator Jump()
    {
        //salto di continuo

        while (endlessJump)
        {
            rb.isKinematic = false;

            if (!isPlayerInSight)
            {
                CheckDirectionToFace(isMovingRight);
                if (isMovingRight)
                {
                    rb.AddForce(new Vector3(jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);
                }
                else
                {
                    rb.AddForce(new Vector3(-jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);
                }
                isMovingRight = !isMovingRight;
            }
            else
            {
                //salto verso la direzione del player
                CheckDirectionToFace(rb.position.x < playerTransform.position.x);
                if (rb.position.x < playerTransform.position.x)
                {
                    //muovi a destra
                    rb.AddForce(new Vector3(jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);
                }
                else
                {
                    //muovi a sinistra
                    rb.AddForce(new Vector3(-jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);
                }
            }

            yield return new WaitForSeconds(jumpCooldown);
        }
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

        IsFacingRight = !IsFacingRight;
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == groundMask)
        {
            rb.isKinematic = true;
        }
    }
}
