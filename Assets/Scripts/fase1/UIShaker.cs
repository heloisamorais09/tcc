using UnityEngine;

public class UIShaker : MonoBehaviour
{
    [Header("Alvo (RectTransform)")]
    public RectTransform target;   // se vazio, usa o próprio
    [Header("Intensidade")]
    public float amplitude = 4f;   // pixels (3–6 confortável)
    public float frequency = 1.1f; // Hz (0.8–1.2 confortável)
    public bool useUnscaledTime = true;
    public bool playOnEnable = false;

    Vector2 basePos;
    float seedX, seedY;
    bool shaking;

    void Reset() { target = GetComponent<RectTransform>(); }
    void Awake()
    {
        if (!target) target = GetComponent<RectTransform>();
        if (target) basePos = target.anchoredPosition;
        seedX = Random.value * 10f;
        seedY = Random.value * 10f;
    }
    void OnEnable()
    {
        if (target) basePos = target.anchoredPosition;
        if (playOnEnable) StartShake();
        else Apply(0, 0);
    }
    void OnDisable() { if (target) target.anchoredPosition = basePos; shaking = false; }

    void Update()
    {
        if (!shaking || !target) return;
        float t = (useUnscaledTime ? Time.unscaledTime : Time.time) * frequency;
        // Perlin = movimento “orgânico”
        float dx = (Mathf.PerlinNoise(seedX, t) - 0.5f) * 2f * amplitude;
        float dy = (Mathf.PerlinNoise(seedY, t) - 0.5f) * 2f * amplitude * 0.5f; // menos vertical p/ leitura
        Apply(dx, dy);
    }

    void Apply(float dx, float dy) { target.anchoredPosition = basePos + new Vector2(dx, dy); }

    public void StartShake(float amp = -1f, float freq = -1f)
    {
        if (amp > 0) amplitude = amp;
        if (freq > 0) frequency = freq;
        if (target) basePos = target.anchoredPosition;
        shaking = true;
    }
    public void StopAndReset()
    {
        shaking = false;
        if (target) target.anchoredPosition = basePos;
    }
}

