using UnityEngine;

public class FishFollower : MonoBehaviour
{
    public Transform leader;
    public Vector3 offset;
    public float speed = 3f;

    void Update()
    {
        if (leader == null) return;

        Vector3 target = leader.position + offset;
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);

        transform.LookAt(leader);
    }
}