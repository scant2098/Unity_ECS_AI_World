Shader "Custom/Outline" {
    Properties {
        _OutlineColor("Outline Color", Color) = (0, 1, 0, 1)
        _OutlineWidth("Outline Width", Range(0.002, 0.1)) = 0.01
        _MainTex("Main Texture", 2D) = "white" {}
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            float _OutlineWidth;
            float4 _OutlineColor;
            sampler2D _MainTex;

            v2f vert(appdata_t v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target {
                // 计算边缘线条
                half4 outlineColor = _OutlineColor;
                half4 insideColor = tex2D(_MainTex, i.uv);
                half4 finalColor = lerp(insideColor, outlineColor, _OutlineWidth);

                return finalColor;
            }
            ENDCG
        }
    }
}
