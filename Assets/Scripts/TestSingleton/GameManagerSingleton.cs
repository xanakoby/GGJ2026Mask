using DesignPatterns.Generics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : Singleton<GameManagerSingleton>
{
    public string textExample= "Showowo";

    //fa l'awake e in più fa anche l'awake del singleton attaccat
    public override void Awake()
    {
        base.Awake();

        //eseguo il resto che voglio
    }

    public void FunctionExample()
    {
        Debug.Log("la funzione d'esempio funziona");
    }
}
