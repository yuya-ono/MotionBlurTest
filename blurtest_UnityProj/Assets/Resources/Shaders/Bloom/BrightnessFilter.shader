Shader "Custom/BrightnessFilter" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
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
			struct appdata{
				float4 pos :POSITION;
				float4 normal: NORMAL;
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
				float4 col = tex2D(_MainTex, i.texcoord);
				float y = 0.299f * col.r + 0.587 * col.g + 0.114 * col.b;
				col = float4(y,y,y,1.0f);
				return col;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
