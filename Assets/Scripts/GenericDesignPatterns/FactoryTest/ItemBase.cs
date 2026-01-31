using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ItemBase : MonoBehaviour, IItem //FactorySpawnable
{
    public delegate void OnDestroy();
    public OnDestroy onDestroyTrigger;
    protected ItemData itemData;
    public virtual void Initialize(ItemData _itemData)
    {
        this.itemData = _itemData;
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
