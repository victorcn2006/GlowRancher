Shader "Supyrb/Lit/Texture_Flat_Responsive_Bright"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color Tint", Color) = (1,1,1,1)
        _Brightness ("Brillo Manual", Range(0, 10)) = 1.0 // Rango aumentado a 10
       
        [Header(Stencil)]
        _Stencil ("Stencil ID", Float) = 0
        _ReadMask ("ReadMask", Int) = 255
        _WriteMask ("WriteMask", Int) = 255
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Comparison", Int) = 0
        [Enum(UnityEngine.Rendering.StencilOp)] _StencilOp ("Stencil Operation", Int) = 0
       
        [Header(Rendering)]
        _Offset("Offset", float) = 0
        [Enum(Off,0,Front,1,Back,2)] _Culling ("Cull Mode", Int) = 0 
    }
   
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
        LOD 100
        
        Stencil {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
        }

        Pass {
            Cull [Off] 
            Offset [_Offset], [_Offset]
           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Brightness; 
           
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
                // Obtenemos la luz ambiental
                fixed3 ambient = ShadeSH9(half4(worldNormal, 1));
                
                // Amplificamos la base de la luz ambiental para que no sea tan oscura
                o.worldLight = ambient * 1.5; 
                
                return o;
            }
           
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                
                // Multiplicamos por la luz ambiental y luego por el Brillo Manual
                col.rgb *= i.worldLight;
                
                return col * _Brightness; 
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
