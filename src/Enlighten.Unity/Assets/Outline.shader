Shader "Unlit/Outline"
{
    Properties
    {
        _Dimensions ("Dimensions", Vector) = (10,10,0,0)
        _OutlinePos ("Outline Position", Float) = 1
        _Roundness ("Roundness", Float) = 3
        _Thin ("Thin-ness", Float) = 4
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Blend OneMinusDstColor One, OneMinusDstColor OneMinusSrcAlpha

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

            float4 _Dimensions;
            float _OutlinePos;
            float _Roundness;
            float _Thin;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
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
                float2 res = _Dimensions.xy;
                float2 pixelCoord = i.uv * res;

                float2 toCenter = pixelCoord - res / 2;
                float dist = -sdRoundedBox(toCenter, res / 2, _Roundness + _OutlinePos);

                float2 uv = i.uv;
                float time = _Time.y * 1.2;
                float angle = time * 0.1;
                
                uv = float2(
                (uv.x * cos(angle)) - (uv.y * sin(angle)),
                (uv.x * sin(angle)) + (uv.y * cos(angle))
                );
                
                float centerDot = dot(normalize(toCenter), float2(cos(angle), sin(angle)));
                centerDot = centerDot * 0.5 + 0.5;

                float f = abs(dist - _OutlinePos) * _Thin;
                f = smoothstep(0, 1, saturate(pow(1 - f, 5)));
                f *= centerDot * 1.5;
                
                uv.x += sin(time * 0.75) * 0.1;
                uv.y *= cos(angle) + sin(uv.x);

                float3 col = f * (clamp(cos(float3(5,uv.y * 0.5 + 0.5,sin(time) + 2.) * uv.x * 0.8 + time), 0, 1) + 1);

                return float4(col,1.0);
            }
            ENDCG
        }
    }
}
