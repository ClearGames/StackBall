using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;          // 카메라가 추적하는 대상 (플레이어)
    [SerializeField] private Transform lastPlatform;    // 게임 클리어 지점

    private float platformWeight = 4;                   // 게임 클리어 지점이 화면 하단에 보이는 위치까지만 이동하도록 하기 위한 변수

    private void Update()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        // target y 위치와 lastPlatform y 위치를 고려해 카메라의 y 위치 설정
        if (transform.position.y > target.position.y && transform.position.y > lastPlatform.position.y + platformWeight)
        {
            // x, z -> 고정
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }
}
