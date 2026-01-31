using System;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public Action OnColEnter;
    private void OnCollisionEnter(Collision col)
    {
        OnColEnter?.Invoke();
    }
}
