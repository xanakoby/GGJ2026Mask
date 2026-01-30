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


    [SerializeField] private bool isPlayerInSight;
    [SerializeField] private bool checkCollision;
    private void Start()
    {
        sight.enteredSight += PlayerEnterRange;
        sight.exitedSight += PlayerExitRange;

        playerTransform = GameManager.Instance.player.transform;

        StartCoroutine(Jump());
    }
    private void PlayerEnterRange()
    {
        Debug.Log("ho visto il nemico");
    }
    private void PlayerExitRange()
    {
        Debug.Log("non vedo più il nemico");
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(jumpCooldown);
        //salto di continuo

        rb.isKinematic = false;

        if (!isPlayerInSight)
        {
            if (isMovingRight)
            {
                rb.AddForce(new Vector3(jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(new Vector3(-jumpForceHorizontal, jumpForceVertical, 0), ForceMode.Impulse);
            }
        }
        else
        {
            //salto verso la direzione del player
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
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == groundMask)
        {
            rb.isKinematic = true;
        }
    }
}
