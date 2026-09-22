using System;
using UnityEngine;

public enum NoteType { Tap, Hold }
public enum JudgementResult { Perfect, Good, Miss }

/// <summary>
/// One entry in a Beatmap: when it happens, what kind of press it needs,
/// and which fence piece (picket or slat) it paints when hit.
/// </summary>
[Serializable]
public class NoteData
{
    [Tooltip("Time in seconds from the start of the song when this note should be hit.")]
    public float time;

    public NoteType type = NoteType.Tap;

    [Tooltip("Only used for Hold notes - how long the button must be held down, in seconds.")]
    public float holdDuration = 0.3f;

    [Tooltip("Index into FencePainter's picketTargets[] (for Tap notes) or slatTargets[] (for Hold notes).")]
    public int targetIndex;
}