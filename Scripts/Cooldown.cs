using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Image skillIcon;
    public static bool coolingDown = false;
    void Awake()
    {
        skillIcon = GetComponent<Image>();
    }

    void Update() {
        if (coolingDown)
            SkillCooldown();
    }

    public void SkillCooldown(){
        skillIcon.fillAmount = 0;
        //skillIcon.fillAmount += 1 / cd * Time.deltaTime;
        if (skillIcon.fillAmount >= 1)
            coolingDown = false;
            
    }
}
