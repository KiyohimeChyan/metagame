using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;
using TMPro;
using UnityEngine.UI;

public class DataSetUI : MonoBehaviour
{
    public GameObject gameResetPanel;
    public GameObject gameDataPanel;
    public Button dataButton;
    Button currentButton;
    public PlayerInputControlls inputs;


    public CharaterData_SO boarHP;
    public AttackData_SO boarAttack;
    public Slider HPSlide;
    public Slider AttackSlide;
    public TMP_Text HPtext;
    public TMP_Text attackText;

    private void OnEnable()
    {
        inputs = new PlayerInputControlls();
        inputs.Enable();
        boarHP.currentHealth = 1;
    }

    private void Update()
    {
        HPtext.text = ""+(HPSlide.value * 5.0f + 2.5f);
    }

    public void OnDataSet(Slider slide)
    {
        slide.Select();
    }

    public void OnDataChoosed(Button button)
    {
        inputs.Gameplay.Jump.started += OnSlideSelected;
        currentButton = button;
    }

    private void OnSlideSelected(InputAction.CallbackContext obj)
    {
        currentButton.Select();
        Debug.Log("back");
        inputs.Gameplay.Jump.started -= OnSlideSelected;

    }


    public void OnDoneButtonClick()
    {
        //TODO: 保存修改的数据=============================
        gameDataPanel.SetActive(false);
        gameResetPanel.SetActive(true);
        dataButton.Select();
    }
}
