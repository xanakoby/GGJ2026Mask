using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LevelSceneStarter : MonoBehaviour
{
    public UnityEvent startSceneActions;
    [SerializeField] bool triggerAfterDelay;
    [SerializeField] float delayTime;
    public UnityEvent delaySceneActions;

    public Vector2 startScenePlayerPos;

    private void Start()
    {
        startSceneActions?.Invoke();
        if (triggerAfterDelay)
        {
            StartCoroutine(StartAfter());
        }
    }
    IEnumerator StartAfter()
    {
        yield return new WaitForSeconds(delayTime);
        delaySceneActions?.Invoke();
    }
}
