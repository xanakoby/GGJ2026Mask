using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public interface IFactory
{
    IItem Create(ItemData itemData);
    IItem Create(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    IItem Create2(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    IItem Create3(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    //Enemies
    IItem CreateClownEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    IItem CreateBedMonsterEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    IItem CreateSpiderEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    IItem CreateFeeverEnemy(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);
    //Projectiles
    IItem CreateSneezeBullet(ItemData itemData, Vector3 spawnPosition, Quaternion rotation);

}
