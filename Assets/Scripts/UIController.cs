using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI nextLevel;

    [Header("Main")]
    [SerializeField] private GameObject mainPanel;

    [Header("InGame")]
    [SerializeField] private Image levelProgressBar;
    [SerializeField] private TextMeshProUGUI currentScore;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI textCurrentScore;
    [SerializeField] private TextMeshProUGUI textHighScore;

    [Header("GameClear")]
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private TextMeshProUGUI textLevelCompleted;

    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("LEVEL") + 1).ToString();
        nextLevel.text = (PlayerPrefs.GetInt("LEVEL") + 2).ToString();

        // 게임 처음 시작      -> Main UI On
        // 스테이지 클리어     -> Main UI Off
        if(PlayerPrefs.GetInt("DEACTIVATEMAIN") == 0)   mainPanel.SetActive(true);
        else                                            mainPanel.SetActive(false);     
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
    }

    public void GameOver(int currentScore)
    {
        textCurrentScore.text = $"SCORE\n{currentScore}";
        textHighScore.text = $"HIGH SCORE\n{PlayerPrefs.GetInt("HIGHSCORE")}";

        gameOverPanel.SetActive(true);

        // GameOver -> Main UI 보이도록 설정
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 0);
    }

    public void GameClear()
    {
        textLevelCompleted.text = $"LEVEL {PlayerPrefs.GetInt("LEVEL")+1}\nCOMPLETED!";
        gameClearPanel.SetActive(true);
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 1);
    }

    /// <summary>
    /// 프로그램이 종료될 때는 DEACTIVATEMAIN을 0으로 설정해
    /// 다시 게임을 시작할 때는 Main UI가 보이도록 한다
    /// </summary>
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 0);
    }

    public float LevelProgressBar { set => levelProgressBar.fillAmount = value; }
    public float CurrentScore { set => currentScore.text = value.ToString(); }
}
