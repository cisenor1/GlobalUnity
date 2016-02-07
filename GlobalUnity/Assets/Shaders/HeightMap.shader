Shader "Unlit/HeightMap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MinHeight("Min Height", Float) = 0
		_MaxHeight("Max Height", Float) = 500
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float3 height;
			float _MinHeight;
			float _MaxHeight;
			float scale;
			float displacement;
			
			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				// pseudocode maybe
				height = tex2Dlod(_MainTex, float4(o.uv,0,0));
				scale = _MaxHeight - _MinHeight;
				displacement = _MinHeight - scale;
				//v.vertex.y = height.r;
				v.vertex.y = (height.r * scale) + displacement;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
