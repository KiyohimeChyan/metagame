using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerStatUI playerStatUI;
    //�¼�����
    public CharacterEventSO characterEventSO;
    public GameObject designerDialogGroup;

    //��Ϸ����UI
    public GameObject gameResetPanel;
    public PlayerInputControlls inputs;
    public TMP_Text resultText;
    public Button restartButton;
    bool isOpen;

    //�������UI
    public GameObject gameDataPanel;
    public Button attackDataButton;

    public int testInt;

    private void Awake()
    {
        inputs = new PlayerInputControlls();
        inputs.Enable();
    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("isDialog") == testInt)
        {
            designerDialogGroup.SetActive(true);
        }

    }


    private void OnEnable()
    {
        inputs.Gameplay.Setttings.started += OnSettingsPressed;
        characterEventSO.OnEventRaised += OnHealthEvent;
    }

    private void OnDisable()
    {
        inputs.Gameplay.Setttings.started -= OnSettingsPressed;
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
        PlayerPrefs.SetInt("isDialog",1);
        SceneManager.LoadScene(sceneName);
    }

    public void OnSettingsClick()
    {
        resultText.text = "Paused";
        gameResetPanel.SetActive(true);
    }

    public void OnDataClick()
    {
        gameDataPanel.SetActive(true);
        gameResetPanel.SetActive(false);
        attackDataButton.Select();
    }

    public void OnDataSet(Slider slide)
    {
        slide.Select();
    }

    public void OnValueChanged()
    {
        Debug.Log("aaa");
    }

    public void OnDoneButtonClick()
    {
        //TODO: �����޸ĵ�����=============================
        gameDataPanel.SetActive(false);
        gameResetPanel.SetActive(true);
        isOpen = true;
    }

    private void OnSettingsPressed(InputAction.CallbackContext obj)
    {
        resultText.text = "Paused";
        gameResetPanel.SetActive(!isOpen);
        isOpen = !isOpen;
        restartButton.Select();
    }

}
