//Shader "_Custom/Light/Anime"
//{
//    Properties
//    {
//        // Texture
//        _Color("颜色", Color) = (1,1,1,1)
//        _MainTex("纹理", 2D) = "white" {}
//
//        // Normal
//        [Enum(Close,0,Open,1)] _Normal("Normal", Float) = 0 // 开启法线
//        _NormalTexture("NormalTexture", 2D) = "black"{} // 法线纹理
//
//        // Bump
//        [Enum(Close,0,Open,1)] _Bump("Bump", Float) = 0 // 开启凹凸
//        _BumpTexture("BumpTexture", 2D) = "black"{} // 凹凸纹理
//        _BumpScale("BumpScale", Range(0.1, 30)) = 30 // 凹凸程度
//
//        // Light
//        _Lightness("Lightness",Range(0,1)) = 1 // 整体亮度
//        _Ambient("Ambient",Range(0,1)) = 0.2 // 自然光
//        _Diffuse("Diffuse",Range(0,1)) = 0.6 // 扩散光
//        _DiffuseRange("DiffuseRange",Range(-1,1)) = 0 // 扩散范围
//        _DiffuseBright("DiffuseBright",Range(-1,1)) = 0 // 扩散明亮面亮度
//        _DiffuseDark("DiffuseDark",Range(-1,1)) = 0 // 扩散阴暗面亮度
//        _Specular("Specular",Range(0,1)) = 1 // 高光
//        _SpecularLightness("SpecularLightness",Range(1,25)) = 5 // 高光亮度
//
//        // Rim
//        _RimLightness("RimLightness",Range(0,1)) = 1 // 轮廓光强度
//        _RimColor("RimColor",Color) = (1,1,1,1) // 轮廓光颜色
//        _RimRadiu("RimRadiu",Range(0,1)) = 0.9 // 轮廓光半径
//        _RimBlur("RimBlur",Range(0,1)) = 0.6 // 轮廓光虚化
//
//        // Side
//        _SideWidth("SideWidth",Range(0,1)) = 0 // 边
//        _SideColor("SideColor",Color) = (1,1,1,1) // 边颜色
//    }
//
//    SubShader
//    {
//        Tags
//        {
//            "RenderType"="Opaque"
//        }
//
//        // 基础灯光渲染（方向光，发射，光照贴图，...）
//        Pass
//        {
//            CGPROGRAM
//            #pragma vertex vertBase
//            #pragma fragment fragBase
//
//            #include "UnityCG.cginc"
//            #include "UnityLightingCommon.cginc"
//
//            // Texture
//            float4 _Color;
//            sampler2D _MainTex;
//
//            // Normal
//            float _Normal;
//            sampler2D _NormalTexture;
//
//            // Bump
//            float _Bump;
//            sampler2D _BumpTexture;
//            float4 _BumpTexture_TexelSize;
//            float _BumpScale;
//
//            // Light
//            float _Lightness;
//            float _Ambient;
//            float _Diffuse;
//            float _DiffuseRange;
//            float _DiffuseBright;
//            float _DiffuseDark;
//            float _Specular;
//            float _SpecularLightness;
//
//            // Rim
//            float _RimLightness;
//            float4 _RimColor;
//            float _RimRadiu;
//            float _RimBlur;
//
//            // Side
//            float _SideWidth;
//            float4 _SideColor;
//
//            struct a2v
//            {
//                float4 vertex : POSITION;
//                float3 normal : NORMAL;
//                float2 uv: TEXCOORD0;
//            };
//
//            struct v2f
//            {
//                float4 vertex : SV_POSITION;
//                float3 normal : NORMAL;
//                float2 uv: TEXCOORD0;
//                float3 view: TEXCOORD1;
//            };
//
//            v2f vertBase(a2v v)
//            {
//                v2f o;
//                o.vertex = UnityObjectToClipPos(v.vertex);
//                o.normal = UnityObjectToWorldNormal(v.normal);
//                o.uv = v.uv;
//                o.view = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
//                return o;
//            }
//
//            float4 fragBase(v2f i) : SV_Target
//            {
//                float3 light = normalize(_WorldSpaceLightPos0.xyz); // 归一化光照
//
//                // Texture
//                float4 text = tex2D(_MainTex, i.uv) * _Color;
//
//                // Normal
//                float3 normalColor = UnpackNormal(tex2D(_NormalTexture, i.uv));
//                fixed3 normalDot = dot(normalColor, light) / 2 + 0.5;
//                fixed3 normal = normalDot * 5 * _LightColor0;
//                text = lerp(text, float4(text.rgb * normal, 1), step(1, _Normal));
//
//                // Bump
//                fixed bumpU = tex2D(_BumpTexture, i.uv + fixed2(-1.0 * _BumpTexture_TexelSize.x, 0)).r - tex2D(
//                    _BumpTexture, i.uv + fixed2(1.0 * _BumpTexture_TexelSize.x, 0)).r;
//                fixed bumpV = tex2D(_BumpTexture, i.uv + fixed2(0, -1.0 * _BumpTexture_TexelSize.y)).r - tex2D(
//                    _BumpTexture, i.uv + fixed2(0, 1.0 * _BumpTexture_TexelSize.y)).r;
//                fixed3 bumpNormal = normalize(i.normal);
//                fixed3 bumpDot = dot(fixed3(bumpNormal.x * bumpU * _BumpScale, bumpNormal.y * bumpV * _BumpScale,
//                                            bumpNormal.z), light) / 2 + 0.5;
//                fixed3 bump = bumpDot * 1.5 * _LightColor0;
//                text = lerp(text, float4(text.rgb * bump, 1), step(1, _Bump));
//
//                // Ambient
//                float3 ambi = _Ambient * _LightColor0;
//
//                // Diffuse
//                float diffDot = dot(i.normal, light);
//                diffDot = diffDot < _DiffuseRange ? _DiffuseDark : _DiffuseBright;
//                float3 diff = diffDot * _Diffuse * _LightColor0;
//
//                // Specular
//                float3 specRefl = reflect(-light, i.normal);
//                float specDot = pow(max(0, dot(i.view, specRefl)), _SpecularLightness);
//                specDot = smoothstep(0.005, 0.1, specDot);
//                float3 spec = specDot * _Specular * _LightColor0;
//
//                // Rim
//                float rimDotLight = dot(i.normal, light);
//                float rimDotView = 1 - dot(i.normal, i.view);
//                float rimSmooth = max(0, rimDotLight) * smoothstep(_RimRadiu - _RimBlur, _RimRadiu + _RimBlur,
//                                                                   rimDotView);
//                float3 rim = rimSmooth * _RimLightness * _RimColor * _LightColor0;
//
//                return float4(lerp(_SideColor.rgb, text * float4((ambi + diff + spec + rim), 1) * _Lightness,
//                                   step(rimDotView, 1 - _SideWidth)), 1);
//            }
//            ENDCG
//        }
//    }
//
//    FallBack "VertexLit"
//}