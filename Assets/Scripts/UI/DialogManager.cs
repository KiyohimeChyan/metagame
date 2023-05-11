using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public GameObject getNameUI;
    public PlayerInputControlls inputs;

    //数据读取
    private int currentScene;
    public TextAsset dialogDataFile2;
    public TextAsset dialogDataFile3;
    public TextAsset dialogDataFile4;

    //对话内容文本
    public TMP_Text dialogText;

    //对话文本-按行分割
    public string[] dialogRows;

    //对话索引
    public int dialogIndex;

    //对话图片
    public List<Sprite> sprites = new List<Sprite>();
    public Image speakingProfile;

    public Button nextButton;
    public bool isOption;

    string at;
    string comma;

    //选项预制体
    public GameObject optionalButton;

    public Transform buttonGroup;

    public float pressTimer;
    float pressTime;

    private void Awake()
    {
        inputs = new PlayerInputControlls();
        inputs.Enable();
        currentScene = PlayerPrefs.GetInt("currentScene");
    }
    private void OnEnable()
    {
        inputs.Gameplay.Dialog.canceled += OnDialogChange;
    }
    private void OnDisable()
    {
        inputs.Gameplay.Dialog.started -= OnDialogChange;

    }

    void Start()
    {
        at = "@";
        comma = ",";
        if(currentScene == 2)
        {
            ReadText(dialogDataFile2);
        }else if(currentScene == 3)
        {
            ReadText(dialogDataFile3);
        }else if(currentScene == 4)
        {
            ReadText(dialogDataFile4);
        }
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
                UpdateImage(int.Parse(cells[5]));
                dialogIndex = int.Parse(cells[3]);
                nextButton.gameObject.SetActive(true);
                isOption = false;
                break;
            }
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex)
            {
                nextButton.gameObject.SetActive(false);
                isOption = true;
                GenerateButton(i);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                if (currentScene!=4)
                {
                    //==============================================暂时使用异步加载===============================================================
                    SceneManager.LoadScene("AsyncScene");
                }
                else
                {
                    getNameUI.SetActive(true);
                }
            }
        }
    }


    public void GenerateButton(int index)
    {
        string[] cells = dialogRows[index].Split(',');
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionalButton, buttonGroup);
            button.GetComponentInChildren<TMP_Text>().text = Regex.Replace(cells[2], at, comma);
            button.GetComponent<Button>().Select();
            button.GetComponent<Button>().onClick.AddListener
                (
                    delegate
                    {
                        OnOptionClick(int.Parse(cells[3]));
                        //执行效果
                        if (cells[4] != "")
                        {
                            OptionEffect(cells[4]);
                        }
                    }
                );
            GenerateButton(index + 1);

        }

    }


    public void OnOptionClick(int id)
    {
        dialogIndex = id;
        ShowDialog();
        //Invoke("ShowDialog", 7.0f);
        pressTime = pressTimer;
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
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
        if (!isOption&&pressTime <=0)
        {
            ShowDialog();
            pressTime = pressTimer;

        }
    }
}
