Shader "Unlit/PlayerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EyeTex ("Texture", 2D) = "white" {}
        _NoseTex ("Texture", 2D) = "white" {}
        _MouthTex ("Texture", 2D) = "white" {}
        _SkinTone ("SkinTone", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { 
            "RenderType"="Transparent" 
            "QUEUE"="Transparent"
        }
        LOD 100
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
                float2 uv : TEXCOORD0;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _EyeTex;
            sampler2D _NoseTex;
            sampler2D _MouthTex;

            fixed4 blendTogether(fixed4 base, fixed4 top);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 _SkinTone;
            fixed4 frag (v2f i) : SV_Target
            {
                float4 texCol = tex2D(_MainTex, i.uv) * _SkinTone;
                return texCol;
                //fixed4 eyeCol = tex2D(_EyeTex, i.uv);
                //
                //float4 a = blendTogether(texCol, eyeCol);
                //a = blendTogether(a, tex2D(_NoseTex, i.uv));
                //a = blendTogether(a, tex2D(_MouthTex, i.uv));
                //return a;
            }

            fixed4 blendTogether(fixed4 base, fixed4 top)
            {
                if(top.a >= 1 || base.a <= 0) return top;
                else if(top.a <= 0) return base;

                fixed3 baseCol = fixed3(base.r, base.g, base.b);
                fixed3 topCol = fixed3(top.r, top.g, top.b);
                fixed3 b = baseCol * (1.0 - top.a) + topCol * top.a;

                return float4(b, top.a + base.a * (1 - top.a));
            }

            ENDCG
        }
    }
}
