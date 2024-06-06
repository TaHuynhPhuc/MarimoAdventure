using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    int maxPlatform = 0;
    public void GameOver()
    {
        gameOverScreen.Setup(maxPlatform);
    }
}
