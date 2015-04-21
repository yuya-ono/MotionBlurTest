Shader "Custom/AddFilter" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_AdditiveTex ("Additive (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _AdditiveTex;
			struct appdata{
				float4 pos :POSITION;
				float2 texcoord: TEXCOORD;
				float4 col :COLOR;
			};
			struct v2f{
				float4 pos :POSITION;
				float2 texcoord: TEXCOORD;
				float4 col :COLOR;
			};

			v2f vert(appdata i){
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, i.pos ); 
		        o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, i.texcoord ); 
				return o;
			}
			float4 frag(v2f i):COLOR{
				//get next pixels
				float4 col  = tex2D(_MainTex, i.texcoord);
				float4 col1 = tex2D(_AdditiveTex, i.texcoord);
				col += col1;
				return col;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
