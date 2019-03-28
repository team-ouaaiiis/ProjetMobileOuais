// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/S_VertexBoi"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Tiling ("Tiling", float) = 0.001
		_Strength ("Strength", float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Cull Back

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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Tiling;
			float _Strength;

            v2f vert (appdata v)
            {
                v2f o;

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 modulo = fmod(worldPos, _Tiling);
				v.vertex.xyz += modulo * _Strength;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
				col.rgb *= ( dot(i.normal, float3(30,30,30)) + 1 )/ 2;

                return col;
            }
            ENDCG
        }
    }
}
