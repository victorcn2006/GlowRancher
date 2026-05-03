Shader "Supyrb/Lit/Slime_Emission_Responsive"
{
    Properties
    {
        _MainTex ("Albedo (Texture)", 2D) = "white" {}
        _Color("Color Tint", Color) = (1,1,1,1)
        _Brightness ("Brillo Cuerpo", Range(0, 10)) = 1.0 
        
        _Opacity ("Opacity", Range(0,1)) = 1.0

        [Header(Emission)]
        _EmissionMap ("Emission Texture", 2D) = "black" {}
        [HDR]_EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _EmissionStr ("Emission Intensity", Range(0, 10)) = 1.0

        [Header(Rendering)]
        _Offset("Offset", float) = 0
        [Enum(Off,0,Front,1,Back,2)] _Culling ("Cull Mode", Int) = 0 
    }
   
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
        LOD 100
        
        Pass {
            Cull [_Culling] 
            Offset [_Offset], [_Offset]

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness; 

            float _Opacity;

            sampler2D _EmissionMap;
            float4 _EmissionMap_ST;
            fixed4 _EmissionColor;
            float _EmissionStr;
           
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL; 
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed3 worldLight : COLOR0;
            };
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                float3 worldNormal = UnityObjectToWorldNormal(v.normal);
                o.worldLight = ShadeSH9(half4(worldNormal, 1)) * 1.5; 
                
                return o;
            }
           
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                col.rgb *= i.worldLight * _Brightness;

                fixed3 emi = tex2D(_EmissionMap, i.uv).rgb * _EmissionColor.rgb * _EmissionStr;

                col.rgb += emi;

                col.a = _Opacity * _Color.a;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
