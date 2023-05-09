using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GetNameUI : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject textReminder;
    string currentName;

    private void OnEnable()
    {
        currentName = PlayerPrefs.GetString("currentName");
    }

    public void OnConfirmNameClick()
    {
        if (inputField.text != "")
        {
            currentName = currentName +  ",";
            currentName = currentName + inputField.text;
            Debug.Log(currentName);
            PlayerPrefs.SetString("currentName", currentName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("AsyncScene");
        }
        else
        {
            textReminder.SetActive(true);
        }
    }
}
