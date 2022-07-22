using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerController player;

    public Text jumpText;
    public Slider jumpSlider;

    public Text healthText;
    public Slider healthSlider;

    public Text goldText;

    public Gradient staminaGradient;

    public Slider staminaSlider;

    public GameObject pauseMenu;

    public GameObject pauseMenuFirstButton;

    public GameObject HUD;

    private void Update()
    {
        jumpSlider.maxValue = player.extraJumps;
        jumpSlider.value = player.extraJumpsValue;
        jumpText.text = "Jumps\n" + player.extraJumpsValue;

        healthText.text = "" + player.health.health + " / " + player.health.maxHealth;
        healthSlider.maxValue = player.health.maxHealth;
        healthSlider.value = player.health.health;

        staminaSlider.maxValue = player.maxStamina;
        staminaSlider.value = player.staminaValue;

        staminaSlider.targetGraphic.color = staminaGradient.Evaluate(staminaSlider.normalizedValue);

        goldText.text = player.gold + "";
    }

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        HUD.SetActive(!HUD.activeSelf);

        if (pauseMenu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(pauseMenuFirstButton);
        }
    }

    public void MainMenu()
    {
        PauseManager.Pause();
        SceneManager.LoadScene("MainMenu");
    }
}
