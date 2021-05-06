Shader "_Custom/PostProcessing/Color"
{
    Properties
    {
    }

    SubShader
    {
        Cull Off
        ZWrite Off
        ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex VertDefault
            #pragma fragment Frag
            #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

            TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
            float4 _Color;
            float _ColorScale;
            float _GrayScale;
            float _ReverseScale;

            float4 Frag(VaryingsDefault i) : SV_Target
            {
                float4 textureColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

                float4 color = lerp(textureColor, textureColor * _Color, _ColorScale);

                // 灰度
                float gray = textureColor.r * 0.299 + textureColor.g * 0.587 + textureColor.b * 0.114;
                color = lerp(color, float4(gray, gray, gray, 1), _GrayScale);

                // 反色
                color = lerp(color, float4(1 - color.r, 1 - color.g, 1 - color.b, 1), _ReverseScale);

                return color;
            }
            ENDHLSL
        }
    }

    FallBack "VertexLit"
}