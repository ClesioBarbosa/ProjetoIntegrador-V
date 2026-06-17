using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 3f;
    public float turnSpeed = 2f;
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
        if (timer >= changeDirectionTime)
        {
            SetNewDirection();
            timer = 0;
        }

        // Rotaciona suavemente para a direção
        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    void SetNewDirection()
    {
        direction = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.3f, 0.3f),
            Random.Range(-1f, 1f)
        ).normalized;
    }
}