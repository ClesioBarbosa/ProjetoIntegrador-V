Shader "Custom/SeaFloorWave"
{
    Properties
    {
        _SandColor ("Sand Color", Color) = (0.6, 0.5, 0.3, 1)
        _WaterColor ("Water Color", Color) = (0.2, 0.5, 0.4, 1)

        _Amplitude ("Amplitude", Float) = 0.2
        _Frequency ("Frequency", Float) = 1.0
        _Speed ("Speed", Float) = 1.0

        _ColorBlend ("Color Blend Height", Float) = 1.0
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

        float _ColorBlend;

        fixed4 _SandColor;
        fixed4 _WaterColor;

        void vert (inout appdata_full v)
        {
            float waveX = sin(_Time.y * _Speed + v.vertex.x * _Frequency);
            float waveZ = cos(_Time.y * _Speed * 0.8 + v.vertex.z * _Frequency);

            float wave = (waveX + waveZ) * 0.5 * _Amplitude;

            v.vertex.y += wave;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // usa altura no mundo pra misturar cor
            float blend = saturate(IN.worldPos.y * _ColorBlend);

            fixed3 finalColor = lerp(_SandColor.rgb, _WaterColor.rgb, blend);

            o.Albedo = finalColor;
        }
        ENDCG
    }

    FallBack "Diffuse"
}