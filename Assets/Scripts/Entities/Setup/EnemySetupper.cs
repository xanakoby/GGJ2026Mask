using UnityEngine;

public class EnemySetupper : MonoBehaviour
{
    public ItemBase itemBase;
    public delegate void OnCollision();
    public OnCollision onCollisionEnter;

    [Header("Setups")]
    public Rigidbody rb;
    public Damageable damageable;
    private void Start()
    {
        damageable.onDeath.RemoveAllListeners();

        damageable.onDeath.AddListener(() =>
        {
            itemBase.onDestroyTrigger?.Invoke();
            gameObject.SetActive(false);
        });
    }
    private void OnEnable()
    {
        //quando un nemico viene spawnato ha velocità 0, vita uguale al max, e lo stato di morte del damageable con il itemBase.onDestroyTrigger?.Invoke();gameObject.SetActive(false);
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;

        damageable.SetFullHealth();
    }
}
