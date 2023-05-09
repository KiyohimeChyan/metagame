using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneLoad : MonoBehaviour
{
    private AsyncOperation async = null;
    int currentScene;
    string sceneName;
    private void Awake()
    {
        currentScene = PlayerPrefs.GetInt("currentScene");
        if(currentScene == 2)
        {
            sceneName = "Prototype2";
        }else if(currentScene == 3)
        {
            sceneName = "Prototype3";
        }else if(currentScene == 4)
        {
            sceneName = "Start";
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
