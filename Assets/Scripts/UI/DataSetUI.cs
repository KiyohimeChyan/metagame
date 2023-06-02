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
    public Button enemyDefaultButton;
    public Button playerDefaultButton;
    public GameObject enemyGroup;
    public GameObject playerGroup;
    public PlayerInputControlls inputs;

    bool isPlayerOpen;
    bool isEnemyOpen;

    [Header("玩家数值")]
    public AttackData_SO playerAttack;
    public CharaterData_SO playerHP;
    public PlayerStats_SO playerBasic;
    public Slider HPSlidePlayer;
    public Slider AttackSlidePlayer;
    public Slider SpeedSlide;
    public Slider JumpForceSlide;
    public Slider SlideDistanceSlider;
    public TMP_Text HPtextPlayer;
    public TMP_Text attackTextPlayer;
    public TMP_Text jumpTextPlayer;
    public TMP_Text slideTextPlayer;
    public TMP_Text speedTextPlayer;



    [Header("豪猪数值")]
    public CharaterData_SO boarHP;
    public AttackData_SO boarAttack;
    public Slider HPSlideBoar;
    public Slider AttackSlideBoar;
    public TMP_Text HPtextBoar;
    public TMP_Text attackTextBoar;

    [Header("蜜蜂数值")]
    public CharaterData_SO beeHP;
    public AttackData_SO beeAttack;
    public Slider HPSlideBee;
    public Slider AttackSlideBee;
    public TMP_Text HPtextBee;
    public TMP_Text attackTextBee;

    [Header("蜗牛数值")]
    public CharaterData_SO snailHP;
    public AttackData_SO snailAttack;
    public Slider HPSlideSnail;
    public Slider AttackSlideSnail;
    public TMP_Text HPtextSnail;
    public TMP_Text attackTextSnail;

    bool isCheat;
    int currentMode;
    public Button cheatMode;
    public Sprite cheatOn;
    public Sprite cheatOnS;
    public Sprite cheatOff;
    public Sprite cheatOffS;
    public Sprite buttonSelect;
    public Sprite buttonOrigin;
    SpriteState spriteState;

    private void OnEnable()
    {
        inputs = new PlayerInputControlls();
        inputs.Enable();
        spriteState = new SpriteState();
        spriteState = cheatMode.spriteState;
        if (isCheat)
        {
            spriteState.selectedSprite = cheatOnS;
        }
        else
        {
            spriteState.selectedSprite = cheatOffS;
        }
        cheatMode.spriteState = spriteState;
        currentMode = PlayerPrefs.GetInt("isCheat");
        if (currentMode == 1)
        {
            isCheat = true;
        }
        else
        {
            isCheat = false;
        }
        playerGroup.SetActive(false);

        inputs.Gameplay.Next.started += OnRBClick;
        inputs.Gameplay.Last.started += OnLBClick;

        HPSlideBoar.value = (boarHP.maxHealth - 3.0f);
        AttackSlideBoar.value = (boarAttack.minDamage - 1.0f);

        HPSlideBee.value = (beeHP.maxHealth - 1.0f);
        AttackSlideBee.value = (beeAttack.minDamage - 2.0f);

        HPSlideSnail.value = (snailHP.maxHealth - 2.0f);
        AttackSlideSnail.value = (snailAttack.minDamage);

        HPSlidePlayer.value = (playerHP.maxHealth - 5.0f);
        AttackSlidePlayer.value = (playerAttack.minDamage - 1.0f);
        SpeedSlide.value = (playerBasic.moveSpeed - 300.0f) / 50.0f;
        JumpForceSlide.value = (playerBasic.jumpForce - 17.5f) / 0.5f;
        SlideDistanceSlider.value = (playerBasic.slideDistance - 5.0f);

        isPlayerOpen = false;
        isEnemyOpen = true;
    }


    private void OnDisable()
    {
        inputs.Gameplay.Next.started -= OnRBClick;
        inputs.Gameplay.Last.started -= OnLBClick;
    }

    private void Update()
    {
        if (isEnemyOpen)
        {
            HPtextBoar.text = "" + (HPSlideBoar.value + 3f);
            attackTextBoar.text = "" + (AttackSlideBoar.value + 1.0f);

            HPtextBee.text = "" + (HPSlideBee.value + 1f);
            attackTextBee.text = "" + (AttackSlideBee.value + 2.0f);

            HPtextSnail.text = "" + (HPSlideSnail.value + 2f);
            attackTextSnail.text = "" + (AttackSlideSnail.value);
        }

        if (isPlayerOpen&&!isCheat)
        {
            HPtextPlayer.text = "" + (HPSlidePlayer.value + 5f);
            attackTextPlayer.text = "" + (AttackSlidePlayer.value+1.0f);
            speedTextPlayer.text = "" + (SpeedSlide.value * 50.0f + 300.0f);
            jumpTextPlayer.text = "" + (JumpForceSlide.value * 0.5f + 17.5f);
            slideTextPlayer.text = "" + (SlideDistanceSlider.value + 5.0f);
        }
    }


    private void OnLBClick(InputAction.CallbackContext obj)
    {
        isPlayerOpen = !isPlayerOpen;
        isEnemyOpen = !isEnemyOpen;
        playerGroup.SetActive(isPlayerOpen);
        enemyGroup.SetActive(isEnemyOpen);
        if (isEnemyOpen)
        {
            SavePlayerData();
            enemyDefaultButton.Select();
        }
        if (isPlayerOpen)
        {
            SaveEnemyData();
            playerDefaultButton.Select();
        }

    }

    private void OnRBClick(InputAction.CallbackContext obj)
    {
        isPlayerOpen = !isPlayerOpen;
        isEnemyOpen = !isEnemyOpen;
        playerGroup.SetActive(isPlayerOpen);
        enemyGroup.SetActive(isEnemyOpen);
        if (isEnemyOpen)
        {
            SavePlayerData();
            enemyDefaultButton.Select();
        }
        if (isPlayerOpen)
        {
            SaveEnemyData();
            playerDefaultButton.Select();
        }

    }

    public void OnDataSet(Slider slide)
    {
        slide.Select();
    }

    public void OnDataChoosed(Button button)
    {
        inputs.Gameplay.Jump.started += OnSlideSelected;
        currentButton = button;
        currentButton.GetComponent<Image>().sprite = buttonSelect;
    }

    private void OnSlideSelected(InputAction.CallbackContext obj)
    {
        currentButton.Select();
        currentButton.GetComponent<Image>().sprite = buttonOrigin;

        Debug.Log("back");
        inputs.Gameplay.Jump.started -= OnSlideSelected;

    }




    public void OnDoneButtonClick()
    {
        if (!isCheat)
        {
            SaveEnemyData();
            SavePlayerData();
        }
        //TODO: 保存修改的数据=============================
        gameDataPanel.SetActive(false);
        gameResetPanel.SetActive(true);
        dataButton.Select();
    }

    private void SaveEnemyData()
    {
        boarHP.maxHealth = (int)(HPSlideBoar.value + 3.0f);
        boarHP.currentHealth = (int)(HPSlideBoar.value + 3.0f);
        boarAttack.minDamage = (int)(AttackSlideBoar.value + 1.0f);
        boarAttack.maxDamage = (int)(AttackSlideBoar.value + 1.0f);

        beeHP.maxHealth = (int)(HPSlideBee.value + 1.0f);
        beeHP.currentHealth = (int)(HPSlideBee.value + 1.0f);
        beeAttack.minDamage = (int)(AttackSlideBee.value + 2.0f);
        beeAttack.maxDamage = (int)(AttackSlideBee.value + 2.0f);

        snailHP.maxHealth = (int)(HPSlideSnail.value + 2.0f);
        snailHP.currentHealth = (int)(HPSlideSnail.value + 2.0f);
        snailAttack.minDamage = (int)(AttackSlideSnail.value);
        snailAttack.maxDamage = (int)(AttackSlideSnail.value);
    }

    public void OnCheatModeOn()
    {
        isCheat = !isCheat;
        spriteState = cheatMode.spriteState;
        if (isCheat)
        {
            PlayerPrefs.SetInt("isCheat", 1);
            cheatMode.GetComponent<Image>().sprite = cheatOn;
            spriteState.selectedSprite = cheatOnS;
        }
        else
        {
            PlayerPrefs.SetInt("isCheat", 0);
            cheatMode.GetComponent<Image>().sprite = cheatOff;
            spriteState.selectedSprite = cheatOffS;
        }
        cheatMode.spriteState = spriteState;
        PlayerPrefs.Save();
        speedTextPlayer.text = "1500";
        attackTextPlayer.text = "100";
        HPtextPlayer.text = "100";
        jumpTextPlayer.text = "50";
        slideTextPlayer.text = "25" ;
        playerBasic.moveSpeed = 1500.0f;
        playerBasic.jumpForce = 50.0f;
        playerBasic.slideDistance = 25.0f;
        playerHP.maxHealth = 100;
        playerHP.currentHealth = 100;
        playerAttack.minDamage = 100;
        playerAttack.maxDamage = 100;
        HPSlidePlayer.value = 100;
        AttackSlidePlayer.value = 100;
        SlideDistanceSlider.value = 100;
        JumpForceSlide.value = 100;
        SpeedSlide.value = 100;
    }

    private void SavePlayerData()
    {
        playerBasic.moveSpeed = SpeedSlide.value * 50.0f + 300.0f;
        playerBasic.jumpForce = JumpForceSlide.value * 0.5f + 17.5f;
        playerBasic.slideDistance = SlideDistanceSlider.value + 5.0f;
        playerHP.maxHealth = (int)(HPSlidePlayer.value + 5f);
        playerHP.currentHealth = (int)(HPSlidePlayer.value + 5f);
        playerAttack.minDamage = (int)(AttackSlidePlayer.value + 1.0f);
        playerAttack.maxDamage = (int)(AttackSlidePlayer.value + 1.0f);
    }
}
