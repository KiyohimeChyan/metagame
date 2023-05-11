using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;


public class NPCInteract : MonoBehaviour,IInteractable
{
    //要不要再次交互
    public bool isDone;
    public GameObject NPCDialog;
    public PlayerController playerController;

    public PlayerInputControlls inputs;


    public TextAsset dialogDataFile;

    //对话内容文本
    public TMP_Text dialogText;
    public TMP_Text nameText;

    //对话文本-按行分割
    public string[] dialogRows;

    //对话索引
    public int dialogIndex;

    //对话图片
    public List<Sprite> sprites = new List<Sprite>();
    public Image speakingProfile;

    string at;
    string comma;

    public float pressTimer;
    float pressTime;

    private void Awake()
    {
        inputs = new PlayerInputControlls();
        inputs.Enable();
    }
    private void OnEnable()
    {
        inputs.Gameplay.Dialog.started += OnDialogChange;
    }
    private void OnDisable()
    {
        inputs.Gameplay.Dialog.started -= OnDialogChange;

    }

    void Start()
    {
        at = "@";
        comma = ",";
        ReadText(dialogDataFile);
        ShowDialog();
    }

    private void Update()
    {
        pressTime -= Time.deltaTime;
    }

    public void UpdateText(string _text)
    {
        dialogText.text = Regex.Replace(_text, at, comma);
    }

    public void UpdateName(string text)
    {
        nameText.text = text;
    }

    public void UpdateImage(int num)
    {
        speakingProfile.sprite = sprites[num];
    }

    public void ReadText(TextAsset _textAsset)
    {
        dialogRows = _textAsset.text.Split('\n');
        //foreach(var row in rows)
        //{
        //    string[] cell = row.Split(',');
        //}

    }

    public void ShowDialog()
    {
        for (int i = 0; i < dialogRows.Length; i++)
        {
            string[] cells = dialogRows[i].Split(',');
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2]);
                UpdateName(cells[6]);
                UpdateImage(int.Parse(cells[5]));
                dialogIndex = int.Parse(cells[3]);
                break;
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                NPCDialog.SetActive(false);
                playerController.isReading = false;
            }
        }
    }



    public void OptionEffect(string effect)
    {
        //if (effect == "M1")
        //{
        //    currentDistance = Mathf.Max(currentDistance - 1, 0);
        //}
        //if (effect == "P1")
        //{
        //    currentDistance += 1;
        //}

    }

    public void OnClickNext()
    {
        ShowDialog();
    }

    private void OnDialogChange(InputAction.CallbackContext obj)
    {
        if (pressTime <= 0)
        {
            ShowDialog();
            pressTime = pressTimer;

        }
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            ShowNPCDialog();
        }
    }

    private void ShowNPCDialog()
    {
        NPCDialog.SetActive(true);
        playerController.isReading = true;
        isDone = true;
        this.gameObject.tag = "Untagged";
    }
}
