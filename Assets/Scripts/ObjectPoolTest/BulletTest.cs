using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    public ItemBase itemBase;
    public delegate void OnCollision();
    public OnCollision onCollisionEnter;
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //qui l'oggetto agisce come se venisse eliminato
        itemBase.onDestroyTrigger?.Invoke();

        //onCollisionEnter?.Invoke();
        gameObject.SetActive(false);
    }
}
