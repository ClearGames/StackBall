using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlatformSpawner platformSpawner;
    [SerializeField] private UIController uiController;

    [Header("SFX")]
    [SerializeField] private AudioClip gameOverCliip;
    [SerializeField] private AudioClip gameClearClip;

    [Header("VFX")]
    [SerializeField] GameObject gameOverEffect;
    [SerializeField] GameObject gameClearEffect;

    private RandomColor randomColor;
    private AudioSource audioSource;

    private int brokePlatformCount = 0;     // ���� ������������ �μ��� �÷��� ����
    private int totalPlatformCount;         // ��ü �÷��� ����
    private int currentScore = 0;           // ���� ����

    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

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

    public void GameOver(Vector3 position)
    {
        IsGamePlay = false;

        // SFX, VFX ���� �� ���
        audioSource.clip = gameOverCliip;
        audioSource.Play();
        gameOverEffect.transform.position = position;
        gameOverEffect.SetActive(true);

        // �ְ� ���� ������ ������Ʈ
        UpdateHighScore();
        uiController.GameOver(currentScore);

        // ���콺 ���� ��ư ������ �� �����
        StartCoroutine(nameof(SceneLoadToOnClick));
    }

    public void GameClear()
    {
        IsGamePlay = false;

        // SFX, VFX ���� �� ���
        audioSource.clip = gameClearClip;
        audioSource.Play();
        gameClearEffect.SetActive(true);

        // �ְ� ���� ������ ������Ʈ
        UpdateHighScore();
        uiController.GameClear();
        PlayerPrefs.SetInt("LEVEL", PlayerPrefs.GetInt("LEVEL") + 1);

        // ���콺 ���� ��ư ������ �� �����
        StartCoroutine(nameof(SceneLoadToOnClick));
    }

    private void UpdateHighScore()
    {
        // ���� ������ �ְ� �������� ������ ���� ������ �ְ� ������ ����
        if(currentScore > PlayerPrefs.GetInt("HIGHSCORE"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", currentScore);
        }
    }

    private IEnumerator SceneLoadToOnClick()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
            yield return null;
        }
    }
}
