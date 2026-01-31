using UnityEngine;
using UnityEngine.UI;

public class Factory : IFactory
{
    ObjectPooler<ItemBase> itemBasePooler;
    ObjectPooler<ItemBase> itemBasePooler2;
    ObjectPooler<ItemBase> itemBasePooler3;

    ObjectPooler<ItemBase> clownEnemyPooler;
    ObjectPooler<ItemBase> bedMonsterEnmeyPooler;
    ObjectPooler<ItemBase> spiderEnmeyPooler;
    ObjectPooler<ItemBase> feeverEnmeyPooler;

    ObjectPooler<ItemBase> sneezeBulletPooler;

    public IItem Create(ItemData itemData)
    {
        try
        {
            if (itemBasePooler == null)
            {
                itemBasePooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = itemBasePooler.Get();



            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    itemBasePooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }
    public IItem Create(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (itemBasePooler == null)
            {
                itemBasePooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = itemBasePooler.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    itemBasePooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem Create2(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (itemBasePooler2 == null)
            {
                itemBasePooler2 = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = itemBasePooler2.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    itemBasePooler2.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem Create3(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (itemBasePooler3 == null)
            {
                itemBasePooler3 = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = itemBasePooler3.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    itemBasePooler3.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem CreateBedMonsterEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (bedMonsterEnmeyPooler == null)
            {
                bedMonsterEnmeyPooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = bedMonsterEnmeyPooler.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    bedMonsterEnmeyPooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem CreateClownEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (clownEnemyPooler == null)
            {
                clownEnemyPooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = clownEnemyPooler.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    clownEnemyPooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem CreateFeeverEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (feeverEnmeyPooler == null)
            {
                feeverEnmeyPooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = feeverEnmeyPooler.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    feeverEnmeyPooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem CreateSneezeBullet(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (sneezeBulletPooler == null)
            {
                sneezeBulletPooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = sneezeBulletPooler.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    sneezeBulletPooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }

    public IItem CreateSpiderEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation)
    {
        try
        {
            if (spiderEnmeyPooler == null)
            {
                spiderEnmeyPooler = new ObjectPooler<ItemBase>(itemData.ItemPrefab);
            }
            ItemBase spawnedItem = spiderEnmeyPooler.Get();

            //qui è dove setto l'oggetto dall'object pool
            spawnedItem.transform.SetPositionAndRotation(spawnPosition, rotation);

            if (!spawnedItem.gameObject.activeSelf)
            {
                spawnedItem.gameObject.SetActive(true);
            }
            else
            {
                spawnedItem.onDestroyTrigger += () =>
                {
                    spiderEnmeyPooler.Set(spawnedItem);
                };
            }

            if (spawnedItem == null || spawnedItem is not IItem) //  typeof(newFoodBase.GetType()) != IFood
            {
                Debug.LogError($"Errore nella Factory. Nessun prefab creato per il FoodBase {itemData.Name}");
                return default;
            }

            spawnedItem.Initialize(itemData);

            return spawnedItem;
        }
        catch
        {
            return default;
        }
    }
}
