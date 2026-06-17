using UnityEngine;

public class FishLeader : MonoBehaviour
{
    public float speed = 5f;
    public float changeDirectionTime = 2f;

    private Vector3 direction;
    private float timer;

    void Start()
    {
        SetNewDirection();
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer > changeDirectionTime)
        {
            SetNewDirection();
            timer = 0;
        }
    }

    void SetNewDirection()
    {
        direction = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.2f, 0.2f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}