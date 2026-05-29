using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AlgaeWave : MonoBehaviour
{
    [Header("Estrutura")]
    public int segments = 6;
    public float height = 3f;

    [Header("Movimento")]
    public float waveAmplitude = 0.3f;
    public float waveFrequency = 2f;
    public float waveSpeed = 2f;

    [Header("Crescimento")]
    public float growDuration = 1f;

    private LineRenderer line;
    private float randomOffset;

    private float baseHeight;
    private float baseWaveAmplitude;
    private float baseWaveSpeed;

    private float growPercent = 0f;

    void Awake()
    {
        line = GetComponent<LineRenderer>();

        line.positionCount = segments;
        line.useWorldSpace = false;

        randomOffset = Random.Range(0f, 100f);

        baseHeight = height;
        baseWaveAmplitude = waveAmplitude;
        baseWaveSpeed = waveSpeed;

        height = baseHeight * Random.Range(0.8f, 1.5f);
        waveAmplitude = baseWaveAmplitude * Random.Range(0.8f, 1.3f);
        waveSpeed = baseWaveSpeed * Random.Range(0.8f, 1.2f);
    }

    void LateUpdate()
    {
        // Crescimento gradual
        if (growPercent < 1f)
        {
            growPercent += Time.deltaTime / growDuration;
            growPercent = Mathf.Clamp01(growPercent);
        }

        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / (segments - 1);

            // Altura cresce gradualmente
            float y = t * height * growPercent;

            float amplitude = waveAmplitude * t;

            float waveX =
                Mathf.Sin(
                    Time.time * waveSpeed +
                    t * waveFrequency +
                    randomOffset
                ) * amplitude;

            // Inclinação natural
            float bend = t * 0.2f;

            Vector3 pos =
                new Vector3(
                    waveX + bend,
                    y,
                    0f
                );

            line.SetPosition(i, pos);
        }
    }
}