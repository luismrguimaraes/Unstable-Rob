Shader "Custom/DiscreteLevels"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _PixelateToggle ("Apply Pixelation", Float) = 0.0
        _PixelSize ("Pixel Size", Float) = 10.0
        _Levels ("Number of Levels", Float) = 5
        _Color ("Color", Color) = (0, 0, 1, 1) // Default to blue
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        
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
            float _PixelSize;
            float _Levels;
            fixed4 _Color;
            float2 _MainTex_TexelSize;
            float _PixelateToggle;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                
                // Pixelation
                if (_PixelateToggle > 0.5)
                {
                    uv *= _PixelSize;
                    uv = floor(uv) / _PixelSize;
                }
                
                // Discrete Levels
                fixed4 color = tex2D(_MainTex, uv);
                float luminance = dot(color.rgb, float3(0.299, 0.587, 0.114));
                float discreteLevel = floor(luminance * (_Levels - 1)) / (_Levels - 1);
                color.rgb = _Color.rgb * discreteLevel;
                
                return color;
            }
            ENDCG
        }
    }
}
