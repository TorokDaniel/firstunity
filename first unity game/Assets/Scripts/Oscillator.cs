using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Oscillator : MonoBehaviour
{
    public float Range = 90;
    public float Speed = 10;
    public float OffSet;

    private float _timer;

    private void Start()
    {
        _timer = OffSet;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        var cosValue = Mathf.Cos(_timer * Speed / (float) Math.PI); 
        transform.localEulerAngles = new Vector3(cosValue * Range, 0, 0);
    }
}