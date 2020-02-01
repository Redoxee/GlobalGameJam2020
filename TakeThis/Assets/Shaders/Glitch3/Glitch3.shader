Shader "Custom/Glitch3"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Speed ("Speed", Float) = 1.
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
		Tags { "Queue" = "Transparent"}
		GrabPass
		{
			"_BackgroundTexture"
		}

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
				float2 grabPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
				o.grabPos = ComputeGrabScreenPos(o.vertex);
                return o;
            }

			half _Speed;
            sampler2D _MainTex;
			sampler2D _BackgroundTexture;

			half2 Noise(half2 p)
			{
				return half2(frac(sin(p.x * 4533.)*536.), frac(sin(p.y*5373.)*483.));
			}

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
				half2 uv = i.uv - 0.5;
				half rep = 100.0;
				half2 id = floor(uv * rep) / rep;
				uv = frac(uv * rep) - .5;
				half2 nid = (Noise(id + frac(_Time.g * .000001))-.5) * 2.;
				
				col.rgb = tex2D(_BackgroundTexture, i.grabPos.xy);
				half f = 1. - length(i.uv - .5);
				col.rg += nid * f *f * f;
				return col;
            }
            ENDCG
        }
    }
}
