using UnityEngine;

/// <summary>
/// Lives on the Tap/Hold note prefab. Moves the note from the spawn point to
/// the hit line so it arrives exactly on its scheduled time, and resolves
/// itself into Perfect/Good/Miss either from player input (via InputHandler
/// calling JudgeTap/ReleaseHold) or automatically if nobody ever presses it.
/// </summary>
public class NoteController : MonoBehaviour
{
    public NoteData Data { get; private set; }
    public bool Resolved { get; private set; }
    public bool IsBeingHeld { get; private set; }

    [Header("Hold visuals (only needed on the Hold prefab)")]
    [Tooltip("A child RectTransform stretched to represent how long the button must be held.")]
    public RectTransform holdBar;

    private RhythmGameManager gameManager;
    private RectTransform rect;
    private RectTransform spawnPoint;
    private RectTransform hitLine;
    private float leadTime;
    private float startSongTime;

    private bool holdStartJudged;
    private JudgementResult holdStartResult;

    public void Initialize(NoteData data, RhythmGameManager manager, RectTransform spawn, RectTransform hit, float lead)
    {
        Data = data;
        gameManager = manager;
        spawnPoint = spawn;
        hitLine = hit;
        leadTime = lead;
        startSongTime = data.time - lead;
        rect = GetComponent<RectTransform>();

        if (Data.type == NoteType.Hold && holdBar != null)
        {
            float pixelsPerSecond = Vector2.Distance(spawnPoint.anchoredPosition, hitLine.anchoredPosition) / leadTime;
            Vector2 size = holdBar.sizeDelta;
            size.y = Data.holdDuration * pixelsPerSecond;
            holdBar.sizeDelta = size;
        }

        gameManager.RegisterActiveNote(this);
    }

    private void Update()
    {
        if (gameManager.State != GameState.Playing || Resolved) return;

        float t = gameManager.SongTime;
        float fraction = Mathf.InverseLerp(startSongTime, Data.time, t);
        rect.anchoredPosition = Vector2.LerpUnclamped(spawnPoint.anchoredPosition, hitLine.anchoredPosition, fraction);

        if (Data.type == NoteType.Tap)
        {
            // Nobody ever pressed it in time.
            if (t > Data.time + gameManager.goodWindow)
            {
                Resolve(JudgementResult.Miss);
            }
            return;
        }

        // Hold note handling below.
        if (!holdStartJudged)
        {
            // The press never came at all.
            if (t > Data.time + gameManager.goodWindow)
            {
                Resolve(JudgementResult.Miss);
            }
            return;
        }

        if (IsBeingHeld && t >= Data.time + Data.holdDuration)
        {
            // Held all the way through (or is still holding past the end) — success.
            float releaseDelta = Mathf.Abs(t - (Data.time + Data.holdDuration));
            JudgementResult releaseResult = releaseDelta <= gameManager.perfectWindow ? JudgementResult.Perfect : JudgementResult.Good;
            JudgementResult finalResult = (holdStartResult == JudgementResult.Perfect && releaseResult == JudgementResult.Perfect)
                ? JudgementResult.Perfect
                : JudgementResult.Good;
            Resolve(finalResult);
        }
    }

    /// <summary>Called by InputHandler on key-down when this is the earliest active note.</summary>
    public JudgementResult JudgeTap()
    {
        float delta = Mathf.Abs(gameManager.SongTime - Data.time);
        JudgementResult result = delta <= gameManager.perfectWindow ? JudgementResult.Perfect
                                 : delta <= gameManager.goodWindow ? JudgementResult.Good
                                 : JudgementResult.Miss;

        if (Data.type == NoteType.Tap)
        {
            Resolve(result);
        }
        else
        {
            holdStartJudged = true;
            holdStartResult = result;
            IsBeingHeld = result != JudgementResult.Miss;
            if (result == JudgementResult.Miss)
            {
                Resolve(JudgementResult.Miss); // pressed way too early/late — dropped note
            }
        }
        return result;
    }

    /// <summary>Called by InputHandler on key-up.</summary>
    public void ReleaseHold()
    {
        if (Resolved || !holdStartJudged) return;
        IsBeingHeld = false;

        float requiredEnd = Data.time + Data.holdDuration;
        if (gameManager.SongTime < requiredEnd - gameManager.goodWindow)
        {
            // Let go far too early.
            Resolve(JudgementResult.Miss);
        }
        // Otherwise: released at/after a valid time — Update() will already have
        // resolved it, or will on the next frame since IsBeingHeld flips false
        // only after this check, so a release right at the end still counts.
        else if (gameManager.SongTime >= requiredEnd)
        {
            float releaseDelta = Mathf.Abs(gameManager.SongTime - requiredEnd);
            JudgementResult releaseResult = releaseDelta <= gameManager.perfectWindow ? JudgementResult.Perfect : JudgementResult.Good;
            JudgementResult finalResult = (holdStartResult == JudgementResult.Perfect && releaseResult == JudgementResult.Perfect)
                ? JudgementResult.Perfect
                : JudgementResult.Good;
            Resolve(finalResult);
        }
    }

    private void Resolve(JudgementResult result)
    {
        if (Resolved) return;
        Resolved = true;
        gameManager.RegisterJudgement(result, Data.targetIndex, Data.type);
        gameManager.UnregisterActiveNote(this);
        Destroy(gameObject, 0.15f);
    }
}