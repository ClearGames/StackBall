using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    private RandomColor randomColor;

    private void Awake()
    {
        // ���� ������������ ����ϴ� �÷��� ����
        platformSpawner.SpawnPlatforms();

        // ���� �ε��� ������ ���� ����
        // Pole, Platform, Player, UI(CurruentLevel, NextLevel, ProgressBar)
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }
}
