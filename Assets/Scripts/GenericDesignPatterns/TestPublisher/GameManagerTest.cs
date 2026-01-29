using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    public bool isGamePaused;
    public void PubblishPauseGameMessage()
    {
        Publisher.Publish(new PauseMessage(isGamePaused));
    }
}
