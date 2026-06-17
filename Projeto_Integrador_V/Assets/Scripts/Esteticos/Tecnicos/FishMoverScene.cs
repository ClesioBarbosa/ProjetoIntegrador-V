using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMoverScene : MonoBehaviour
{
     [Header("Movimento")]
    public float speed = 2f;

    [Header("Flutuação")]
    public float verticalAmplitude = 0.2f;
    public float verticalFrequency = 1f;

    private Vector3 direction;

    private float randomOffset;

    void Start()
    {
        randomOffset = Random.Range(0f, 100f);

        if (transform.position.x < 0)
        {
            direction = Vector3.right;
        }
        else
        {
            direction = Vector3.left;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    void Update()
    {
        float yWave =
            Mathf.Sin(
                Time.time * verticalFrequency +
                randomOffset
            ) * verticalAmplitude;

        transform.position +=
            direction * speed * Time.deltaTime;

        transform.position +=
            Vector3.up *
            yWave *
            Time.deltaTime;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
