using UnityEngine;
using System.Collections;

public class TouchTrailSpawner : MonoBehaviour
{
    [Header("Objeto Visual")]
    public GameObject visualPrefab;

    [Header("Distância da câmera")]
    public float spawnDistance = 5f;

    [Header("Desaparecimento")]
    public float fadeDuration = 1f;

    private GameObject currentObject;

    void Update()
    {
        // =========================
        // MOUSE (PC)
        // =========================

        if (Input.GetMouseButtonDown(0))
        {
            SpawnObject(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            MoveObject(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            RemoveObject();
        }

        // =========================
        // TOUCH (MOBILE)
        // =========================

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                SpawnObject(touch.position);
            }

            if (touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
            {
                MoveObject(touch.position);
            }

            if (touch.phase == TouchPhase.Ended ||
                touch.phase == TouchPhase.Canceled)
            {
                RemoveObject();
            }
        }
    }

    void SpawnObject(Vector2 screenPosition)
    {
        if (visualPrefab == null) return;

        Vector3 worldPos = GetWorldPosition(screenPosition);

        currentObject = Instantiate(
            visualPrefab,
            worldPos,
            Quaternion.identity
        );
    }

    void MoveObject(Vector2 screenPosition)
    {
        if (currentObject == null) return;

        currentObject.transform.position =
            GetWorldPosition(screenPosition);
    }

    void RemoveObject()
    {
        if (currentObject != null)
        {
            StartCoroutine(FadeAndDestroy(currentObject));

            currentObject = null;
        }
    }

    IEnumerator FadeAndDestroy(GameObject obj)
    {
        float timer = 0;

        Renderer[] renderers =
            obj.GetComponentsInChildren<Renderer>();

        Vector3 startScale =
            obj.transform.localScale;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float t = timer / fadeDuration;

            // diminui escala
            obj.transform.localScale =
                Vector3.Lerp(
                    startScale,
                    Vector3.zero,
                    t
                );

            // fade alpha
            foreach (Renderer r in renderers)
            {
                foreach (Material mat in r.materials)
                {
                    if (mat.HasProperty("_Color"))
                    {
                        Color c = mat.color;

                        c.a = Mathf.Lerp(1, 0, t);

                        mat.color = c;
                    }
                }
            }

            yield return null;
        }

        Destroy(obj);
    }

    Vector3 GetWorldPosition(Vector2 screenPosition)
    {
        Ray ray =
            Camera.main.ScreenPointToRay(screenPosition);

        Vector3 worldPos =
            ray.GetPoint(spawnDistance);

        return worldPos;
    }
}