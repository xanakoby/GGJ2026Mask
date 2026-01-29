using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Caller : MonoBehaviour
{
    GameManagerSingleton gm;

    public bool GameManagerAtStart;

    void Start()
    {
        if (GameManagerAtStart)
            gm = GameManagerSingleton.Instance;
        //posso eseguire le funzioni sia con gm che direttamente istanziandolo
    }

    public void ShowGMVar()
    {
        Debug.Log(GameManagerSingleton.Instance.textExample);
    }

    public void ExecuteGMFunction()
    {
        GameManagerSingleton.Instance.FunctionExample();
    }

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }
}
