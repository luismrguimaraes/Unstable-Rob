Shader "Custom/DiscreteLevels" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Levels ("Number of Levels", Float) = 5
        _Color ("Color", Color) = (0, 0, 1, 1) // Default to blue
    }
    
    SubShader {
        Tags { "RenderType"="Opaque" }
        
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            
            sampler2D _MainTex;
            float _Levels;
            fixed4 _Color;
            
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target {
                fixed4 color = tex2D(_MainTex, i.uv);
                
                // Convert color to grayscale
                float luminance = dot(color.rgb, float3(0.299, 0.587, 0.114));
                
                // Calculate discrete levels
                float discreteLevel = floor(luminance * (_Levels - 1)) / (_Levels - 1);
                
                // Map discrete levels to desired colors using the _Color property
                color.rgb = _Color.rgb * discreteLevel;
                
                return color;
            }
            ENDCG
        }
    }
}
