Shader "Unlit/PanelBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Dimensions ("Dimensions", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv * _Dimensions.xy;
                float time = _Time.y * 0.2;
                
                float angle = time * 0.1;
                
                uv += 1.;
                
                uv = float2(
                (uv.x * cos(angle)) - (uv.y * sin(angle)),
                (uv.x * sin(angle)) + (uv.y * cos(angle))
                );
                
                uv.x += sin(time * 0.75) * 0.1;
                uv.y *= cos(angle) + sin(uv.x);
                
                float2 currPoint = uv * (5 + cos(time * 0.3));
                float2 flooredPoint = floor(currPoint);
                
                float dist = 1.;
                
                float offset = (1. - pow(1. - (sin(time * 0.3) * 0.5 + 0.5), 1.)) * 0.5 * 0.5 + 0.25;
                
                for (int x = 0; x <= 1; x++) {
                    for (int y = 0; y <= 1; y++) {
                        float2 p = flooredPoint + float2(x, y);
                        p.x += p.y % 2. < 0.5 ? 0. : offset;
                        dist = min(dist, length(currPoint - p));
                    }
                }
                
                float f = dist * 1.6 + abs(cos(_Time)) * 0.2;
                f = smoothstep(0,2,f);
                f = saturate(f);
                float3 col = f * cos(float3(3,uv.y * 0.5,sin(time * 0.2) + 2.) * uv.x * 0.4 + time);
                col += i.uv.x * 0.2;

                return float4(col,1.0);
            }
            ENDCG
        }
    }
}
