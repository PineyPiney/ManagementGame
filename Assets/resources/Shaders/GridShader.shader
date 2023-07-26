Shader "Unlit/GridShader"
{
    Properties
    {
        _Color("Color", Color) = (0, 0, 0, 1)
        _Size("Size", Range(0.001, 100)) = 1
        _Thickness("Thickness", Range(0, 1)) = 0.01
        _OuterThickness("OuterThickness", Range(0, 1)) = 0.02
        _Offset("Offset", Vector) = (0, 0, 0, 0)
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
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 worldPos : TEXCOORD0;
            };

            float checkAxis(float val);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 _Color;
            float _Size, _Thickness, _OuterThickness;
            float2 _Offset;
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 p = i.worldPos + (_Offset * _Size);
                float dist = min(checkAxis(p.x), checkAxis(p.y));
                
                float4 col;
                if(dist < _OuterThickness) {
                    col = _Color;
                    if(dist > _Thickness){
                        col.a *= ((_OuterThickness - dist) / (_OuterThickness - _Thickness));
                    }
                }
                else discard;
                
                return col;
            }

            float checkAxis(float val){
                return abs((_Size / 2) - (abs(val) % _Size));
            }

            ENDCG
        }
    }
}
