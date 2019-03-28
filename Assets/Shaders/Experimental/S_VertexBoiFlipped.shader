// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/S_VertexBoiFlipped"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Tiling ("Tiling", float) = 0.001
		_Strength ("Strength", float) = 1
		_ColorA ("ColorA", Color) = (1,1,1,1)
		_CircleTex ("Circle Tex", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
        Cull Front
		ZWrite Off
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
				float4 normal : NORMAL;
				
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float4 normal : NORMAL;
				float4 scrPos : TEXCOORD1;

            };

            sampler2D _MainTex;
            sampler2D _CircleTex;
            float4 _MainTex_ST;
			float _Tiling;
			float _Strength;
			float4 _ColorA;
			float4 _ColorB;

            v2f vert (appdata v)
            {
                v2f o;
				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 modulo = fmod(worldPos, _Tiling);
				v.vertex.xyz += modulo * _Strength;
				v.vertex.xyz += v.normal * 0.008;
				o.normal = v.normal;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(o.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 tram = tex2D(_CircleTex, (i.scrPos.xy/i.scrPos.w) * float2(1080,1920) * 0.05);
				fixed4 color = i.normal;
				color.a = step( (sin(((_Time.y * 2) + i.uv.x) ) + 1) / 2, tram.r);
                return color;
            }
            ENDCG
        }
    }    
}
