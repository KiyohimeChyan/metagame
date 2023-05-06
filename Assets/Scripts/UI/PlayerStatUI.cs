using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatUI : MonoBehaviour
{
    public Image healthBar;
    public Image healthDelayBar;
    public Image energyBar;

    private void Update()
    {
        if (healthDelayBar.fillAmount > healthBar.fillAmount)
        {
            healthDelayBar.fillAmount -= Time.deltaTime*0.75f;
        }
    }

    public void OnHealthChange(float persentage)
    {
        healthBar.fillAmount = persentage;
    }
}
