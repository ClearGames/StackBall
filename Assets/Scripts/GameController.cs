using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    private RandomColor randomColor;

    private void Awake()
    {
        // 현재 스테이지에서 사용하는 플랫폼 생성
        platformSpawner.SpawnPlatforms();

        // 씬을 로드할 때마다 색상 변경
        // Pole, Platform, Player, UI(CurruentLevel, NextLevel, ProgressBar)
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }
}
