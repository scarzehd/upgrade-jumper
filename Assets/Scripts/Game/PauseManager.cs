using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public UIManager uiManager;

    private static PauseManager _instance;

    public static bool paused;

    private void Start()
    {
        _instance = this;
        paused = false;
    }

    public static void Pause()
    {
        _instance.uiManager.Pause();
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }
}
