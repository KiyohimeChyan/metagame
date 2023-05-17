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
    float usedTime;

    private void Update()
    {
        if (isLevel3)
        {
            usedTime += Time.deltaTime;
            timeCount.text = usedTime.ToString("f2");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameResetPanel.SetActive(true);
        selectedButton.Select();
        result.text = "You win!";
    }
}
