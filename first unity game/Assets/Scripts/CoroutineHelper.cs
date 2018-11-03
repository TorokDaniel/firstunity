using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : SingletonMonoBehaviour<CoroutineHelper>
{
    
    public delegate void CoroutineCompletionHandler();

    private static readonly Dictionary<string, Coroutine> RunningCoroutines = new Dictionary<string, Coroutine>();
        
    public static Coroutine StartCoroutine(string id, IEnumerator coroutine, CoroutineCompletionHandler completionHandler)
    {
        CoroutineCompletionHandler wrappedCompletionHandler = () =>
        {
            RunningCoroutines.Remove(id);
            completionHandler();
        };
        
        if (!RunningCoroutines.ContainsKey(id))
        {
            RunningCoroutines[id] = Instance.StartCoroutine(CoroutineWrapper(coroutine, wrappedCompletionHandler));
        }

        return RunningCoroutines[id];
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