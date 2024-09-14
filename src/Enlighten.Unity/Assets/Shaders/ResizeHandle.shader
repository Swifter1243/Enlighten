Shader "Unlit/ResizeHandle"
{
    Properties
    {
        _Inset ("Inset", Float) = 1
        _Width ("Width", Float) = 1
        _Roundness ("Roundness", Float) = 1
        _Blur ("Blur", Float) = 0
        _EdgeBlur ("Edge Blur", Float) = 0
        _Color ("Color", Color) = (1,1,1)
    }
    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent+1"
        }
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Inset;
            float _Roundness;
            float _Width;
            float _Blur;
            float _EdgeBlur;
            float3 _Color;

            float sdRoundedBox(float2 p, float2 b, float4 r )
            {
                r.xy = (p.x>0.0)?r.xy : r.zw;
                r.x  = (p.y>0.0)?r.x  : r.y;
                float2 q = abs(p)-b+r.x;
                return min(max(q.x,q.y),0.0) + length(max(q,0.0)) - r.x;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float f = 0;

                float2 uv = i.uv * _Inset;
                float d = sdRoundedBox(uv, 1, _Roundness);
                f += 1 - smoothstep(_Width - _Blur, _Width, abs(d));

                float edgeDist = min(i.uv.x, i.uv.y);
                f *= smoothstep(0, _EdgeBlur, edgeDist);

                return float4(f * _Color, f);
            }
            ENDCG
        }
    }
}
