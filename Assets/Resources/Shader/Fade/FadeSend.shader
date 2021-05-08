Shader "_Custom/Fade/Send"
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
        _SendScale("SendScale", float) = 0 // 传送程度
        _SideColor_1("SideColor_1", Color) = (1,1,1,1) // 边颜色_1
        _SideColor_2("SideColor_2", Color) = (1,1,1,1) // 边颜色_2
        _SideNoise("SideNoise", 2D) = "white" {} // 边噪声
        _SideWidth("SendWidth", float) = 0.5 // 边宽度
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
            Cull Off

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
            // #pragma fragment fragBase

            #include "UnityStandardCoreForward.cginc"

            //---------- 拓展 Start ----------//
            #pragma fragment fragBaseExpand

            float _SendScale;
            float4 _SideColor_1;
            float4 _SideColor_2;
            sampler2D _SendSide;
            float _SideWidth;

            half4 fragBaseExpand(VertexOutputForwardBase i) : SV_Target
            {
                half4 noise = tex2D(_SendSide, i.tex.xy) + 0.5;

                FRAGMENT_SETUP(s);
                float3 pos = s.posWorld - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

                fixed cutout = lerp(i.tex.r - 1, i.tex.r, step(pos.y, _SendScale));
                clip(cutout);

                half4 iColor = fragForwardBaseInternal(i);

                float ramp = _SideWidth / 2;
                float4 dColor1 = lerp(_SideColor_1, _SideColor_2, smoothstep(_SendScale, _SendScale - ramp, pos.y));
                float4 dColor2 = lerp(dColor1 * noise, iColor, smoothstep(_SendScale, _SendScale - ramp * 2, pos.y));
                half4 sColor = lerp(iColor, dColor2, step(_SendScale - _SideWidth, pos.y));

                return sColor;
            }

            //---------- 拓展 End ----------//
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
            // #pragma fragment fragAdd

            #include "UnityStandardCoreForward.cginc"

            //---------- 拓展 Start ----------//
            #pragma fragment fragAddExpand

            float _SendScale;

            half4 fragAddExpand(VertexOutputForwardAdd i) : SV_Target
            {
                FRAGMENT_SETUP_FWDADD(s);
                float3 pos = s.posWorld - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

                fixed cutout = lerp(i.tex.r - 1, i.tex.r, step(pos.y, _SendScale));
                clip(cutout);

                return fragForwardAddInternal(i);
            }

            //---------- 拓展 End ----------//
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

            ZWrite On
            ZTest LEqual
            Cull Off

            CGPROGRAM
            #pragma target 3.0
            #pragma shader_feature_local _ _ALPHATEST_ON _ALPHABLEND_ON _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local _METALLICGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local _PARALLAXMAP
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_instancing
            // #pragma multi_compile _ LOD_FADE_CROSSFADE // 抖动LOD交叉淡入淡出
            // #pragma vertex vertShadowCaster
            // #pragma fragment fragShadowCaster

            // #include "UnityStandardShadow.cginc"

            //---------- 拓展 Start ----------//
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_fragment _EMISSION
            #pragma shader_feature_local_fragment _DETAIL_MULX2
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _GLOSSYREFLECTIONS_OFF
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma vertex vertBase
            #pragma fragment fragShadowCasterExpand

            #include "UnityStandardCoreForward.cginc"

            float _SendScale;

            half4 fragShadowCasterExpand(VertexOutputForwardBase i) : SV_Target
            {
                FRAGMENT_SETUP(s);
                float3 pos = s.posWorld - mul(unity_ObjectToWorld, float4(0, 0, 0, 1)).xyz;

                fixed cutout = lerp(i.tex.r - 1, i.tex.r, step(pos.y, _SendScale));
                clip(cutout);

                return fragForwardBaseInternal(i);
            }

            //---------- 拓展 End ----------//
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
        //---------- 拓展 End ----------//
    }

    FallBack "VertexLit"
    // CustomEditor "StandardShaderGUI"
}