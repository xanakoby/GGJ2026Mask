using DesignPatterns.Generics;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player player;
    public PlayerInput playerInput;

    public override void Awake()
    {
        base.Awake();
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
}
