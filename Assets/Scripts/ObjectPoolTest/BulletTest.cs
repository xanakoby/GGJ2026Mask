using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    public delegate void OnCollision();
    public OnCollision onCollisionEnter;
    public Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        onCollisionEnter?.Invoke();
        gameObject.SetActive(false);
    }
}
