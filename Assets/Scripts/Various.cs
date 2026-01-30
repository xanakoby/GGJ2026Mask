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
    public void PlaySound(string soundName)
    {
        AudioManager.instance.PlaySFX(soundName);
    }
}
