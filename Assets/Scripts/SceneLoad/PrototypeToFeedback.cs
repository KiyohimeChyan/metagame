using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PrototypeToFeedback : MonoBehaviour
{
    public GameObject gameResetPanel;
    public TMP_Text result;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameResetPanel.SetActive(true);
        result.text = "You win!";
    }
}
