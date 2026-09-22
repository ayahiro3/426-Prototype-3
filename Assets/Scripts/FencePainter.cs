using UnityEngine;

/// <summary>
/// Bridges the abstract note judgement (index + type) to the actual scene
/// objects: picketTargets[] for Tap notes, slatTargets[] for Hold notes.
/// Drag your fence picket/slat GameObjects into these arrays in the order
/// their targetIndex values expect.
/// </summary>
public class FencePainter : MonoBehaviour
{
    public RhythmGameManager gameManager;
    public PaintTarget[] picketTargets;
    public PaintTarget[] slatTargets;

    private void OnEnable()
    {
        if (gameManager != null) gameManager.OnJudgement += HandleJudgement;
    }

    private void OnDisable()
    {
        if (gameManager != null) gameManager.OnJudgement -= HandleJudgement;
    }

    private void HandleJudgement(JudgementResult result, int targetIndex, NoteType type)
    {
        PaintTarget[] pool = type == NoteType.Tap ? picketTargets : slatTargets;
        if (pool != null && targetIndex >= 0 && targetIndex < pool.Length && pool[targetIndex] != null)
        {
            pool[targetIndex].Paint(result);
        }
    }
}