using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public int currentScene;
    public void OnFeedBackClick()
    {
        PlayerPrefs.SetInt("currentScene", currentScene);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Feedback1");
    }
}
