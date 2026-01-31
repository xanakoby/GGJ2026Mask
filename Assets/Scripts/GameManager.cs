using DesignPatterns.Generics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public PlayerInput playerInput;

    [Space]
    [SerializeField] ItemData _clownEnemy;
    [SerializeField] ItemData _bedMonsterEnemy;
    [SerializeField] ItemData _spiderEnemy;
    [SerializeField] ItemData _feeverEnemy;

    [SerializeField] ItemData _sneezeBullet;

    public IFactory enemyFactory { get; private set; }

    public override void Awake()
    {
        base.Awake();

        enemyFactory = new Factory();
    }
    public void FreezePlayer(bool _toFreeze)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (_toFreeze)
        {
            playerInput.enabled = false;

            rb.isKinematic = true;
        }
        else
        {
            playerInput.enabled = true;

            rb.isKinematic = false;
        }
    }
    public void ChangeScene(string _sceneName)
    {
        LevelManager.Instance.ChangeScene(_sceneName);
    }

    #region SPAWN ENEMY 
    public GameObject CreateClownEnemy(Transform transform)
    {
        ItemBase itemBase = (ItemBase)enemyFactory.CreateClownEnemy(_clownEnemy, transform.position, Quaternion.identity);
        return itemBase.gameObject;
    }
    public GameObject CreateBedMonsterEnemy(Transform transform)
    {
        ItemBase itemBase = (ItemBase)enemyFactory.CreateBedMonsterEnemy(_bedMonsterEnemy, transform.position, Quaternion.identity);
        return itemBase.gameObject;
    }
    public GameObject CreateFeeverEnemy(Transform transform)
    {
        ItemBase itemBase = (ItemBase)enemyFactory.CreateFeeverEnemy(_feeverEnemy, transform.position, Quaternion.identity);
        return itemBase.gameObject;
    }
    public GameObject CreateSpiderEnemy(Transform transform)
    {
        ItemBase itemBase = (ItemBase)enemyFactory.CreateSpiderEnemy(_spiderEnemy, transform.position, Quaternion.identity);
        return itemBase.gameObject;
    }
    #endregion
    #region SPAWN BULLETS
    public GameObject CreateSneezeBullet(Transform transform)
    {
        ItemBase itemBase = (ItemBase)enemyFactory.CreateSneezeBullet(_sneezeBullet, transform.position, Quaternion.identity);
        return itemBase.gameObject;
    }
    #endregion
}
