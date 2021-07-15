Shader "Hidden/learnShader_uv_turn"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                float angle = _Time.y;
                float2 uv = (0,0);

                i.uv -= float2(0.5,0.5);//计算前偏移 让旋转中心点在（0,0）

                if(length(i.uv) > 0.5)
                {
                    return fixed4(0,0,0,0);
                }

                uv.x = i.uv.x * cos(angle) + i.uv.y*sin(angle);
                uv.y = i.uv.y * cos(angle) - i.uv.x*sin(angle);

                uv += float2(0.5,0.5);//计算后偏移回去

                fixed4 col = tex2D(_MainTex, uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                return col;
            }
            ENDCG
        }
    }
}
