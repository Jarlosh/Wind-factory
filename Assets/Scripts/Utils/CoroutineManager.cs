using System;
using System.Collections;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;
    private static CoroutineManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = (new GameObject("CoroutineManager")).AddComponent<CoroutineManager>();
            }
            return _instance;
        }
    }

    public static Coroutine Start(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }

    public static Coroutine Delay(float value, Action action)
    {
        return instance.StartCoroutine(delay());
        IEnumerator delay()
        {
            yield return new WaitForSeconds(value);
            action?.Invoke();
        }
    }

    public static void Start(GameObject target, IEnumerator coroutine)
    {
        var localInstance = target.GetComponent<CoroutineManager>();
        if(localInstance == null)
        {
            localInstance = target.AddComponent<CoroutineManager>();
        }
        localInstance.StartCoroutine(coroutine);
    }

    public static void KillAll(GameObject target)
    {
        var localInstance = target.GetComponent<CoroutineManager>();
        if(localInstance != null)
        {
            localInstance.StopAllCoroutines();
        }
    }

    public static void Kill(Coroutine coroutine)
    {
        instance.StopCoroutine(coroutine);
    }
}
