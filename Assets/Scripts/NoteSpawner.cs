using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Watches the song clock and instantiates note prefabs far enough ahead of
/// their hit time that they have time to travel from spawnPoint to hitLine.
/// </summary>
public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    public RhythmGameManager gameManager;
    public GameObject tapNotePrefab;   // needs a NoteController + Image
    public GameObject holdNotePrefab;  // needs a NoteController + Image + holdBar child
    public RectTransform spawnPoint;
    public RectTransform hitLine;

    [Header("Timing")]
    [Tooltip("Seconds a note travels before it's due — bigger = more reaction time, but a slower-feeling lane.")]
    public float leadTime = 1.5f;

    private List<NoteData> pending;
    private int nextIndex;

    private void Start()
    {
        pending = new List<NoteData>(gameManager.beatmap.notes);
        pending.Sort((a, b) => a.time.CompareTo(b.time));
        nextIndex = 0;
    }

    private void Update()
    {
        if (gameManager.State != GameState.Playing) return;

        while (nextIndex < pending.Count && pending[nextIndex].time - gameManager.SongTime <= leadTime)
        {
            SpawnNote(pending[nextIndex]);
            nextIndex++;
        }
    }

    private void SpawnNote(NoteData data)
    {
        GameObject prefab = data.type == NoteType.Tap ? tapNotePrefab : holdNotePrefab;
        GameObject obj = Instantiate(prefab, spawnPoint.parent);
        NoteController controller = obj.GetComponent<NoteController>();
        controller.Initialize(data, gameManager, spawnPoint, hitLine, leadTime);
    }
}