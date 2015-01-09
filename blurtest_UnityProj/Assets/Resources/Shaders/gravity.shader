Shader "Custom/gravity" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
	
	Pass
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		sampler2D _MainTex;
		
		struct Input{
		
		}
		struct v2f{
		
		}
		
		v2f vert(Input i){
			v2f o;
			return o;
		}
		void frag (v2f i) {
			float4 o;
			return o;
		}
		ENDCG
	}
		
	} 
	FallBack "Diffuse"
}
