using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public Image healthBar;
    public Image healthDelayBar;
    public Image energyBar;

    bool isRecover;
    CharacterStats characterStats;
    private void Update()
    {
        if (healthDelayBar.fillAmount > healthBar.fillAmount)
        {
            healthDelayBar.fillAmount -= Time.deltaTime*0.75f;
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
