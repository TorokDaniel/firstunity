using System;
using UnityEngine;

public class AltitudeObjectFollower : MonoBehaviour
{
    public GameObject ObjectToFollow;

    private Transform _selfTransform;
    private Transform _followedTransform;
    
    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _followedTransform = ObjectToFollow.GetComponent<Transform>();
    }
    
    private void Update()
    {
        var selfPosition = _selfTransform.position;
        var followedPosition = _followedTransform.position;

        var relativeAngleToGround = 90 - _selfTransform.eulerAngles.x;
        var relativeAngleToGroundInRad = relativeAngleToGround * (Math.PI/180);
        var distanceToGroundCenter = selfPosition.y * Math.Tan(relativeAngleToGroundInRad);
        var centerPositionedZ = followedPosition.z - (float) distanceToGroundCenter;
        
        _selfTransform.position = new Vector3(followedPosition.x, selfPosition.y, centerPositionedZ);
    }    
}
