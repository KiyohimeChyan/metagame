using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    string currentName;
    public GameObject fallingText;
    public Transform generateTrans1;
    public Transform generateTrans2;
    public Transform generateTrans3;
    public Button start;
    Transform generateTrans;
    int lastName;
    public string[] nameGroup;
    int nameColor;
    private void Awake()
    {
        currentName = PlayerPrefs.GetString("currentName");
    }

    private void Start()
    {
        nameColor = PlayerPrefs.GetInt("nameColor");
        nameGroup = currentName.Split(",");
        lastName = nameGroup.Length - 1;
        InstantiateFallingText();
        start.Select();
    }

    void InstantiateFallingText()
    {
        for(int i=0; i < nameGroup.Length; i++)
        {
            int x = Random.Range(0, 3);
            switch (x)
            {
                case 0:
                    generateTrans = generateTrans1;
                    break;
                case 1:
                    generateTrans = generateTrans2;
                    break;
                case 2:
                    generateTrans = generateTrans3;
                    break;
                default:
                    break;
            }
            GameObject text = Instantiate(fallingText, generateTrans);
            text.GetComponent<TMP_Text>().text = nameGroup[i];
            if (i == lastName&&nameColor==1)
            {
                text.GetComponent<TMP_Text>().color = new Color(1.0f, 0.9333333f, 0.372549f,1.0f);
            }
        }
    }

    public void OnStartClick()
    {
        PlayerPrefs.SetInt("isDialog", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Prototype1");
    }

    public void OnQuitClick()
    {
        PlayerPrefs.SetInt("nameColor", 0);
        PlayerPrefs.Save();
        Application.Quit();
    }

}
