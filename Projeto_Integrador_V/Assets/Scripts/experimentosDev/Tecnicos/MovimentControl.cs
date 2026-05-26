
using UnityEngine;

public class MovimentControl : MonoBehaviour
{
    public Transform parentObject;

    [Header("Rotação")]
    public float rotationSpeed = 2f;

    [Header("Movimento")]
    public float smoothVelocity = 5f;

    [Header("Offset")]
    public Vector3 rotationOffset;

    private Vector3 lastPosition;
    private Vector3 currentDirection;

    void Start()
    {
        if (parentObject == null)
            parentObject = transform.parent;

        lastPosition = parentObject.position;

        currentDirection = parentObject.forward;
    }

    void LateUpdate()
    {
        Vector3 movement =
            (parentObject.position - lastPosition);

        // evita problemas quando parado
        if (movement.sqrMagnitude > 0.00001f)
        {
            // suaviza direção
            currentDirection = Vector3.Lerp(
                currentDirection,
                movement.normalized,
                smoothVelocity * Time.deltaTime
            );

            Quaternion targetRotation =
                Quaternion.LookRotation(currentDirection);

            targetRotation *= Quaternion.Euler(rotationOffset);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        lastPosition = parentObject.position;
    }
}
