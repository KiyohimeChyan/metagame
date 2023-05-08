using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerStatUI playerStatUI;
    //�¼�����
    public CharacterEventSO characterEventSO;

    private void OnEnable()
    {
        characterEventSO.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        characterEventSO.OnEventRaised -= OnHealthEvent;
    }

    //����Playerÿ��Ѫ���仯
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
