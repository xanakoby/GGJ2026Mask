using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubblisherConRef : MonoBehaviour
{
    public bool booleano;
    public string stringa;
    public int integer;
    public void PubblishMessageWValue()
    {
        Publisher.Publish(new TestMessageWValue(booleano, stringa, integer));
    }
}
