using UnityEngine;

public class Various : MonoBehaviour
{
    public void Message(string _msg)
    {
        Debug.Log(_msg);
    }
    public void ChangeSceneee(string _sceneName)
    {
        GameManager.Instance.ChangeScene(_sceneName);
    }
    public void PlayMusic(string musicName)
    {
        AudioManager.Instance.PlayMusic(musicName);
    }
    public void PlaySound(string soundName)
    {
        AudioManager.Instance.PlaySFX(soundName);
    }
    #region SPAWN ENEMY 
    public void CreateClownEnemy(Transform transform)
    {
        GameManager.Instance.CreateClownEnemy(transform);
    }
    public void CreateBedMonsterEnemy(Transform transform)
    {
        GameManager.Instance.CreateBedMonsterEnemy(transform);
    }
    public void CreateFeeverEnemy(Transform transform)
    {
        GameManager.Instance.CreateFeeverEnemy(transform);
    }
    public void CreateSpiderEnemy(Transform transform)
    {
        GameManager.Instance.CreateSpiderEnemy(transform);
    }
    #endregion
}
