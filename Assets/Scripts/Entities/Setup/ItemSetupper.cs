using UnityEngine;

public class ItemSetupper : MonoBehaviour
{
    public ItemBase itemBase;
    public delegate void OnDestroy();
    public OnDestroy onDestroyTriggered;

    [Header("Setups")]
    public Rigidbody rb;
    private void Start()
    {
        onDestroyTriggered += () =>
        {
            itemBase.onDestroyTrigger?.Invoke();
            gameObject.SetActive(false);
        };
    }
    private void OnEnable()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }

}
