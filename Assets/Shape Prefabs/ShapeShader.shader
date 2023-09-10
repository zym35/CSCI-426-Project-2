Shader "Custom/ColoredInteriorBorder"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _BorderColor("Border Color", Color) = (0,0,0,1)
        _BorderThickness("Border Thickness", float) = 0.1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" }

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed4 _BorderColor;
            float _BorderThickness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture and apply the color
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                
                // Calculate the distance to the edge
                float minDistToEdge = min(min(i.uv.x, 1.0 - i.uv.x), min(i.uv.y, 1.0 - i.uv.y));
                
                // Create the border
                float isBorder = step(_BorderThickness, minDistToEdge);
                
                // Combine the colors
                fixed4 finalColor = lerp(_BorderColor, texColor, isBorder);

                return finalColor;
            }
            ENDCG
        }
    }
}
