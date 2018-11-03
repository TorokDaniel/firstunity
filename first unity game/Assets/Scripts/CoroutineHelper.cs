using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : SingletonMonoBehaviour<CoroutineHelper>
{
    public delegate void CoroutineCompletionHandler();

    private static Dictionary<string, Coroutine> _runningCoroutines = new Dictionary<string, Coroutine>();
        
    public static Coroutine StartCoroutine(string id, IEnumerator coroutine, CoroutineCompletionHandler completionHandler)
    {
        if (!_runningCoroutines.ContainsKey(id))
        {
            _runningCoroutines[id] = Instance.StartCoroutine(CoroutineWrapper(coroutine, completionHandler));
        }

        return _runningCoroutines[id];
    }
    
    private static IEnumerator CoroutineWrapper(IEnumerator coroutine, CoroutineCompletionHandler completionHandler)
    {
        while (coroutine.MoveNext())
        {
            yield return null;
        }
        
        completionHandler();
    }
}