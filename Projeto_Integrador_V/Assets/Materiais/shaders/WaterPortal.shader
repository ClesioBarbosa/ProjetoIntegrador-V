Shader "Custom/WaterPortal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _ColorA ("Color A", Color) = (0.1,0.5,1,1)
        _ColorB ("Color B", Color) = (0.8,1,1,1)

        _Strength ("Distortion Strength", Float) = 0.05
        _Speed ("Speed", Float) = 2.0
        _Scale ("Wave Scale", Float) = 10.0

        _Direction ("Direction", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;

        fixed4 _ColorA;
        fixed4 _ColorB;

        float _Strength;
        float _Speed;
        float _Scale;
        float _Direction;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 uv = IN.uv_MainTex;

            // centro da UV
            float2 center = float2(0.5, 0.5);

            // direção do centro
            float2 dir = uv - center;

            // distância do centro
            float dist = length(dir);

            // normaliza direção
            dir = normalize(dir);

            // onda radial
            float wave = sin(dist * _Scale - _Time.y * _Speed * _Direction);

            // distorce UV
            uv += dir * wave * _Strength;

            fixed4 tex = tex2D(_MainTex, uv);

            // mistura cores
            fixed3 finalColor = lerp(_ColorA.rgb, _ColorB.rgb, wave * 0.5 + 0.5);

            o.Albedo = tex.rgb * finalColor;

            o.Emission = finalColor * 0.5;

            o.Smoothness = 1;
            o.Metallic = 0;
        }
        ENDCG
    }

    FallBack "Diffuse"
}