using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    //private Job currentJob;
    //private int intellect = 5;
    private int maxHealth = 500;
    private int currHealth = 500;
    public static int currMana = 10000;
    public static Enemy target = null;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public Slider healthSlider;
    public Slider manaSlider;
    void Awake()
    {
        manaSlider.maxValue = 10000;
        healthSlider.maxValue = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currHealth;
        manaSlider.value = currMana;
        healthText.text = "HP: " + currHealth;
        manaText.text = "MP: " + currMana;
    }
}
