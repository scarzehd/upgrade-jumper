using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(delegate { Play(); });
        quitButton.onClick.AddListener(delegate { Quit(); });
    }

    public void Play()
    {
        SceneManager.LoadScene("TestWorld");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
