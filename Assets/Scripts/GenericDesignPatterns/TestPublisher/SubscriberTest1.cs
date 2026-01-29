using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriberTest1 : MonoBehaviour, ISubscriber
{
    //Mi iscrivo al pubblischer
    private void Awake()
    {
        Publisher.Subscribe(this, typeof(PauseMessage));
        Publisher.Subscribe(this, typeof(TestMessage));
        Publisher.Subscribe(this, typeof(TestMessageWValue));
    }

    //quello che faccio al ricevimento di un messaggio specifico
    public void OnPublish(IPublisherMessage message)
    {
        if (message is PauseMessage pauseMessage)
        {
            Debug.Log("prova del PauseMessage che ha il valore: " + pauseMessage.GamePaused);
        }
        else if (message is TestMessage)
        {
            Debug.Log("prova del TestMessage");
        }
        else if (message is TestMessageWValue testValueMessage)
        {
            Debug.Log("prova del TestMessageWValue e ha valore: Booleano = " + testValueMessage.provaBooleano1 + " ,Stringa = "+ testValueMessage.provaStringa1+" , Integer = "+ testValueMessage.provaInt1);
        }
    }
    //Mi disiscrivo al pubblischer
    public void OnDisableSubscriber()
    {
        Publisher.Unsubscribe(this, typeof(PauseMessage));
        Publisher.Unsubscribe(this, typeof(TestMessage));
        Publisher.Unsubscribe(this, typeof(TestMessageWValue));
    }
    private void OnDestroy()
    {
        OnDisableSubscriber();
    }
}
