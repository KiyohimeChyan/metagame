using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PrototypeToFeedback : MonoBehaviour
{
    public GameObject gameResetPanel;
    public TMP_Text result;
    public Button selectedButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameResetPanel.SetActive(true);
        selectedButton.Select();
        result.text = "You win!";
    }
}
