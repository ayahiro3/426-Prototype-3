using UnityEngine;

/// <summary>
/// Purely cosmetic polish: the brush bobs gently up and down while idle, and
/// dips further down whenever Space is held, like it's pressing paint onto
/// the fence. Put this on a UI Image of a paintbrush near the hit line.
/// </summary>
public class Paintbrush : MonoBehaviour
{
    [Header("Idle bob")]
    public float bobHeight = 8f;
    public float bobSpeed = 2f;

    [Header("Press dip")]
    public float dipDistance = 24f;
    public float dipSpeed = 14f;

    private RectTransform rect;
    private Vector2 restPosition;
    private float dipOffset;
    private bool pressed;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        restPosition = rect.anchoredPosition;
    }

    private void Update()
    {
        float bob = Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        float targetDip = pressed ? dipDistance : 0f;
        dipOffset = Mathf.Lerp(dipOffset, targetDip, Time.deltaTime * dipSpeed);

        rect.anchoredPosition = restPosition + new Vector2(0f, bob - dipOffset);
    }

    public void OnPress() => pressed = true;
    public void OnRelease() => pressed = false;
}