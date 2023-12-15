using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.InputSystem;

public class TSceneManager : TSingleton<TSceneManager>
{
    [SerializeField]
    private Animator at;

    public string currentSceneName;

    public Text percent;

    public List<TSceneEntity> scenes;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        ResetCanvas();
    }

    private void ResetCanvas()
    {
        at.SetInteger("Status", (int)TSwitchSceneStatus.Reset);
        percent.text = "0.0%";
    }

    private void ShowCanvas()
    {
        at.SetInteger("Status", (int)TSwitchSceneStatus.Show);
        percent.text = "0.0%";
    }

    private string GetNameByKey(TSceneKey key)
    {
        var s = scenes.Find(s => s.key == key);
        return s != null ? s.name : "Start";
    }

    private IEnumerator SwitchScene(string name, Action OnFinish, bool autoEnter)
    {
        ShowCanvas();
        currentSceneName = name;
        var load = SceneManager.LoadSceneAsync(name);
        load.allowSceneActivation = false;
        while (!load.isDone)
        {
            if (load.progress < 0.9f)
            {
                percent.text = $"{load.progress * 100.0f}";
            }
            else
            {
                percent.text = "100.0%";
                if (autoEnter)
                {
                    yield return new WaitForSeconds(1.0f);
                    load.allowSceneActivation = true;
                    yield return null;
                    ResetCanvas();
                    OnFinish();
                    yield break;
                }
                else
                {
                    if (Input.touchCount != 0 || Mouse.current.leftButton.isPressed)
                    {
                        load.allowSceneActivation = true;
                        yield return null;
                        ResetCanvas();
                        OnFinish();
                        yield break;
                    }
                    else
                    {
                        at.SetInteger("Status", (int)TSwitchSceneStatus.Wait);
                    }

                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="key">场景枚举</param>
    /// <param name="Enter">是否加载完成后自动进入</param>
    /// <param name="OnFinish">加载完成后回调</param>
    /// <param name="autoEnter">是否自动进入</param>
    public void LoadScene(TSceneKey key, Action OnFinish, bool autoEnter)
    {
        string name = GetNameByKey(key);
        if (name == currentSceneName)
        {
            Debug.Log("不可加载同场景！");
        }
        else
        {
            StartCoroutine(SwitchScene(name, OnFinish, autoEnter));
        }
    }
}

[System.Serializable]
public class TSceneEntity
{
    [Tooltip("关卡名称")]
    public string name;

    [Tooltip("关卡枚举")]
    public TSceneKey key;

}

public enum TSceneKey
{
    /// <summary>
    /// 主菜单
    /// </summary>
    [Tooltip("主菜单")]
    Start,
    /// <summary>
    /// 主场景
    /// </summary>
    [Tooltip("主世界")]
    Fracture,
}

public enum TSwitchSceneStatus
{
    Reset,
    Show,
    Wait
}