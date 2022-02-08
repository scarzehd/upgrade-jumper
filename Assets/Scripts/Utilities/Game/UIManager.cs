using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Update()
    {
        jumpSlider.maxValue = player.GetExtraJumps();
        jumpSlider.value = player.GetExtraJumpsValue();
        jumpText.text = "Jumps\n" + player.GetExtraJumpsValue();

        healthText.text = "" + player.GetHealth() + " / " + player.GetMaxHealth();
        healthSlider.maxValue = player.GetMaxHealth();
        healthSlider.value = player.GetHealth();

        staminaSlider.maxValue = player.GetMaxStamina();
        staminaSlider.value = player.GetStaminaValue();

        staminaSlider.targetGraphic.color = staminaGradient.Evaluate(staminaSlider.normalizedValue);

        goldText.text = player.GetGold() + "";
    }
}
