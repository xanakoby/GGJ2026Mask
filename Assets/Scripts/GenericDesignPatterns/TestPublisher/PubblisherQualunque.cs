using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubblisherQualunque : MonoBehaviour
{
    public void PubblishMessage()
    {
        Publisher.Publish(new TestMessage());
    }
}
