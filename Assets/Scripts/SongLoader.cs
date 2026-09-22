using UnityEngine;

/// <summary>
/// Beat-matched chart for "prettyjohn1-beat-523170.mp3" (69.93s, ~94 BPM).
///
/// The note times below were pulled directly from the track's actual beat
/// onsets (via audio analysis), not a generic uniform grid, so they should
/// line up with the beat noticeably better than Beatmap.GenerateFromBPM().
/// 79 notes total: 8 pickets and 4 slats, cycled round-robin so the fence
/// fills in evenly across the whole song.
///
/// USAGE:
/// 1. Create your Beatmap asset as usual (Assets > Create > Rhythm > Beatmap).
///    Set songName = "prettyjohn1-beat-523170", bpm = 94, songLength = 69.93.
/// 2. Add this component to any empty GameObject in the scene temporarily
///    (it doesn't need to stay - it's just an editor-time helper).
/// 3. Drag your Beatmap asset onto its "target" field.
/// 4. Right-click the component header and choose "Load PrettyJohn1 Chart".
///    This overwrites target.notes with the chart below.
/// 5. Remove the helper GameObject - the Beatmap asset now has the chart saved on it.
/// </summary>
public class PrettyJohn1ChartLoader : MonoBehaviour
{
    public Beatmap target;

    [ContextMenu("Load PrettyJohn1 Chart")]
    public void LoadChart()
    {
        if (target == null)
        {
            Debug.LogError("Assign a Beatmap asset to 'target' first.");
            return;
        }

        target.songName = "prettyjohn1-beat-523170";
        target.bpm = 94f;
        target.songLength = 69.93f;
        target.notes.Clear();

        var notes = target.notes;
        notes.Add(new NoteData { time = 1.637f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 2.276f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 2.914f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 3.541f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 4.214f, type = NoteType.Hold, holdDuration = 0.765f, targetIndex = 0 });
        notes.Add(new NoteData { time = 5.48f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 6.095f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 6.745f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 7.384f, type = NoteType.Hold, holdDuration = 0.8f, targetIndex = 1 });
        notes.Add(new NoteData { time = 8.649f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 9.311f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 9.915f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 10.588f, type = NoteType.Hold, holdDuration = 0.765f, targetIndex = 2 });
        notes.Add(new NoteData { time = 11.865f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 12.481f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 13.142f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 13.758f, type = NoteType.Hold, holdDuration = 0.812f, targetIndex = 3 });
        notes.Add(new NoteData { time = 15.035f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 15.697f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 16.312f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 16.962f, type = NoteType.Hold, holdDuration = 0.777f, targetIndex = 0 });
        notes.Add(new NoteData { time = 18.239f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 18.866f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 19.528f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 20.143f, type = NoteType.Hold, holdDuration = 0.8f, targetIndex = 1 });
        notes.Add(new NoteData { time = 21.42f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 22.071f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 22.686f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 23.359f, type = NoteType.Hold, holdDuration = 0.765f, targetIndex = 2 });
        notes.Add(new NoteData { time = 24.636f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 25.252f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 25.902f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 26.517f, type = NoteType.Hold, holdDuration = 0.812f, targetIndex = 3 });
        notes.Add(new NoteData { time = 27.806f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 28.456f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 29.083f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 29.745f, type = NoteType.Hold, holdDuration = 0.754f, targetIndex = 0 });
        notes.Add(new NoteData { time = 31.01f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 31.637f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 32.276f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 32.891f, type = NoteType.Hold, holdDuration = 0.812f, targetIndex = 1 });
        notes.Add(new NoteData { time = 34.191f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 34.842f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 35.492f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 36.13f, type = NoteType.Hold, holdDuration = 0.777f, targetIndex = 2 });
        notes.Add(new NoteData { time = 37.396f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 38.034f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 38.673f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 39.311f, type = NoteType.Hold, holdDuration = 0.789f, targetIndex = 3 });
        notes.Add(new NoteData { time = 40.588f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 41.215f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 41.866f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 42.504f, type = NoteType.Hold, holdDuration = 0.789f, targetIndex = 0 });
        notes.Add(new NoteData { time = 43.781f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 44.397f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 45.058f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 45.674f, type = NoteType.Hold, holdDuration = 0.812f, targetIndex = 1 });
        notes.Add(new NoteData { time = 46.951f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 47.601f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 48.228f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 48.878f, type = NoteType.Hold, holdDuration = 0.777f, targetIndex = 2 });
        notes.Add(new NoteData { time = 50.167f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 50.782f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 51.432f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 52.059f, type = NoteType.Hold, holdDuration = 0.8f, targetIndex = 3 });
        notes.Add(new NoteData { time = 53.325f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 53.998f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 54.602f, type = NoteType.Tap, targetIndex = 3 });
        notes.Add(new NoteData { time = 55.275f, type = NoteType.Hold, holdDuration = 0.765f, targetIndex = 0 });
        notes.Add(new NoteData { time = 56.541f, type = NoteType.Tap, targetIndex = 4 });
        notes.Add(new NoteData { time = 57.156f, type = NoteType.Tap, targetIndex = 5 });
        notes.Add(new NoteData { time = 57.818f, type = NoteType.Tap, targetIndex = 6 });
        notes.Add(new NoteData { time = 58.445f, type = NoteType.Hold, holdDuration = 0.8f, targetIndex = 1 });
        notes.Add(new NoteData { time = 59.722f, type = NoteType.Tap, targetIndex = 7 });
        notes.Add(new NoteData { time = 60.383f, type = NoteType.Tap, targetIndex = 0 });
        notes.Add(new NoteData { time = 60.987f, type = NoteType.Tap, targetIndex = 1 });
        notes.Add(new NoteData { time = 61.649f, type = NoteType.Hold, holdDuration = 0.777f, targetIndex = 2 });
        notes.Add(new NoteData { time = 62.914f, type = NoteType.Tap, targetIndex = 2 });
        notes.Add(new NoteData { time = 63.541f, type = NoteType.Tap, targetIndex = 3 });

        Debug.Log($"Loaded {notes.Count} beat-matched notes into '{target.songName}'.");

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(target);
#endif
    }
}