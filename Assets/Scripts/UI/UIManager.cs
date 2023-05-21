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
    //事件监听
    public CharacterEventSO characterEventSO;
    public GameObject designerDialogGroup;

    //游戏结束UI
    public GameObject gameResetPanel;
    public PlayerInputControlls inputs;
    public TMP_Text resultText;
    public Button restartButton;
    bool isOpen;


    //数据设计UI
    public GameObject gameDataPanel;
    public Button attackDataButton;
    Button currentButton;

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

    //监听Player每次血量变化
    private void OnHealthEvent(CharacterStats cs)
    {
        float persentage = (float)cs.CurrentHealth / cs.MaxHealth;
        playerStatUI.OnHealthChange(persentage);
        playerStatUI.OnPowerChange(cs);
        if (cs.CurrentHealth <= 0)
        {
            Debug.Log("111");
            StartCoroutine(ShowGameSetPanel());
        }
    }

    IEnumerator ShowGameSetPanel()
    {
        yield return new WaitForSeconds(2.0f);
        resultText.text = "You are Dead";
        gameResetPanel.SetActive(true);
        Debug.Log("222");
        restartButton.Select();

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
        restartButton.Select();
    }

    public void OnDataClick()
    {
        gameDataPanel.SetActive(true);
        gameResetPanel.SetActive(false);
        attackDataButton.Select();
    }

    //public void OnDataSet(Slider slide)
    //{
    //    slide.Select();
    //}

    //public void OnDataChoosed(Button button)
    //{
    //    inputs.Gameplay.Jump.started += OnSlideSelected;
    //    currentButton = button;
    //}

    //private void OnSlideSelected(InputAction.CallbackContext obj)
    //{
    //    currentButton.Select();
    //    Debug.Log("back");
    //    inputs.Gameplay.Jump.started -= OnSlideSelected;

    //}


    //public void OnDoneButtonClick()
    //{
    //    //TODO: 保存修改的数据=============================
    //    gameDataPanel.SetActive(false);
    //    gameResetPanel.SetActive(true);
    //    isOpen = true;
    //}

    private void OnSettingsPressed(InputAction.CallbackContext obj)
    {
        resultText.text = "Paused";
        gameResetPanel.SetActive(!isOpen);
        isOpen = !isOpen;
        restartButton.Select();
    }

}
