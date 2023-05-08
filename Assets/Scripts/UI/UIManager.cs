using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerStatUI playerStatUI;
    //事件监听
    public CharacterEventSO characterEventSO;

    private void OnEnable()
    {
        characterEventSO.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        characterEventSO.OnEventRaised -= OnHealthEvent;
    }

    //监听Player每次血量变化
    private void OnHealthEvent(CharacterStats cs)
    {
        float persentage = (float)cs.CurrentHealth / cs.MaxHealth;
        playerStatUI.OnHealthChange(persentage);
        playerStatUI.OnPowerChange(cs);
    }

    public void Restart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
