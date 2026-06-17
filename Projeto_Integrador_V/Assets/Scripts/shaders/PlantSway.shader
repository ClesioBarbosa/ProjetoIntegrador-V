Shader "Custom/PlantSway"
{
    Properties
    {
        _ColorBase ("Base Color", Color) = (0.2, 0.5, 0.3, 1)
        _ColorTop ("Top Color", Color) = (0.4, 0.8, 0.5, 1)

        _Amplitude ("Amplitude", Float) = 0.05
        _Frequency ("Frequency", Float) = 1.0
        _Speed ("Speed", Float) = 1.0

        _HeightInfluence ("Height Influence", Float) = 1.0
        _ColorBlend ("Color Blend", Float) = 1.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard vertex:vert

        struct Input
        {
            float3 worldPos;
        };

        float _Amplitude;
        float _Frequency;
        float _Speed;
        float _HeightInfluence;
        float _ColorBlend;

        fixed4 _ColorBase;
        fixed4 _ColorTop;

        // função pseudo-random
        float rand(float2 co)
        {
            return frac(sin(dot(co, float2(12.9898,78.233))) * 43758.5453);
        }

        void vert (inout appdata_full v)
        {
            float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

            // aleatoriedade por posição
            float randomOffset = rand(worldPos.xz);

            // altura influencia o movimento (base menos, topo mais)
            float heightFactor = saturate(worldPos.y * _HeightInfluence);

            // movimento em 2 direções
            float waveX = sin(_Time.y * _Speed + worldPos.x * _Frequency + randomOffset * 6.28);
            float waveZ = cos(_Time.y * _Speed * 0.8 + worldPos.z * _Frequency + randomOffset * 6.28);

            float wave = (waveX + waveZ) * 0.5 * _Amplitude;

            // aplica movimento
            v.vertex.x += wave * heightFactor;
            v.vertex.z += wave * heightFactor;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // gradiente vertical
            float blend = saturate(IN.worldPos.y * _ColorBlend);

            // variação de cor por planta
            float colorRandom = rand(IN.worldPos.xz);

            fixed3 variedBase = lerp(_ColorBase.rgb, _ColorTop.rgb, colorRandom * 0.3);

            fixed3 finalColor = lerp(variedBase, _ColorTop.rgb, blend);

            o.Albedo = finalColor;
        }
        ENDCG
    }

    FallBack "Diffuse"
}