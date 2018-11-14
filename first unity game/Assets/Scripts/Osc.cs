using System;
using UnityEngine;

public class Osc : MonoBehaviour
{
    private delegate void OscSetter(Vector3 vector);

    [Serializable]
    public class Option
    {
        public bool Looped;
        public bool EaseInOut;
    }
    
    [Serializable]
    public class Type
    {
        public bool Position;
        public bool Rotation;
        public bool Scale;
    }
    
    [Serializable]
    public class Path
    {
        public Vector3 Delta;
        public float TransitionTime;
    }

    public Path[] Paths;
    private Path _currentPath;
    private int _currentPathIndex;
    
    public Option Options;
    public Type Types;
    
    private Vector3 _startVector;
    private Vector3 _currentVector;

    private AnimationCurve _animationCurveX;
    private AnimationCurve _animationCurveY;
    private AnimationCurve _animationCurveZ;
    
    private float _currentTime = 0f;
    private OscSetter _oscSetter;

    private void Start()
    {
        _currentPathIndex = 0;
        _currentPath = Paths[_currentPathIndex];
        
        if (Types.Position)
        {
            _startVector = transform.localPosition;
            _oscSetter = vector => transform.localPosition = vector;
        } else if (Types.Rotation)
        {
            _startVector = transform.localEulerAngles;
            _oscSetter = vector => transform.localEulerAngles = vector;
        }
        else if (Types.Scale)
        {
            _startVector = transform.localScale;
            _oscSetter = vector => transform.localScale = vector;
        }

        _currentVector = _startVector;
        SetAnimationCurves();
    }

    private void Update()
    {
        if (_currentTime >= _currentPath.TransitionTime)
        {
            if (_currentPathIndex == Paths.Length - 1)
            {
                if (!Options.Looped)
                {
                    return;
                }
                
                _currentPathIndex = 0;
            }
            else
            {
                _currentPathIndex += 1;
            }

            _currentVector += _currentPath.Delta;
            _currentPath = Paths[_currentPathIndex];
            _currentTime = 0;
            SetAnimationCurves();
        }
        
        _currentTime += Time.deltaTime;
        
        var newVector = new Vector3(
            _animationCurveX.Evaluate(_currentTime),
            _animationCurveY.Evaluate(_currentTime),
            _animationCurveZ.Evaluate(_currentTime)
        );
        
        _oscSetter(newVector);

    }

    private void SetAnimationCurves()
    {
        if (Options.EaseInOut)
        {
            _animationCurveX = AnimationCurve.EaseInOut(0, _currentVector.x, _currentPath.TransitionTime, _currentVector.x + _currentPath.Delta.x);
            _animationCurveY = AnimationCurve.EaseInOut(0, _currentVector.y, _currentPath.TransitionTime, _currentVector.y + _currentPath.Delta.y);
            _animationCurveZ = AnimationCurve.EaseInOut(0, _currentVector.z, _currentPath.TransitionTime, _currentVector.z + _currentPath.Delta.z);            
        }
        else
        {
            _animationCurveX = AnimationCurve.Linear(0, _currentVector.x, _currentPath.TransitionTime, _currentVector.x + _currentPath.Delta.x);
            _animationCurveY = AnimationCurve.Linear(0, _currentVector.y, _currentPath.TransitionTime, _currentVector.y + _currentPath.Delta.y);
            _animationCurveZ = AnimationCurve.Linear(0, _currentVector.z, _currentPath.TransitionTime, _currentVector.z + _currentPath.Delta.z);
        }   
    }
    
}