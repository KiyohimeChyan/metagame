using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncSceneLoad : MonoBehaviour
{
    public string SceneName;
    private AsyncOperation async = null;

    void Start()
    {
        StartCoroutine(AsyncLoading());
    }

    IEnumerator AsyncLoading()
    {
        //异步加载场景
        async = SceneManager.LoadSceneAsync(SceneName);
        //阻止当加载完成自动切换
        async.allowSceneActivation = false;
        yield return new WaitForSeconds(2.8f);
        async.allowSceneActivation = true;

    }

}
