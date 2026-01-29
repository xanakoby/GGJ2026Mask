using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessageWValue : IPublisherMessage
{
    public bool provaBooleano1 { get; }
    public string provaStringa1 { get; }
    public int provaInt1 { get; }

    //il valore viene messo qui da chi lo chiama, e viene preso da qui da chi subisce la chiamata
    public TestMessageWValue(bool _bool, string _string, int _int)
    {
        provaBooleano1 = _bool;
        provaStringa1 = _string;
        provaInt1 = _int;
    }
}
