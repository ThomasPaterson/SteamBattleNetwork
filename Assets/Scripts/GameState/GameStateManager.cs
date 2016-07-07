using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    public bool paused;
    public float gameTime { get; private set; }

    void Awake()
    {
        instance = this;
        paused = false;
        gameTime = 1f;
    }

    public void TogglePause()
    {
        PauseGame(!paused);
    }

    public void PauseGame(bool pause)
    {
        paused = pause;
        gameTime = paused ? 0f : 1f;
    }
}
