using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;

    private void Awake()
    {
        // ���� ������������ ����ϴ� �÷��� ����
        platformSpawner.SpawnPlatforms();
    }
}
