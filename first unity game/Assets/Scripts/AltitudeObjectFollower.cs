using System;
using UnityEngine;

public class AltitudeObjectFollower : MonoBehaviour
{
    public GameObject ObjectToFollow;
    
    [Range(0.0F, 1.0F)]
    public float FollowRate = 0.025F;

    private Transform _selfTransform;
    private Transform _followedTransform;
    private float _deltaY;
    
    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _followedTransform = ObjectToFollow.GetComponent<Transform>();
        _deltaY = _selfTransform.position.y - _followedTransform.position.y;
    }
    
    private void Update()
    {
        var selfPosition = _selfTransform.position;
        var followedPosition = _followedTransform.position;

        var relativeAngleToGround = 90 - _selfTransform.eulerAngles.x;
        var relativeAngleToGroundInRad = relativeAngleToGround * (Math.PI/180);
        var distanceToGroundCenter = selfPosition.y * Math.Tan(relativeAngleToGroundInRad);
        var centerPositionedZ = followedPosition.z - (float) distanceToGroundCenter;

        var newX = selfPosition.x + (followedPosition.x - selfPosition.x) * FollowRate;
        var newZ = selfPosition.z + (centerPositionedZ - selfPosition.z) * FollowRate;
        var newY = selfPosition.y + (followedPosition.y + _deltaY - selfPosition.y) * FollowRate;
        
        _selfTransform.position = new Vector3(newX, newY, newZ);
    }    
}
