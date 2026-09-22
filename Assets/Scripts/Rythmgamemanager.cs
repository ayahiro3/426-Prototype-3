using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Countdown, Playing, GameOver }

/// <summary>
/// The brain of the game. Drives song timing off the audio DSP clock (not
/// Time.time, which drifts), tracks score/combo, and broadcasts events that
/// the UI and fence-painting scripts subscribe to.
/// </summary>
public class RhythmGameManager : MonoBehaviour
{
    public static RhythmGameManager Instance { get; private set; }

    [Header("Song & Chart")]
    public AudioSource musicSource;   // assign a clip on this in the Inspector
    public Beatmap beatmap;
    public float countdownSeconds = 3f;

    [Header("Timing Windows (seconds)")]
    public float perfectWindow = 0.06f;
    public float goodWindow = 0.15f;

    [Header("Scoring")]
    public int perfectScore = 100;
    public int goodScore = 50;

    public GameState State { get; private set; } = GameState.Countdown;
    public float SongTime { get; private set; }

    public int Score { get; private set; }
    public int Combo { get; private set; }
    public int MaxCombo { get; private set; }
    public int PerfectCount { get; private set; }
    public int GoodCount { get; private set; }
    public int MissCount { get; private set; }

    // (result, targetIndex, noteType) — subscribers use targetIndex+type to
    // find which fence piece to paint.
    public event Action<JudgementResult, int, NoteType> OnJudgement;
    public event Action OnGameOver;
    public event Action<float> OnCountdownTick; // seconds remaining, 0 = "go"

    private float dspSongStartTime;
    private bool songStarted;
    private readonly List<NoteController> activeNotes = new List<NoteController>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        float remaining = countdownSeconds;
        while (remaining > 0f)
        {
            OnCountdownTick?.Invoke(remaining);
            yield return new WaitForSeconds(1f);
            remaining -= 1f;
        }
        OnCountdownTick?.Invoke(0f);
        BeginSong();
    }

    private void BeginSong()
    {
        State = GameState.Playing;
        // Schedule slightly in the future so the very first frames of Update
        // never see a negative SongTime.
        dspSongStartTime = (float)AudioSettings.dspTime + 0.1f;
        musicSource.PlayScheduled(dspSongStartTime);
        songStarted = true;
    }

    private void Update()
    {
        if (State != GameState.Playing || !songStarted) return;

        SongTime = (float)(AudioSettings.dspTime - dspSongStartTime);

        if (SongTime > 0.5f && !musicSource.isPlaying)
        {
            EndGame();
        }
    }

    public void RegisterActiveNote(NoteController note) => activeNotes.Add(note);
    public void UnregisterActiveNote(NoteController note) => activeNotes.Remove(note);

    /// <summary>The single button always judges against whichever unresolved note is due soonest.</summary>
    public NoteController GetEarliestActiveNote()
    {
        NoteController earliest = null;
        foreach (var n in activeNotes)
        {
            if (n.Resolved) continue;
            if (earliest == null || n.Data.time < earliest.Data.time) earliest = n;
        }
        return earliest;
    }

    public void RegisterJudgement(JudgementResult result, int targetIndex, NoteType type)
    {
        switch (result)
        {
            case JudgementResult.Perfect:
                Score += perfectScore;
                Combo++;
                PerfectCount++;
                break;
            case JudgementResult.Good:
                Score += goodScore;
                Combo++;
                GoodCount++;
                break;
            case JudgementResult.Miss:
                Combo = 0;
                MissCount++;
                break;
        }
        MaxCombo = Mathf.Max(MaxCombo, Combo);
        OnJudgement?.Invoke(result, targetIndex, type);
    }

    public void EndGame()
    {
        if (State == GameState.GameOver) return;
        State = GameState.GameOver;
        musicSource.Stop();
        OnGameOver?.Invoke();
    }

    public float Accuracy
    {
        get
        {
            int total = PerfectCount + GoodCount + MissCount;
            if (total == 0) return 0f;
            float weighted = PerfectCount * 1f + GoodCount * 0.5f;
            return weighted / total;
        }
    }

    public string Grade
    {
        get
        {
            float acc = Accuracy;
            if (acc >= 0.95f) return "S";
            if (acc >= 0.85f) return "A";
            if (acc >= 0.70f) return "B";
            if (acc >= 0.50f) return "C";
            return "D";
        }
    }
}