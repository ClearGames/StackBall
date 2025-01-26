using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private UIController uiController;

    private RandomColor randomColor;

    private int brokePlatformCount = 0;     // 현재 스테이지에서 부서진 플랫폼 개수
    private int totalPlatformCount;         // 전체 플랫폼 개수
    private int currentScore = 0;           // 현재 점수

    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        // 현재 스테이지에서 사용하는 플랫폼 생성
        totalPlatformCount = platformSpawner.SpawnPlatforms();

        // 씬을 로드할 때마다 색상 변경
        // Pole, Platform, Player, UI(CurruentLevel, NextLevel, ProgressBar)
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();
                yield break;
            }
            yield return null;
        }
    }

    private void GameStart()
    {
        IsGamePlay = true;
        uiController.GameStart();
    }

    public void OnCollisionWithPlatform(int addedScore = 1)
    {
        brokePlatformCount++;
        uiController.LevelProgressBar = (float)brokePlatformCount / (float)totalPlatformCount;

        currentScore += addedScore;
        uiController.CurrentScore = currentScore;
    }
}
