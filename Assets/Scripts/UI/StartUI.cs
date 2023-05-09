using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartUI : MonoBehaviour
{
    string currentName;
    public GameObject fallingText;
    public Transform generateTrans1;
    public Transform generateTrans2;
    public Transform generateTrans3;
    Transform generateTrans;
    int lastName;
    string[] nameGroup;
    private void Awake()
    {
        currentName = PlayerPrefs.GetString("currentName");
    }

    private void Start()
    {
        nameGroup = currentName.Split(",");
        lastName = nameGroup.Length - 1;
        InstantiateFallingText();
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
            if (i == lastName)
            {
                text.GetComponent<TMP_Text>().color = Color.red;
            }
        }
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene("Prototype1");
    }


}
