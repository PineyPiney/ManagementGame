Shader "Unlit/BuildingShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Primary("Primary", Color) = (0, 1, 0, 1)
        _Secondary("Secondary", Color) = (1, 0, 0, 1)
        _Tertiary("Tertiary", Color) = (0, 0, 0, 1)

        _Outline("Outline", Range(0, 0.1)) = 0.02
        [MaterialToggle] _Outlined("Outlined", Float) = 0
        _OutlineColour("Outline Colour", Color) = (1, 1, 1, 1)
    }
    Stencil{
        
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

            bool shouldOutline(v2f i);

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float _Outlined, _Outline;
            fixed4 _Primary, _Secondary, _Tertiary, _OutlineColour;

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 texCol = tex2D(_MainTex, i.uv);

                // If the object should be outlined then check all outlining criteria
                if(_Outlined > 0 && shouldOutline(i)) return _OutlineColour;

                else{
                    if(texCol.a == 0) discard;

                    else if(texCol.x + texCol.y + texCol.z == 0) return float4(0, 0, 0, texCol.a);
                }

                float4 weighted;
                if(texCol.b <= 0){
                    if(texCol.g <= 0) weighted = _Primary;
                    else if(texCol.r <= 0) weighted = _Secondary;
                }
                else if(texCol.r <= 0 && texCol.g <= 0) weighted = _Tertiary;
                else return texCol;

                return float4(weighted.xyz, weighted.w * texCol.a);
            }

            float4 _MainTex_TexelSize;

            // The sprite renderer uses a custom mesh that fits the texture very tightly, 
            // not allowing for much room for outlining.
            bool shouldOutline(v2f i){
                if(_Outline <= 0) return false;

                // Size of outline scaled to texture aspect ratio
                float2 o = float2(_Outline, _Outline * _MainTex_TexelSize.z / _MainTex_TexelSize.w);


                if(abs(0.5 - i.uv.y) > 0.5 - o.y || abs(0.5 - i.uv.x) > 0.5 - o.x) {
                   // return true;
                }

                else if(tex2D(_MainTex, i.uv).a < 0.15){
                    float2 offsets[8] = {
                        float2(0, o.y),
                        o,
                        float2(o.x, 0),
                        float2(o.x, -o.y),
                        float2(0, -o.y),
                        -o,
                        float2(-o.x, 0),
                        float2(-o.x, o.y)
                    };

                    for(uint j = 0; j < 8; j++){
                        float2 uv = clamp(i.uv + offsets[j], float2(0, 0), float2(1, 1));
                        if(tex2D(_MainTex, uv).a != 0){
                            return true;
                        }
                    }
                }
                return false;
            }

            ENDCG
        }
    }
}
