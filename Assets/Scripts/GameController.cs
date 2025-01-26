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

    private int brokePlatformCount = 0;     // 현재 스테이지에서 부서진 플랫폼 개수
    private int totalPlatformCount;         // 전체 플랫폼 개수
    private int currentScore = 0;           // 현재 점수

    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

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

    public void GameOver(Vector3 position)
    {
        IsGamePlay = false;

        // SFX, VFX 설정 및 재생
        audioSource.clip = gameOverCliip;
        audioSource.Play();
        gameOverEffect.transform.position = position;
        gameOverEffect.SetActive(true);

        // 최고 점수 데이터 업데이트
        UpdateHighScore();
        uiController.GameOver(currentScore);

        // 마우스 왼쪽 버튼 누르면 씬 재시작
        StartCoroutine(nameof(SceneLoadToOnClick));
    }

    public void GameClear()
    {
        IsGamePlay = false;

        // SFX, VFX 설정 및 재생
        audioSource.clip = gameClearClip;
        audioSource.Play();
        gameClearEffect.SetActive(true);

        // 최고 점수 데이터 업데이트
        UpdateHighScore();
        uiController.GameClear();
        PlayerPrefs.SetInt("LEVEL", PlayerPrefs.GetInt("LEVEL") + 1);

        // 마우스 왼쪽 버튼 누르면 씬 재시작
        StartCoroutine(nameof(SceneLoadToOnClick));
    }

    private void UpdateHighScore()
    {
        // 현재 점수가 최고 점수보다 높으면 현재 점수를 최고 점수로 설정
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
