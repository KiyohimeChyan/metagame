using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public Image healthBar;
    public Image healthDelayBar;
    public Image energyBar;
    public bool isLevel2;
    public GameObject levelBG;
    public Sprite blue;
    public Sprite red;
    public Sprite green;

    bool isRecover;
    CharacterStats characterStats;

    private void Awake()
    {
        string num = PlayerPrefs.GetString("HPColor");
        if (num == "Red HP Bar")
        {
            healthBar.sprite = red;
        }else if (num == "Green HP Bar")
        {
            healthBar.sprite = green;
        }
        else
        {
            healthBar.sprite = blue;
        }
        if (isLevel2)
        {
            string bg = PlayerPrefs.GetString("BackgroundStyle");
            Debug.Log(bg);
            if (bg == "Add Cave Background")
            {
                levelBG.SetActive(false);
            }
        }
        healthBar.type = Image.Type.Filled;
    }
    private void Update()
    {
        if (!isLevel2)
        {
            if (healthDelayBar.fillAmount > healthBar.fillAmount)
            {
                healthDelayBar.fillAmount -= Time.deltaTime * 0.75f;
            }
        }
        if (isRecover)
        {
            float persentage = characterStats.CurrentPower / characterStats.MaxPower;
            energyBar.fillAmount = persentage;

            if (persentage >= 1)
            {
                isRecover = false;
                return;
            }
        }
    }

    public void OnHealthChange(float persentage)
    {
        healthBar.fillAmount = persentage;
    }

    //监听后执行的事件
    public void OnPowerChange(CharacterStats cs)
    {
        isRecover = true;
        characterStats = cs;
    }

}
