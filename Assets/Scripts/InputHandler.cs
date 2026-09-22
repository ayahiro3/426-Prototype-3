using UnityEngine;

/// <summary>
/// One-button input. On key-down, judges whichever active note is due
/// soonest. On key-up, releases any hold note currently in progress.
/// </summary>
public class InputHandler : MonoBehaviour
{
    public RhythmGameManager gameManager;
    public Paintbrush paintbrush; // optional, for the up/down brush polish
    public KeyCode key = KeyCode.Space;

    private NoteController heldNote;

    private void Update()
    {
        if (gameManager.State != GameState.Playing) return;

        if (Input.GetKeyDown(key))
        {
            NoteController target = gameManager.GetEarliestActiveNote();
            if (target != null)
            {
                JudgementResult result = target.JudgeTap();
                if (target.Data.type == NoteType.Hold && result != JudgementResult.Miss)
                {
                    heldNote = target;
                }
            }
            if (paintbrush != null) paintbrush.OnPress();
        }

        if (Input.GetKeyUp(key))
        {
            if (heldNote != null)
            {
                heldNote.ReleaseHold();
                heldNote = null;
            }
            if (paintbrush != null) paintbrush.OnRelease();
        }
    }
}