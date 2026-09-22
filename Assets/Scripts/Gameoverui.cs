using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Shows a results panel once the song ends. Hook the Restart() method up to
/// a UI Button's OnClick if you want a replay option.
/// </summary>
public class GameOverUI : MonoBehaviour
{
    public RhythmGameManager gameManager;
    public GameObject panel;
    public TMP_Text finalScoreText;
    public TMP_Text gradeText;
    public TMP_Text breakdownText;

    private void OnEnable()
    {
        gameManager.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        gameManager.OnGameOver -= HandleGameOver;
    }

    private void Start()
    {
        if (panel != null) panel.SetActive(false);
    }

    private void HandleGameOver()
    {
        if (panel != null) panel.SetActive(true);
        if (finalScoreText != null) finalScoreText.text = $"Final Score: {gameManager.Score}";
        if (gradeText != null) gradeText.text = $"Grade: {gameManager.Grade}";
        if (breakdownText != null)
        {
            breakdownText.text =
                $"Perfect: {gameManager.PerfectCount}\n" +
                $"Good: {gameManager.GoodCount}\n" +
                $"Miss: {gameManager.MissCount}\n" +
                $"Max Combo: {gameManager.MaxCombo}";
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}