using System;
using UnityEngine;

public class GenericSight : MonoBehaviour
{
    public LayerMask layerMask;
    public Action enteredSight;
    public Action exitedSight;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == layerMask)
        {
            enteredSight?.Invoke();
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == layerMask)
        {
            exitedSight?.Invoke();
        }
    }
}
