using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private UIController uiController;

    private RandomColor randomColor;

    private int brokePlatformCount = 0;     // ���� ������������ �μ��� �÷��� ����
    private int totalPlatformCount;         // ��ü �÷��� ����
    private int currentScore = 0;           // ���� ����

    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        // ���� ������������ ����ϴ� �÷��� ����
        totalPlatformCount = platformSpawner.SpawnPlatforms();

        // ���� �ε��� ������ ���� ����
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
