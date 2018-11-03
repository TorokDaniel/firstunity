using System.Collections;
using UnityEngine;

public class CoroutineHelper : SingletonMonoBehaviour<CoroutineHelper>
{
    
    public delegate void CoroutineCompletionHandler();
   
    public static Coroutine StartCoroutine(IEnumerator coroutine, CoroutineCompletionHandler completionHandler)
    {
        return Instance.StartCoroutine(CoroutineWrapper(coroutine, completionHandler));
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