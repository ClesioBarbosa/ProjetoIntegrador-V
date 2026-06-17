using UnityEngine;

[ExecuteAlways]
public class ChangeMaterialProperties : MonoBehaviour
{
    public Color color = Color.white;

    public Vector2 tiling = Vector2.one;
    public Vector2 offset = Vector2.zero;

    private Renderer rend;
    private MaterialPropertyBlock mpb;

    void Awake()
    {
        Setup();
        Apply();
    }

    void OnValidate()
    {
        Setup();
        Apply();
    }

    void Setup()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();

        if (mpb == null)
            mpb = new MaterialPropertyBlock();
    }

    void Apply()
    {
        if (rend == null) return;

        rend.GetPropertyBlock(mpb);

        // Cor
        mpb.SetColor("_Color", color); // ou "_BaseColor"

        // Tiling + Offset (MainTex)
        mpb.SetVector("_MainTex_ST", new Vector4(
            tiling.x,
            tiling.y,
            offset.x,
            offset.y
        ));

        rend.SetPropertyBlock(mpb);
    }
}