using UnityEngine;

public class FishColor : MonoBehaviour
{
    void Start()
    {
        TrailRenderer trail = GetComponent<TrailRenderer>();

        if (trail != null)
        {
            Color color = Random.ColorHSV(
                0.5f, 0.7f,   // faixa de cor (azul/verde, opcional)
                0.6f, 1f,
                0.7f, 1f
            );

            // Gradiente do trail
            Gradient gradient = new Gradient();

            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(color, 0.0f),
                    new GradientColorKey(color, 1.0f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1.0f, 0.0f),
                    new GradientAlphaKey(0.0f, 1.0f)
                }
            );

            trail.colorGradient = gradient;
        }
    }
}