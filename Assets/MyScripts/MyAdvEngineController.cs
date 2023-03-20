using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utage;
using UtageExtensions;
using UnityEngine.SceneManagement;


public class MyAdvEngineController : CreateSingleton<MyAdvEngineController>
{
    public bool IsPlaying { get; private set; }

    float defaultSpeed = -1;


    protected override void InitSingleton()
    {
        //InitMember();
    }

    public void JumpScenario(string label)
    {
        StartCoroutine(JumpScenarioAsync(label, null));
    }

    public void JumpScenario(string label, Action onComplete)
    {
        StartCoroutine(JumpScenarioAsync(label, onComplete));
    }

    IEnumerator JumpScenarioAsync(string label, Action onComplete)
    {
        SceneManager.LoadScene("Gal");
        IsPlaying = true;

        yield return new WaitForSeconds(.1f);
        Debug.Log(GameObject.Find("AdvEngine"));
        AdvEngine AdvEngine = GameObject.Find("AdvEngine").GetComponent<AdvEngine>();
        Debug.Log(AdvEngine);

        AdvEngine.JumpScenario(label);
        while (!AdvEngine.IsEndOrPauseScenario)
        {
            yield return null;
        }
        IsPlaying = false;
        if (onComplete != null) onComplete();
    }


    public void JumpScenario(string label, Action onComplete, Action onFailed)
    {
        JumpScenario(label, null, onComplete, onFailed);
    }


    public void JumpScenario(string label, Action onStart, Action onComplete, Action onFailed)
    {
        SceneManager.LoadScene("Gal");
        AdvEngine AdvEngine = GameObject.Find("AdvEngine").GetComponent<AdvEngine>();
        if (string.IsNullOrEmpty(label))
        {
            if (onFailed != null) onFailed();
            Debug.LogErrorFormat("label为空");
            return;
        }
        if (label[0] == '*')
        {
            label = label.Substring(1);
        }
        if (AdvEngine.DataManager.FindScenarioData(label) == null)
        {
            if (onFailed != null) onFailed();
            Debug.LogErrorFormat("{0}对应数据不存在", label);
            return;
        }

        if (onStart != null) onStart();
        JumpScenario(
            label,
            onComplete);
    }
}
