using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A chart for one song. Create one via Assets > Create > Rhythm > Beatmap.
/// You can hand-author the notes list, or right-click the component header
/// in the Inspector and choose "Generate Notes From BPM" for a quick
/// playable placeholder chart locked to your song's tempo.
/// </summary>
[CreateAssetMenu(fileName = "NewBeatmap", menuName = "Rhythm/Beatmap")]
public class Beatmap : ScriptableObject
{
    [Header("Song info")]
    public string songName;
    public float bpm = 90f;
    [Tooltip("Length of the song in seconds. Only used by the auto-generator below.")]
    public float songLength = 60f;

    [Header("Chart")]
    public List<NoteData> notes = new List<NoteData>();

    [Header("Auto-generate helper (optional)")]
    [Tooltip("How many pickets exist in your scene's FencePainter.picketTargets array.")]
    public int picketCount = 8;
    [Tooltip("How many slats exist in your scene's FencePainter.slatTargets array.")]
    public int slatCount = 4;

    // Right-click the component header in the Inspector (or the gear icon)
    // and choose "Generate Notes From BPM" to auto-fill the notes list with
    // a simple alternating tap/hold pattern locked to the BPM above.
    [ContextMenu("Generate Notes From BPM")]
    public void GenerateFromBPM()
    {
        notes.Clear();
        float beatLength = 60f / Mathf.Max(bpm, 1f);
        float t = beatLength * 2f; // small lead-in before the first note
        int picketIndex = 0;
        int slatIndex = 0;
        bool placeHold = false;

        while (t < songLength - beatLength)
        {
            if (placeHold && slatIndex < slatCount)
            {
                notes.Add(new NoteData
                {
                    time = t,
                    type = NoteType.Hold,
                    holdDuration = beatLength * 1.5f,
                    targetIndex = slatIndex
                });
                slatIndex++;
                t += beatLength * 3f; // hold notes take up more room before the next note
            }
            else
            {
                notes.Add(new NoteData
                {
                    time = t,
                    type = NoteType.Tap,
                    targetIndex = picketIndex % Mathf.Max(picketCount, 1)
                });
                picketIndex++;
                t += beatLength;
            }

            // Every 4th picket, try to slot in a hold note next.
            placeHold = (picketIndex % 4 == 0) && !placeHold;
        }

        Debug.Log($"Generated {notes.Count} notes for '{songName}' at {bpm} BPM.");
    }
}