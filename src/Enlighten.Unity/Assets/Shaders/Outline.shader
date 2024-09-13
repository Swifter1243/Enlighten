Shader "Unlit/Outline"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OutlinePos ("Outline Position", Float) = 1
        _Roundness ("Roundness", Float) = 3
        _Thin ("Thin-ness", Float) = 4
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 vertex : SV_POSITION;
                float2 localPos : TEXCOORD1;
                float2 uv : TEXCOORD2;
            };

            float _OutlinePos;
            float _Roundness;
            float _Thin;

            float2 _TopRightCorner;

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float2 uv = v.uv * 2 - 1;
                o.localPos = uv * _TopRightCorner;
                o.uv = v.uv;
                return o;
            }

            float sdRoundedBox( in float2 p, in float2 b, in float4 r )
            {
                r.xy = (p.x>0.0)?r.xy : r.zw;
                r.x  = (p.y>0.0)?r.x  : r.y;
                float2 q = abs(p)-b+r.x;
                return min(max(q.x,q.y),0.0) + length(max(q,0.0)) - r.x;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float dist = -sdRoundedBox(i.localPos, _TopRightCorner, _Roundness + _OutlinePos);

                float2 uv = i.uv;
                float time = _Time.y * 1.2;
                float angle = time * 0.1;

                uv = float2(
                (uv.x * cos(angle)) - (uv.y * sin(angle)),
                (uv.x * sin(angle)) + (uv.y * cos(angle))
                );

                float centerDot = dot(normalize(i.localPos), float2(cos(angle), sin(angle)));
                centerDot = centerDot * 0.5 + 0.5;

                float f = abs(dist - _OutlinePos) * _Thin;
                f = smoothstep(0, 1, saturate(pow(1 - f, 5)));
                f *= centerDot * 1.5;

                uv.x += sin(time * 0.75) * 0.1;
                uv.y *= cos(angle) + sin(uv.x);

                float3 col = f * (clamp(cos(float3(5,uv.y * 0.5 + 0.5,sin(time) + 2.) * uv.x * 0.8 + time), 0, 1) + 1);

                return float4(col, f);
            }
            ENDCG
        }
    }
}
