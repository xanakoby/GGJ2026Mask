using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    //per il test dei messaggi pubblisher
    public bool isGamePaused;
    public void PubblishPauseGameMessage()
    {
        Publisher.Publish(new PauseMessage(isGamePaused));
    }
    //da qui in poi factory

    ObjectPooler<BulletTest> itemPooler;

    public Transform spawnTransform;
    public IFactory itemFactory { get; private set; }
    //public IFactory itemFactory2 { get; private set; }
    //public IFactory itemFactory3 { get; private set; }

    //private List<FactorySpawnable> objectPool = new List<FactorySpawnable>();

    private void Awake()
    {
        itemFactory = new Factory();
    }

    public void CreateItem1(ItemData _itemData)
    {
        itemFactory.Create(_itemData, spawnTransform.position, Quaternion.identity);
    }
    public void CreateItem2(ItemData _itemData)
    {
        itemFactory.Create2(_itemData, spawnTransform.position, Quaternion.identity);
    }
    public void CreateItem3(ItemData _itemData)
    {
        itemFactory.Create3(_itemData, spawnTransform.position, Quaternion.identity);
    }

    //private void InitializeItem(ItemBase _itemBase)
    //{
    //    _itemBase.transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.transform.rotation);
    //}
    public void Shoot()
    {
        //qui � dove prendo
        BulletTest spawnedBullet = itemPooler.Get();

        if (!spawnedBullet.gameObject.activeSelf)
        {
            spawnedBullet.gameObject.SetActive(true);
        }
        else
        {
            spawnedBullet.onCollisionEnter += () =>
            {
                itemPooler.Set(spawnedBullet);
            };
        }

        //qui � dove setto l'oggetto dall'object pool a 0 con le cose di base
        spawnedBullet.transform.SetPositionAndRotation(spawnTransform.position, Quaternion.identity);
        spawnedBullet.rb.linearVelocity = Vector3.zero;
        //spawnedBullet.rb.AddForce(shootPivot.forward * shootForce, ForceMode.Impulse);
    }
}
