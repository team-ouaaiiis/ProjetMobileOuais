Shader "Postprocessing/S_TramageAnimated"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	_NoiseTex("Noise Texture", 2D) = "white" {}
	_NoiseScroll(" NoiseScroll", 2D) = "white" {}
	_Grad("Gradient Texture", 2D) = "white" {}
	_NoiseVector("Noise : Speed (X,Y), Intensity (Z)", Vector) = (1,1,1,1)
		_ScreenSize("Screen Size", vector) = (1,1,1,1)
		_Color("Tint", Color) = (1,1,1,1)
		_Scale("Scale", float) = 1
	}
		SubShader
	{
		Cull Off ZWrite Off ZTest Always

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

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	sampler2D _Grad;
	sampler2D _NoiseScroll;
	sampler2D _NoiseTex;
	float4 _NoiseVector;
	fixed4 _Color;
	fixed4 _TextureSampleAdd;
	float4 _ClipRect;
	float4 _ScreenSize;
	float4 _MainTex_ST;
	float _Scale;

	fixed4 frag(v2f i) : SV_Target
	{
		/*
		float2 scrollUv = i.uv + float2(_Time.y * _NoiseVector.x, _Time.y * _NoiseVector.y);
		float4 noise = tex2D(_NoiseTex, scrollUv);


		half4 col = (tex2D(_MainTex, i.uv * _Scale) + _TextureSampleAdd);
		col.rgb = step(noise.r * _NoiseVector.z, col.r);

		col.rgb *= grad;

		col.a *= step(0.001, col.r);
		*/
		float2 scrollUv = i.uv + float2(_Time.y * _NoiseVector.x, _Time.y * _NoiseVector.y);
		float4 noiseScroll = tex2D(_NoiseScroll, scrollUv);
		fixed4 col = tex2D(_MainTex, i.uv);
		float4 grad = tex2D(_Grad, float2(noiseScroll.r, 0));

		float2 sclNoise = float2(_ScreenSize.x * _Scale, _ScreenSize.y * _Scale);
		fixed4 noise = tex2D(_NoiseTex, (i.uv * sclNoise));

		col.rgb += (step((noiseScroll.r * _NoiseVector.z) + _NoiseVector.w, noise.rgb) * grad) * 0.02;



		return col;


	}
		ENDCG
	}
	}
}
