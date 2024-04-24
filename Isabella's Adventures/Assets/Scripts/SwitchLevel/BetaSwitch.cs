using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class BetaSwitch : MonoBehaviour
{
    public float DelayBeforeLoading;
    
    public Slider Slider;
    public Canvas Canvas;
    
    public void StartLoad(int Index)
    {
        Canvas.enabled = true;
        StartCoroutine(Loading(Index));
    }
    
    public void StartLoad(string Name)
    {
        Canvas.enabled = true;
        StartCoroutine(Loading(Name));
    }
    

    private IEnumerator Loading(int Index)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(Index);
        _operation.allowSceneActivation = false;

        while (!_operation.isDone)
        {
            Slider.value = _operation.progress;
            if (_operation.progress >= .9f && !_operation.allowSceneActivation)
            {
                yield return new WaitForSeconds(DelayBeforeLoading);
                _operation.allowSceneActivation = true;
                print(_operation.progress);
            }
            yield return null;
        }
        print("DoNE");
    }
    
    private IEnumerator Loading(string Name)
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync(Name);
        _operation.allowSceneActivation = false;

        while (!_operation.isDone)
        {
            Slider.value = _operation.progress;
            if (_operation.progress >= .9f && !_operation.allowSceneActivation)
            {
                yield return new WaitForSeconds(DelayBeforeLoading);
            }
        }
    }
}