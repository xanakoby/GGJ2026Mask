using System;
using UnityEngine;

public class GenericSight : MonoBehaviour
{
    public LayerMask layerMask;
    public Action enteredSight;
    public Action exitedSight;

    private void OnTriggerEnter(Collider col)
    {
        if (((1 << col.gameObject.layer) & layerMask) != 0)
        {
            enteredSight?.Invoke();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (((1 << col.gameObject.layer) & layerMask) != 0)
        {
            exitedSight?.Invoke();
        }
    }
}
