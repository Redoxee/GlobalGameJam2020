Shader "Custom/Glitch1"
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

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
				half2 uv = i.uv - 0.5;
				half rep = 10.0;
//				half2 id = floor(uv * rep) / rep;
//				uv = frac(uv * rep) - .5;
				i.grabPos.x += sin(i.grabPos.y * 500. + _Time.g * _Speed) * .015 * (1. - length(uv));
                col.rgb = tex2D(_BackgroundTexture, i.grabPos.xy);
                return col;
            }
            ENDCG
        }
    }
}
