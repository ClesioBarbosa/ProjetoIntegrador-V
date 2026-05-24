using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class AlgaeWave : MonoBehaviour
{
    public int segments = 6;//define a quantidade de nós que a linha tem
    public float height = 3f;
    private float baseHeight;

    public float waveAmplitude = 0.3f; // até onde vai
    public float waveFrequency = 2f; // quão drastico ou não pode ser o movimento
    public float waveSpeed = 2f;

    private LineRenderer line;
    private float randomOffset;
    public Vector3 localOffset;

    void Awake()
    {
    line = GetComponent<LineRenderer>();

    line.positionCount = segments;
    line.useWorldSpace = false;

    randomOffset = Random.Range(0f, 100f);

    baseHeight = height;

    waveSpeed *= Random.Range(0.8f, 1.2f);
    waveAmplitude *= Random.Range(0.8f, 1.3f);

    height = baseHeight * Random.Range(0.8f, 1.5f);
    }

    void LateUpdate()
    {
        for (int i = 0; i < segments; i++)
        {
            float t = (float)i / (segments - 1);

            float y = t * height;

            float amplitude = waveAmplitude * t;

            float waveX = Mathf.Sin(
                Time.time * waveSpeed +
                t * waveFrequency +
                randomOffset
            ) * amplitude;

            // leve inclinação natural
            float bend = t * 0.2f;

            Vector3 pos = localOffset + new Vector3(waveX + bend, y, 0f);

            line.SetPosition(i, pos);
        }
    }
}