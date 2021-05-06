Shader "_Custom/Light/Rim"
{
    Properties
    {
        // 纹理
        _Color("颜色", Color) = (1,1,1,1)
        _MainTex("纹理", 2D) = "white" {}

        // 透明度修剪
        _Cutoff("通道透明度", Range(0.0, 1.0)) = 0.5

        // 平滑度
        [Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel("平滑纹理渠道", Float) = 0
        _Glossiness("平滑度", Range(0.0, 1.0)) = 0.5
        _GlossMapScale("平滑度程度", Range(0.0, 1.0)) = 1.0

        // 金属
        [Gamma] _Metallic("金属性", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap("金属性纹理", 2D) = "white" {}

        // 镜面
        [ToggleOff] _SpecularHighlights("镜面高光", Float) = 1.0
        [ToggleOff] _GlossyReflections("光感", Float) = 1.0

        // 法线
        _BumpScale("法线程度", Float) = 1.0
        [Normal] _BumpMap("法线纹理", 2D) = "bump" {}

        // 凹凸
        _Parallax("凹凸程度", Range (0.005, 0.08)) = 0.02
        _ParallaxMap("凹凸纹理", 2D) = "black" {}

        // 遮挡
        _OcclusionStrength("遮挡程度", Range(0.0, 1.0)) = 1.0
        _OcclusionMap("遮挡纹理", 2D) = "white" {}

        // 发散
        _EmissionColor("发散颜色", Color) = (0,0,0)
        _EmissionMap("发散纹理", 2D) = "white" {}

        // 细节遮罩
        _DetailMask("细节遮罩纹理", 2D) = "white" {}
        _DetailAlbedoMap("细节反照率x2纹理", 2D) = "grey" {}
        _DetailNormalMapScale("细节法线程度", Float) = 1.0
        _DetailNormalMap("细节法线纹理", 2D) = "bump" {}

        // UV
        [Enum(UV0,0,UV1,1)] _UVSec("UV设置辅助纹理", Float) = 0

        // 前置渲染选项
        [HideInInspector] _Mode("__mode", Float) = 0
        [HideInInspector] _SrcBlend("__src", Float) = 1

        // 进阶渲染选项
        [HideInInspector] _DstBlend("__dst", Float) = 0
        [HideInInspector] _ZWrite("__zw", Float) = 1

        //---------- 拓展 Start ----------//
        _RimLightness("RimLightness",Range(0,1)) = 1 // 轮廓光强度
        _RimColor("RimColor",Color) = (1,1,1,1) // 轮廓光颜色
        _RimRadiu("RimRadiu",Range(0,1)) = 0.9 // 轮廓光半径
        _RimBlur("RimBlur",Range(0,1)) = 0.6 // 轮廓光虚化
        _SideWidth("SideWidth",Range(0,1)) = 0 // 边宽度
        _SideColor("SideColor",Color) = (1,1,1,1) // 边颜色
        //---------- 拓展 End ----------//
    }

    CGINCLUDE
    #define UNITY_SETUP_BRDF_INPUT MetallicSetup
    ENDCG

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        // 基础灯光渲染（方向光，发射，光照贴图，...）
        Pass
        {
            Name "FORWARD"

            Tags
            {
                "LightMode"="ForwardBase"
            }

            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]

            CGPROGRAM
            #pragma target 3.0
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_fragment _EMISSION
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local_fragment _DETAIL_MULX2
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _GLOSSYREFLECTIONS_OFF
            #pragma shader_feature_local _PARALLAXMAP
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma multi_compile_instancing
            // #pragma multi_compile _ LOD_FADE_CROSSFADE // 抖动LOD交叉淡入淡出
            #pragma vertex vertBase
            #pragma fragment fragBase

            #include "UnityStandardCoreForward.cginc"
            ENDCG
        }

        // 附加灯光渲染（每次通过一灯）
        Pass
        {
            Name "FORWARD_DELTA"

            Tags
            {
                "LightMode"="ForwardAdd"
            }

            Blend [_SrcBlend] One
            ZWrite Off
            ZTest LEqual
            Fog // 在加和雾中应为黑色
            {
                Color (0,0,0,0)
            }

            CGPROGRAM
            #pragma target 3.0
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _DETAIL_MULX2
            #pragma shader_feature_local _PARALLAXMAP
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            // #pragma multi_compile _ LOD_FADE_CROSSFADE // 抖动LOD交叉淡入淡出
            #pragma vertex vertAdd
            #pragma fragment fragAdd

            #include "UnityStandardCoreForward.cginc"
            ENDCG
        }

        // 阴影渲染
        Pass
        {
            Name "SHADOWCASTER"

            Tags
            {
                "LightMode"="ShadowCaster"
            }

            ZWrite On ZTest LEqual

            CGPROGRAM
            #pragma target 3.0
            #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local _PARALLAXMAP
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_instancing
            // #pragma multi_compile _ LOD_FADE_CROSSFADE // 抖动LOD交叉淡入淡出
            #pragma vertex vertShadowCaster
            #pragma fragment fragShadowCaster

            #include "UnityStandardShadow.cginc"
            ENDCG
        }

        // 延期通行证
        Pass
        {
            Name "DEFERRED"

            Tags
            {
                "LightMode"="Deferred"
            }

            CGPROGRAM
            #pragma target 3.0
            #pragma exclude_renderers nomrt
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_fragment _EMISSION
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _DETAIL_MULX2
            #pragma shader_feature_local _PARALLAXMAP
            #pragma multi_compile_prepassfinal
            #pragma multi_compile_instancing
            // #pragma multi_compile _ LOD_FADE_CROSSFADE // 抖动LOD交叉淡入淡出
            #pragma vertex vertDeferred
            #pragma fragment fragDeferred

            #include "UnityStandardCore.cginc"
            ENDCG
        }

        // 提取照明贴图，GI（发射，反照率...）的信息
        // 此传递在常规渲染期间不使用。
        Pass
        {
            Name "META"

            Tags
            {
                "LightMode"="Meta"
            }

            Cull Off

            CGPROGRAM
            #pragma vertex vert_meta
            #pragma fragment frag_meta
            #pragma shader_feature_fragment _EMISSION
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _DETAIL_MULX2
            #pragma shader_feature EDITOR_VISUALIZATION

            #include "UnityStandardMeta.cginc"
            ENDCG
        }

        //---------- 拓展 Start ----------//
        // 边缘光
        Pass
        {
            Name "FORWARD_RIM"

            Tags
            {
                "LightMode"="ForwardBase"
            }

            Blend [_SrcBlend] One
            Fog // 在加和雾中应为黑色
            {
                Color (0,0,0,0)
            }

            CGPROGRAM
            #pragma vertex vertRim
            #pragma fragment fragRim
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            float _RimLightness;
            float4 _RimColor;
            float _RimRadiu;
            float _RimBlur;
            float _SideWidth;
            float4 _SideColor;

            struct a2v // 应用阶段到vertex shader阶段的数据
            {
                float4 vertex: POSITION;
                float3 normal: NORMAL;
                float2 uv: TEXCOORD0;
            };

            struct v2f // vertex shader阶段输出的内容
            {
                float4 vertex: SV_POSITION;
                float3 normal: NORMAL;
                float2 uv: TEXCOORD0;
                float3 view: TEXCOORD1;
            };

            //定义顶点shader
            v2f vertRim(a2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.uv = v.uv;
                o.view = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
                return o;
            }

            //定义片元shader
            float4 fragRim(v2f i) : SV_Target
            {
                float3 light = normalize(_WorldSpaceLightPos0.xyz); // 归一化光照

                // Rim
                float diffDot = dot(i.normal, light);
                float rimDot = 1 - dot(i.normal, i.view);
                float rimSmooth = max(0, diffDot) * smoothstep(_RimRadiu - _RimBlur, _RimRadiu + _RimBlur, rimDot);
                float3 rim = rimSmooth * _RimLightness * _RimColor * _LightColor0;

                return float4(lerp(_SideColor.rgb, rim, step(rimDot - 0.05, 1 - _SideWidth)), 1);
            }
            ENDCG
        }
        //---------- 拓展 End ----------//
    }

    FallBack "VertexLit"
    // CustomEditor "StandardShaderGUI"
}