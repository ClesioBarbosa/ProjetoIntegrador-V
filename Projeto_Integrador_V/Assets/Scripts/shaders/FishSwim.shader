Shader "Custom/FishSwim"//movimento peixe
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        [Header(Main Movement)]
        _SwimSpeed ("Swim Speed", Float) = 2
        _WaveFrequency ("Wave Frequency", Float) = 4
        _WaveAmplitude ("Wave Amplitude", Float) = 0.08

        [Header(Serpentine Movement)]
        _SerpentineStrength ("Serpentine Strength", Float) = 1
        _SerpentineFrequency ("Serpentine Frequency", Float) = 2

        [Header(Vertical Movement)]
        [Toggle]_UseVerticalMotion ("Use Vertical Motion", Float) = 0

        _VerticalAmplitude ("Vertical Amplitude", Float) = 0.03
        _VerticalFrequency ("Vertical Frequency", Float) = 2

        [Header(Tail Control)]
        _TailStrength ("Tail Strength", Float) = 1.5
        _BodyOffset ("Body Offset", Float) = 0

        [Header(Color)]
        _ColorA ("Color A", Color) = (1,1,1,1)
        _ColorB ("Color B", Color) = (0.8,0.9,1,1)

        _ColorSpeed ("Color Speed", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        LOD 200

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows vertex:vert

        sampler2D _MainTex;

        float _SwimSpeed;
        float _WaveFrequency;
        float _WaveAmplitude;

        float _SerpentineStrength;
        float _SerpentineFrequency;

        float _UseVerticalMotion;
        float _VerticalAmplitude;
        float _VerticalFrequency;

        float _TailStrength;
        float _BodyOffset;

        fixed4 _ColorA;
        fixed4 _ColorB;

        float _ColorSpeed;

        struct Input
        {
            float2 uv_MainTex;
        };

        void vert(inout appdata_full v)
        {
            // posição ao longo do corpo
            float bodyPos = v.vertex.z + _BodyOffset;

            // máscara progressiva da cauda
            float tailMask = saturate(bodyPos * _TailStrength);

            // onda principal contínua
           float waveTime =
                 frac(_Time.y * _SwimSpeed);

           float mainWave =
                 sin(
                     (waveTime * 6.28318) +
                     (bodyPos * _WaveFrequency)
    );

            // onda serpentina
            float serpentineWave =
                sin(
                    (_Time.y * _SwimSpeed * 0.7) +
                    (bodyPos * _SerpentineFrequency)
                );

            // movimento lateral serpentino
            float sideMovement =
                (
                    mainWave +
                    (serpentineWave * _SerpentineStrength)
                )
                * _WaveAmplitude
                * tailMask;

            v.vertex.x += sideMovement;

            // movimento vertical opcional
            if (_UseVerticalMotion > 0.5)
            {
                float verticalWave =
                    sin(
                        (_Time.y * _SwimSpeed) +
                        (bodyPos * _VerticalFrequency)
                    );

                v.vertex.y +=
                    verticalWave *
                    _VerticalAmplitude *
                    tailMask;
            }
        }

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);

            // mistura dinâmica de cor
            float colorWave =
                sin(_Time.y * _ColorSpeed) * 0.5 + 0.5;

            fixed3 finalColor =
                lerp(
                    _ColorA.rgb,
                    _ColorB.rgb,
                    colorWave
                );

            o.Albedo = tex.rgb * finalColor;

            o.Smoothness = 0.7;
            o.Metallic = 0;

            // leve emissão
            o.Emission = finalColor * 0.08;

            o.Alpha = tex.a;
        }

        ENDCG
    }

    FallBack "Diffuse"

}