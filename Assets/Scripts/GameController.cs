using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;

    private void Awake()
    {
        // 현재 스테이지에서 사용하는 플랫폼 생성
        platformSpawner.SpawnPlatforms();
    }
}
