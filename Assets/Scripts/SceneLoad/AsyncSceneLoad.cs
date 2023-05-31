using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AsyncSceneLoad : MonoBehaviour
{
    private AsyncOperation async = null;
    public GameObject feedback;
    int currentScene;
    string sceneName;

    public TMP_Text change1;
    public TMP_Text change2;
    public TMP_Text change3;
    private void Awake()
    {
        currentScene = PlayerPrefs.GetInt("currentScene");
        if(currentScene == 2)
        {
            sceneName = "Prototype2";
            change1.text = PlayerPrefs.GetString("BackgroundStyle");
            change2.text = PlayerPrefs.GetString("HPColor");
            change3.text = PlayerPrefs.GetString("Visual");
        }else if(currentScene == 3)
        {
            sceneName = "Prototype3";
            change1.text = PlayerPrefs.GetString("V2");
            change2.text = "Add New Ability";
            change3.text = "Adjust Tutorial";
        }else if(currentScene == 4)
        {
            sceneName = "Start";
            feedback.SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(AsyncLoading());  
    }

    IEnumerator AsyncLoading()
    {
        //异步加载场景
        async = SceneManager.LoadSceneAsync(sceneName);
        //阻止当加载完成自动切换
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(2.8f);
        async.allowSceneActivation = true;

    }

}
