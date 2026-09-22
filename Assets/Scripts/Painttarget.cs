using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach to each fence picket / slat GameObject. Expects two Image
/// components using the SAME wood-plank sprite (fence_part / fence_part_horizontal):
///   - baseImage: the always-visible wood art, tinted white, sitting underneath.
///   - paintOverlay: a child Image with the identical sprite and RectTransform,
///     transparent by default. Painting fades this overlay's alpha/color in,
///     so the paint naturally clips to the plank's silhouette (since the PNG's
///     background is already transparent) instead of covering a plain rectangle.
///
/// Call Paint(result) to react to a judgement: Perfect gives a clean solid
/// pastel fill, Good gives a slightly uneven fill plus splatter dots, Miss
/// leaves it bare with a brief red flash on the overlay.
/// </summary>
public class PaintTarget : MonoBehaviour
{
    [Header("Sprite layers")]
    [Tooltip("The always-visible wood art underneath (fence_part or fence_part_horizontal).")]
    public Image baseImage;
    [Tooltip("Child Image using the SAME sprite as baseImage, alpha 0 by default. This is what gets tinted/faded in when painted.")]
    public Image paintOverlay;

    [Header("Pastel palette — one is picked at random when this piece is painted")]
    public Color[] pastelColors = new Color[]
    {
        new Color(0.98f, 0.80f, 0.82f), // pastel pink
        new Color(0.80f, 0.90f, 0.98f), // pastel blue
        new Color(0.85f, 0.98f, 0.80f), // pastel mint
        new Color(0.98f, 0.93f, 0.75f), // pastel butter
        new Color(0.88f, 0.80f, 0.98f), // pastel lavender
    };

    [Header("Perfect vs Good fill strength")]
    [Range(0f, 1f)] public float perfectAlpha = 0.95f; // near-total coverage, clean paint job
    [Range(0f, 1f)] public float goodAlpha = 0.6f;      // patchier, wood grain still shows through

    [Header("Splatter (Good result)")]
    [Tooltip("A small circle Image prefab used to build up a splatter pattern. Unity's built-in UI 'Knob' sprite works fine as a placeholder.")]
    public GameObject splatterDotPrefab;
    public int splatterDotCount = 14;

    private bool painted;

    private void Awake()
    {
        if (baseImage == null) baseImage = GetComponent<Image>();
        baseImage.color = Color.white; // let the wood art show its real colors

        if (paintOverlay != null)
        {
            Color c = paintOverlay.color;
            c.a = 0f;
            paintOverlay.color = c;
        }
    }

    public void Paint(JudgementResult result)
    {
        if (painted) return;

        if (result == JudgementResult.Miss)
        {
            StartCoroutine(FlashMiss());
            return;
        }

        painted = true;
        Color chosen = pastelColors[Random.Range(0, pastelColors.Length)];

        if (result == JudgementResult.Perfect)
        {
            chosen.a = perfectAlpha;
            StartCoroutine(FillOverlay(chosen));
        }
        else // Good — a patchier finish plus splatter dots
        {
            chosen.a = goodAlpha;
            StartCoroutine(FillOverlay(chosen));
            SpawnSplatter(chosen);
        }
    }

    private IEnumerator FillOverlay(Color target)
    {
        if (paintOverlay == null) yield break;
        float t = 0f;
        Color start = paintOverlay.color;
        while (t < 1f)
        {
            t += Time.deltaTime * 4f;
            paintOverlay.color = Color.Lerp(start, target, t);
            yield return null;
        }
    }

    private void SpawnSplatter(Color color)
    {
        if (splatterDotPrefab == null || paintOverlay == null) return;
        RectTransform rect = paintOverlay.GetComponent<RectTransform>();

        for (int i = 0; i < splatterDotCount; i++)
        {
            GameObject dot = Instantiate(splatterDotPrefab, paintOverlay.transform);
            RectTransform dotRect = dot.GetComponent<RectTransform>();

            float x = Random.Range(-rect.rect.width, rect.rect.width) * 0.5f;
            float y = Random.Range(-rect.rect.height, rect.rect.height) * 0.5f;
            dotRect.anchoredPosition = new Vector2(x, y);
            dotRect.localScale = Vector3.one * Random.Range(0.15f, 0.5f);

            Image dotImage = dot.GetComponent<Image>();
            if (dotImage != null)
            {
                Color c = color;
                c.a = Random.Range(0.6f, 1f);
                dotImage.color = c;
            }
        }
    }

    private IEnumerator FlashMiss()
    {
        if (paintOverlay == null) yield break;
        Color original = paintOverlay.color;
        Color flash = new Color(0.8f, 0.2f, 0.2f, 0.5f);
        paintOverlay.color = flash;
        yield return new WaitForSeconds(0.15f);
        paintOverlay.color = original; // fades back to transparent — stays bare
    }
}