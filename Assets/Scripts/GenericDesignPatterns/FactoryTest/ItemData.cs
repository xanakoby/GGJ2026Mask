using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ScriptableObjects/ItemData")]
public class ItemData : FactoryScriptableObject, ISpawnable
{
    public string Name;
    public ItemBase ItemPrefab;
    public GameObject GetGameObject()
    {
        return null;
    }
}
