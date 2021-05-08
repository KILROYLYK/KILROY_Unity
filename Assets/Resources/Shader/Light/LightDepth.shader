Shader "_Custom/Light/Depth"
{
    Properties
    {
        _Color("Color",Color) = (1,1,1,1) // 颜色
        _Brightness("Brightness",Range(0,1)) = 1 // 亮度
        _ShadowRange("ShadowRange",Range(1,10)) = 3 // 距离
        _ScannerWidth("ScannerWidth",float) = 0 // 扫描宽度
        _ScannerDistance("ScannerDistance",float) = 0 // 扫描
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color;
            float _Brightness;
            float _ShadowRange;
            float _ScannerWidth;
            float _ScannerDistance;

            struct a2v // 应用阶段到vertex shader阶段的数据
            {
                float4 vertex : POSITION;
            };

            struct v2f // vertex shader阶段输出的内容
            {
                float4 vertex : SV_POSITION;
                float depth : DEPTH;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.depth = -UnityObjectToViewPos(v.vertex).z * _ProjectionParams.w * _ShadowRange;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float invert = 1 - i.depth;
                float color = invert * _Brightness;

                if (_ScannerWidth > 0 && _ScannerDistance > 0 &&
                    abs(i.depth - _ScannerDistance) < _ScannerWidth)
                    color = color * 2;

                return fixed4(color, color, color, 1) * _Color;
            }
            ENDCG
        }
    }
    FallBack "VertexLit"
}