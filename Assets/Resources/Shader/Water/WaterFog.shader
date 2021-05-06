Shader "_Custom/Water/Fog"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _FogDepth ("Fog Depth", Float) = 5
        _EdgeBlend ("Edge Blend", Range(0,1)) = 0.25
        _DepthBlur ("Depth Blur", Float) = 1

        _Noise ("Noise", 2D) = "white" {}

        _FogCullDistance ("Water Cull Distance", Float) = 1000
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue"="Transparent"
            "IgnoreProjector" = "True"
        }

        ZWrite On
        Cull Back
        LOD 200

        GrabPass
        {
        }

        CGPROGRAM
        #pragma surface surf BlinnPhong noshadow novertexlights nolightmap vertex:vert alpha:blend
        #pragma target 3.0
        sampler2D _MainTex;
        sampler2D _Noise;
        sampler2D _GrabTexture;

        half4 _GrabTexture_TexelSize;
        float _FogCullDistance;
        UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
        fixed4 _Color;
        float _FogDepth, _EdgeBlend;
        float _DepthBlur, _Focus;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        struct Input
        {
            float4 projPos;
            float4 grabPos;
            float3 worldPos;
            float2 localDir;
            float4 screenPos;
            float3 viewDir;
            float2 uv_Noise;
            float depth;
        };

        inline float InverseLerp(float a, float b, float value)
        {
            return saturate((value - a) / (b - a));
        }

        inline fixed3 Blend(fixed3 bg, fixed3 fore, fixed alpha)
        {
            return (bg * (1 - alpha)) + (alpha * fore);
        }

        static const float3 forward = float3(0, 0, 1);

        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.localDir = v.vertex.xz;
            float4 vertex = UnityObjectToClipPos(v.vertex);
            o.projPos = ComputeScreenPos(vertex);
            COMPUTE_EYEDEPTH(o.projPos.z);
            o.grabPos = ComputeGrabScreenPos(vertex);
            float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

            float3 relPos = worldPos.xyz - _WorldSpaceCameraPos.xyz;
            o.depth = length(relPos);
        }

        void surf(Input IN, inout SurfaceOutput o)
        {
            float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.projPos)));
            float partZ = IN.projPos.z;
            float zDiff = abs(sceneZ - partZ);
            float fog = 1 - InverseLerp(_FogDepth, 0, zDiff);
            float fade = InverseLerp(_EdgeBlend, 0, zDiff);

            float4 bgUV = IN.grabPos;
            bgUV.xy /= bgUV.w;
            fixed3 bg = tex2D(_GrabTexture, bgUV.xy).rgb;

            float distanceAlpha = 1 - InverseLerp(_FogCullDistance * 0.7, _FogCullDistance, IN.depth);
            float2 uv = IN.uv_Noise;

            half n = tex2D(_Noise, uv);
            half nd = tex2D(_Noise, uv * 5);
            o.Albedo = Blend(bg, _Color, fog * _Color.a * (n * nd));
            o.Specular = 0;
            o.Alpha = min(1 - fade, distanceAlpha);
        }
        ENDCG
    }

    FallBack "Diffuse"
}