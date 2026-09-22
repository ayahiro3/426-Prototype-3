using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// HUD text. Requires TextMeshPro (Window > TMP > Import TMP Essential Resources
/// if you haven't already). Any field left unassigned is simply skipped.
/// </summary>
public class ScoreUI : MonoBehaviour
{
    public RhythmGameManager gameManager;
    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text judgementPopupText;
    public TMP_Text countdownText;

    private void OnEnable()
    {
        gameManager.OnJudgement += HandleJudgement;
        gameManager.OnCountdownTick += HandleCountdown;
    }

    private void OnDisable()
    {
        gameManager.OnJudgement -= HandleJudgement;
        gameManager.OnCountdownTick -= HandleCountdown;
    }

    private void Update()
    {
        if (scoreText != null) scoreText.text = $"Score: {gameManager.Score}";
        if (comboText != null) comboText.text = gameManager.Combo > 1 ? $"{gameManager.Combo} COMBO" : "";
    }

    private void HandleJudgement(JudgementResult result, int targetIndex, NoteType type)
    {
        if (judgementPopupText == null) return;
        judgementPopupText.text = result switch
        {
            JudgementResult.Perfect => "PERFECT!",
            JudgementResult.Good => "GOOD",
            _ => "MISS"
        };
        StopAllCoroutines();
        StartCoroutine(FadePopup());
    }

    private IEnumerator FadePopup()
    {
        Color c = judgementPopupText.color;
        c.a = 1f;
        judgementPopupText.color = c;
        yield return new WaitForSeconds(0.35f);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f;
            c.a = 1f - t;
            judgementPopupText.color = c;
            yield return null;
        }
    }

    private void HandleCountdown(float remaining)
    {
        if (countdownText == null) return;
        countdownText.text = remaining > 0f ? Mathf.CeilToInt(remaining).ToString() : "PAINT!";
    }
}