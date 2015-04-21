Shader "Custom/BlackFill" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform fixed4 _MaskColor;
			
			struct appdata {
		        	float4 vertex   : POSITION; //position of vertex
		        	float3 normal   : NORMAL; //position of vertex
		            float2 texcoord : TEXCOORD0; //1st texture coord
		            float4 col : COLOR; //vertex color
		    };
		        
			struct v2f{
				float4 Pos : SV_POSITION;//position of vertex under view frustrum? 
				float2 texcoord : TEXCOORD0;
				float4 Col : COLOR;
			};
			
			v2f vert(appdata i){
				v2f o; 
		        o.Pos = mul (UNITY_MATRIX_MVP, i.vertex ); 
		        o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, i.texcoord ); 
				return o;
			}
			
			float4 frag(v2f i):COLOR{
				float a = tex2D(_MainTex, i.texcoord).a;
				if(a == 0.0f)
					a = 1.0f;
				else
					a = 0.0f;
				float4 col = a;
				return col;
			}
			ENDCG
			
		}
		
	} 
	FallBack "Diffuse"
}
