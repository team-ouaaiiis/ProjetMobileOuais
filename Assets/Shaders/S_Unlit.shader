Shader "Unlit/ToonHLSL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Normal("Normal", 2D) = "black" {}
		_NormalStrength("Normal", Range(0,1)) = 1
		_Color("Color", Color) = (1,1,1,1)
		_ShadowColor("Shadow Color", Color) = (0.5,0.5,0.5,0.5)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
            sampler2D _Normal;
            float4 _MainTex_ST;
            float4 _LightDir;
			float4 _Color;
			float _NormalStrength;

            v2f vert (appdata v)
            {
                v2f o;
				o.normal = v.normal;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {			
				float3 worldNormal = UnityObjectToWorldNormal(i.normal); //WORLD SPACE NORMAL VECTOR

                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 normalSample = tex2D(_Normal, i.uv);
				
				float3 intensityNorm = float3(worldNormal.x * normalSample.r, worldNormal.y * normalSample.g, worldNormal.z * normalSample.b);
				float dotP = 1 - dot(normalize(_LightDir), intensityNorm); //Dot Product
				//float dotP = 1 - dot(normalize(_LightDir), i.normal.xyz); //Dot Product
				float stepped = step(0.8, dotP); //Step


				float4 result = stepped * _Color * col;
                return result;
            }
            ENDCG
        }
    }
}
