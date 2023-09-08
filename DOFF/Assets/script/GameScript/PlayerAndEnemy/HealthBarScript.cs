using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health) //To calculate the max health and the current of the player
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health) //To indicate the current health of the player
    {
        slider.value = health;
    }
}
