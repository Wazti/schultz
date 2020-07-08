using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCamFollowPosFunc;
    [SerializeField] private Transform _target;

    private void Start()
    {
        Setup(() => _target.position);
    }

    public void Setup(Func<Vector3> GetCamFollowPosFunc)
    {
        this.GetCamFollowPosFunc = GetCamFollowPosFunc;
    }

    public void SetGetCamFollowPosFunc(Func<Vector3> GetCamFollowPosFunc)
    {
        this.GetCamFollowPosFunc = GetCamFollowPosFunc;
    }

    private void FixedUpdate()
    {

        Vector3 camFollowPos = GetCamFollowPosFunc();
        camFollowPos.z = transform.position.z;

        Vector3 camMoveDir = (camFollowPos - transform.position).normalized;
        float distance = Vector3.Distance(camFollowPos, transform.position);
        float camMoveSpeed = 2f;

        if (distance > 0)
        {
            Vector3 newCamPos = transform.position + camMoveDir * distance * camMoveSpeed * Time.fixedDeltaTime;
            float distanceAfterMove = Vector3.Distance(newCamPos, camFollowPos);
            if (distanceAfterMove > distance)
            {
                //Overshot the target
                newCamPos = camFollowPos;
            }
            transform.position = newCamPos;
        }
        
    }
}
