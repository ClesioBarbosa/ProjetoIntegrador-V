using UnityEngine;

public class FishDespawn : MonoBehaviour
{
private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);

        bool exit =
            viewPos.z < 0 ||
            viewPos.x < -0.2f || viewPos.x > 1.2f ||
            viewPos.y < -0.2f || viewPos.y > 1.2f;

        if (exit)
        {
            Destroy(gameObject);
        }
    }
}