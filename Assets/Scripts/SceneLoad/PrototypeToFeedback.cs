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
    public TMP_Text timeCount;
    public TMP_Text resultTime;
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
                score *= 1.5f;
                fullHPGroup.SetActive(true);
            }

            resultTime.text = "Score: " + score.ToString("f2");
        }

        gameResetPanel.SetActive(true);
        selectedButton.Select();
        result.text = "You win!";
    }
}
