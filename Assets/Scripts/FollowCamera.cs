using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothTime = 0.3f;
    Vector2 _velocity;

    private void FixedUpdate()
    {
        Vector3 followPosition = Vector2.SmoothDamp(transform.position, _target.position, ref _velocity, _smoothTime);
        followPosition.z = -10;
        transform.position = followPosition;
    }
}
