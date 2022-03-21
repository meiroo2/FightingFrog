﻿Shader "vdev/FX/Wobble (Multiply)"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Intensity("Intensity", Range(0.0, 360.0)) = 1.0
		_Speed("Speed", Range(0, 3)) = 0.0
	}
		SubShader
		{
			// No culling or depth
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
				uniform float4 _Color;
				float _Intensity;
				int _Speed;
				const float PI = 3.14;

				fixed4 frag(v2f i) : SV_Target
				{
					int speed = round(_Speed);
					float sine = sin((i.uv.y + abs(_Time[speed]))* _Intensity);
					i.uv.x += sine * 0.01;

					fixed4 col = tex2D(_MainTex, i.uv);
					col *= _Color;
					return col;
				}
			ENDCG
		}
		}
}
