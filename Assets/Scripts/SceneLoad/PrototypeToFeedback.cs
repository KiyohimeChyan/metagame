using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class PrototypeToFeedback : MonoBehaviour
{
    public bool isLevel3;
    public GameObject gameResetPanel;
    public TMP_Text result;
    public Button selectedButton;
    public GameObject secondButton;
    public TMP_Text timeCount;
    public TMP_Text resultTime;
    public TMP_Text dataText;
    float usedTime;
    float score;
    public float damageTaken;
    public GameObject fullHPGroup;

    private void Update()
    {
        if (isLevel3)
        {
            usedTime += Time.deltaTime;
            timeCount.text = usedTime.ToString("f2");
        }
    }

    public void PlayerGetHurt()
    {
        damageTaken++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLevel3)
        {
            score = usedTime;
            if (damageTaken == 0)
            {
                score /= 2.0f;
                fullHPGroup.SetActive(true);
            }

            resultTime.text = "Score: " + score.ToString("f2");
            if (PlayerPrefs.GetInt("FirstData") == 0)
            {
                PlayerPrefs.SetInt("FirstData", 1);
                PlayerPrefs.Save();
                dataText.text = "Excellent! Can you help us adjust the values to your personal preference?";
            }
            else
            {
                dataText.text = "";
                secondButton.SetActive(true);
            }
        }
        gameResetPanel.SetActive(true);
        selectedButton.Select();
        result.text = "You win!";
        Destroy(this.gameObject);
    }
}
