using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;          // ī�޶� �����ϴ� ��� (�÷��̾�)
    [SerializeField] private Transform lastPlatform;    // ���� Ŭ���� ����

    private float platformWeight = 4;                   // ���� Ŭ���� ������ ȭ�� �ϴܿ� ���̴� ��ġ������ �̵��ϵ��� �ϱ� ���� ����

    private void Update()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        // target y ��ġ�� lastPlatform y ��ġ�� ����� ī�޶��� y ��ġ ����
        if (transform.position.y > target.position.y && transform.position.y > lastPlatform.position.y + platformWeight)
        {
            // x, z -> ����
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }
    }
}
