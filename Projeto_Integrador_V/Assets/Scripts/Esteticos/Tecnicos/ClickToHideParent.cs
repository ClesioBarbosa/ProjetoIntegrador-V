using UnityEngine;
using System.Collections;

public class ClickToHideParent : MonoBehaviour
{
    [Header("Fade")]
    public float fadeDuration = 1f;

    private Transform parentObj;
    private Renderer[] renderers;
    private Collider[] colliders;

    private bool isDisappearing = false;

    void Start()
    {
        parentObj = transform.parent;

        if (parentObj == null)
        {
            Debug.LogWarning("Esse objeto precisa ser filho de outro objeto.");
            return;
        }

        renderers = parentObj.GetComponentsInChildren<Renderer>();
        colliders = parentObj.GetComponentsInChildren<Collider>();
    }

    void Update()
    {
        // Mouse (PC)
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick(Input.mousePosition);
        }

        // Touch (Mobile)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CheckClick(Input.GetTouch(0).position);
        }
    }

    void CheckClick(Vector2 screenPos)
    {
        if (isDisappearing) return;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // verifica se clicou exatamente nesse filho
            if (hit.transform == transform)
            {
                StartCoroutine(FadeAndDestroy());
            }
        }
    }

    IEnumerator FadeAndDestroy()
    {
        isDisappearing = true;

        // desativa colisões
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        float time = 0;

        Material[][] materials = new Material[renderers.Length][];

        // pega materiais
        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].materials;

            foreach (Material mat in materials[i])
            {
                // ativa transparência
                mat.SetFloat("_Mode", 3);

                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                mat.SetInt("_ZWrite", 0);

                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                mat.renderQueue = 3000;
            }
        }

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(1, 0, time / fadeDuration);

            for (int i = 0; i < materials.Length; i++)
            {
                foreach (Material mat in materials[i])
                {
                    if (mat.HasProperty("_Color"))
                    {
                        Color c = mat.color;
                        c.a = alpha;
                        mat.color = c;
                    }
                }
            }

            yield return null;
        }

        Destroy(parentObj.gameObject);
    }
}