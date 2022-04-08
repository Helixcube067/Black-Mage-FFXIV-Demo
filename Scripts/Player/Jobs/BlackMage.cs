using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum States { AstralFire1, AstralFire2, AstralFire3, UmbralIce1, UmbralIce2, UmbralIce3, Neutral }
public enum Element { Fire, Ice}
public class BlackMage : Job
{
    public TextMeshProUGUI fireCost;
    public TextMeshProUGUI blizzCost;
    public Sprite firePic;
    public Sprite blizzPic;
    public Image changer;
    public Slider castTimeSlider;
    public GameObject sliderObject;
    public TextMeshProUGUI castText;
    public TextMeshProUGUI battleText;
    public GameObject noTargetText;
    public GameObject damageHolder;
    public TextMeshProUGUI damageText;
    public GameObject fireStacks;
    public GameObject blizzStacks;
    public TextMeshProUGUI stackText;
    private States currState = States.Neutral;
    public static bool casting = false;
    float baseFireManaCost = 800;
    float baseblizzManaCost = 400;
    float currFireCost = 800;
    float currBlizzCost = 400;
    float baseBlizzPotency = 180;
    float baseFirePotency = 180;
    float currFirePotency = 180;
    float currBlizzPotency = 180;
    float manaRegen;
    public int baseRegen = 120;
    private float castTime = 21f;
    void Start()
    {
        CostUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        StacksUpdate();
        CostUpdate();
        if (currState == States.UmbralIce1 || currState == States.UmbralIce2 || currState == States.UmbralIce3)
            IceRegen();

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            FireAdjustments(currState);
            if (PlayerStats.target != null)
            {
                if (PlayerStats.currMana >= currFireCost)
                {
                    if (casting == false)
                    {
                        changer.sprite = firePic;
                        sliderObject.SetActive(true);
                        casting = true;
                        StartCoroutine(CastingUpdater());
                        PlayerStats.currMana -= (int)currFireCost;
                        damageText.text = "You did " + (currFirePotency * 180) + " to " + PlayerStats.target.gameObject.name;
                        damageHolder.SetActive(true);
                        Invoke("DamageTextToggle", 3);
                        sliderObject.SetActive(false);
                        casting = false;
                        CycleElement(currState, Element.Fire);
                    }
                    else
                    {
                        battleText.text = "Already casting";
                        noTargetText.gameObject.SetActive(true);
                        Invoke("TargetTextToggle", 1);
                    }

                }
                else
                {
                    battleText.text = "Not enough mana";
                    noTargetText.gameObject.SetActive(true);
                    Invoke("TargetTextToggle", 1);
                }

            }
            else
            {
                battleText.text = "No target selected";
                noTargetText.gameObject.SetActive(true);
                Invoke("TargetTextToggle", 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            IceAdjustments(currState);

            if (PlayerStats.target != null)
            {
                if (PlayerStats.currMana >= currBlizzCost)
                {
                    if (casting == false)
                    {
                        changer.sprite = blizzPic;
                        sliderObject.SetActive(true);
                        casting = true;
                        StartCoroutine(CastingUpdater());
                        PlayerStats.currMana -= (int)currBlizzCost;
                        damageText.text = "You did " + (currBlizzPotency * 180) + " to " + PlayerStats.target.gameObject.name;
                        damageHolder.SetActive(true);
                        Invoke("DamageTextToggle", 3);
                        sliderObject.SetActive(false);
                        casting = false;
                        CycleElement(currState, Element.Ice);
                    }
                    else
                    {
                        battleText.text = "Already casting";
                        noTargetText.gameObject.SetActive(true);
                        Invoke("TargetTextToggle", 1);
                    }

                }
                else
                {
                    battleText.text = "Not enough mana";
                    noTargetText.gameObject.SetActive(true);
                    Invoke("TargetTextToggle", 1);
                }
            }
            else
            {
                battleText.text = "No target selected";
                noTargetText.gameObject.SetActive(true);
                Invoke("TargetTextToggle", 1);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            if (casting == false)
                Transpose();
            else
            {
                battleText.text = "Already casting";
                noTargetText.gameObject.SetActive(true);
                Invoke("TargetTextToggle", 1);
            }
        }
    }

    void FireAdjustments(States currState) {
        switch (currState)
        {
            case States.AstralFire1:
                currFirePotency = baseFirePotency * 1.4f;
                currFireCost = baseFireManaCost* 2;
                break;
            case States.AstralFire2:
                currFirePotency = baseFirePotency * 1.6f;
                currFireCost = baseFireManaCost * 2;
                break;
            case States.AstralFire3:
                currFirePotency = baseFirePotency * 1.8f;
                currFireCost = baseFireManaCost * 2;
                break;
            case States.UmbralIce1:
                currFirePotency = baseFirePotency * 0.9f;
                break;
            case States.UmbralIce2:
                currFirePotency = baseFirePotency * 0.9f;
                break;
            case States.UmbralIce3:
                currFirePotency = baseFirePotency * 0.7f;
                break;
            default:
                currFirePotency = baseFirePotency;
                currFireCost = baseFireManaCost;
                break;
        }
    }

    void IceAdjustments(States currState) {
        switch (currState)
        {
            case States.AstralFire1:
                currBlizzPotency = baseBlizzPotency * 0.8f;
                break;
            case States.AstralFire2:
                currBlizzPotency = baseBlizzPotency * 0.8f;
                break;
            case States.AstralFire3:
                currBlizzPotency = baseBlizzPotency * 0.7f;
                break;
            case States.UmbralIce1:
                currBlizzPotency = baseBlizzPotency;
                currBlizzCost = baseblizzManaCost * 0.75f;
                manaRegen = 16;
                break;
            case States.UmbralIce2:
                currBlizzPotency = baseBlizzPotency;
                currBlizzCost = baseblizzManaCost * 0.75f;
                manaRegen = 23.5f;
                break;
            case States.UmbralIce3:
                currBlizzPotency = baseBlizzPotency;
                currBlizzCost = baseblizzManaCost * 0.75f;
                manaRegen = 31;
                break;
            default:
                currBlizzPotency = baseBlizzPotency;
                currBlizzCost = baseblizzManaCost;
                break;
        }
    }
    void IceRegen() {
        PlayerStats.currMana += baseRegen * (int)manaRegen;
        if (PlayerStats.currMana >= 10000)
            PlayerStats.currMana = 10000;
    }

    void CycleElement(States currState, Element type) {
        if (type == Element.Fire)
        {
            switch (currState)
            {
                case States.Neutral:
                    this.currState = States.AstralFire1;
                    break;
                case States.AstralFire1:
                    this.currState = States.AstralFire2;
                    break;
                case States.AstralFire2:
                case States.AstralFire3:
                    this.currState = States.AstralFire3;
                    break;
                default:
                    this.currState = States.Neutral;
                    break;
            }
        }
        else {
            switch (currState)
            {
                case States.Neutral:
                    this.currState = States.UmbralIce1;
                    break;
                case States.UmbralIce1:
                    this.currState = States.UmbralIce2;
                    break;
                case States.UmbralIce2:
                case States.UmbralIce3:
                    this.currState = States.UmbralIce3;
                    break;
                default:
                    this.currState = States.Neutral;
                    break;
            }
        }
    }

    void CostUpdate() {
        fireCost.text = currFireCost.ToString();
        blizzCost.text = currBlizzCost.ToString();
    }
    void DamageTextToggle()
    {
        damageHolder.SetActive(false);
    }
    void TargetTextToggle() {
        noTargetText.SetActive(false);
    }

    void Transpose() {
        switch (currState) {
            case States.Neutral:
                battleText.text = "No effect";
                noTargetText.gameObject.SetActive(true);
                Invoke("TargetTextToggle", 1);
                break;
            case States.AstralFire1:
            case States.AstralFire2:
            case States.AstralFire3:
                currState = States.UmbralIce1;
                for (int i = 0; i < fireStacks.gameObject.transform.childCount; i++)
                {
                    fireStacks.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    stackText.text = "Neutral";
                }
                break;
            case States.UmbralIce1:
            case States.UmbralIce2:
            case States.UmbralIce3:
                currState = States.AstralFire1;
                for (int i = 0; i < blizzStacks.gameObject.transform.childCount; i++)
                {
                    blizzStacks.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    stackText.text = "Neutral";
                }
                break;
        }
    }
    void StacksUpdate() {
        switch (currState) {
            case States.Neutral:
                for (int i = 0; i < blizzStacks.gameObject.transform.childCount; i++) {
                    blizzStacks.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    fireStacks.gameObject.transform.GetChild(i).gameObject.SetActive(false);
                    stackText.text = "Neutral";
                }
                break;
            case States.AstralFire1:
                fireStacks.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                stackText.text = "Astral Fire";
                break;
            case States.AstralFire2:
                fireStacks.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                stackText.text = "Astral Fire";
                break;
            case States.AstralFire3:
                fireStacks.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                stackText.text = "Astral Fire";
                break;
            case States.UmbralIce1:
                blizzStacks.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                stackText.text = "Umbral Ice";
                break;
            case States.UmbralIce2:
                blizzStacks.gameObject.transform.GetChild(1).gameObject.SetActive(true);
                stackText.text = "Umbral Ice";
                break;
            case States.UmbralIce3:
                blizzStacks.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                stackText.text = "Umbral Ice";
                break;
        }
    }
    IEnumerator CastingUpdater() {
        bool done = false;
        castTimeSlider.maxValue = castTime;
        castTimeSlider.value = 0;
        while (done == false) {
            castTimeSlider.value += (Time.deltaTime / 2);
            if (castTimeSlider.value <= castTimeSlider.maxValue)
                done = true;
            //Debug.Log("Done");
        }
        yield return null;
    }
}
