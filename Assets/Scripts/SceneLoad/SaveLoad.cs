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
        PlayerPrefs.SetInt("isDialog", 0);
        PlayerPrefs.SetInt("FirstData", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Feedback1");
    }

    public void OnRestartSceneClick()
    {
        PlayerPrefs.SetInt("isDialog", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Prototype1");
    }
}
