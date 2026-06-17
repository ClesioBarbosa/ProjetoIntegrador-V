Shader "Custom/UnderwaterLight"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.2, 0.5, 0.4, 1)
        _LightColor ("Light Color", Color) = (0.6, 0.9, 0.7, 1)

        _Intensity ("Light Intensity", Float) = 0.5
        _Speed ("Speed", Float) = 1.0
        _Scale ("Pattern Scale", Float) = 2.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        struct Input
        {
            float3 worldPos;
        };

        fixed4 _BaseColor;
        fixed4 _LightColor;

        float _Intensity;
        float _Speed;
        float _Scale;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.worldPos.xz * _Scale;

            // padrão de onda (simula água)
            float wave1 = sin(uv.x + _Time.y * _Speed);
            float wave2 = cos(uv.y + _Time.y * _Speed * 0.8);

            float caustic = (wave1 + wave2) * 0.5;

            caustic = saturate(caustic);

            // mistura luz com base
            fixed3 finalColor = lerp(_BaseColor.rgb, _LightColor.rgb, caustic * _Intensity);

            o.Albedo = finalColor;
        }
        ENDCG
    }

    FallBack "Diffuse"
}