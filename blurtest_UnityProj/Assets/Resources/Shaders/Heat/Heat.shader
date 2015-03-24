Shader "Custom/Heat" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_HeatMapTex ("Heat Map", 2D) = "white" {}
	}

	SubShader {
		Tags { "RenderType"="Heat" }
		LOD 200
		
		Pass
		{
			Blend One Zero
//			Blend Zero One
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			sampler2D _HeatMapTex;
			uniform float _AddRate;
			
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
				float4 hcol = tex2D(_HeatMapTex, i.texcoord);
				float mx = hcol.r * 0.2 * hcol.a;
				float my = hcol.g * 0.2 * hcol.a;
//				float2 sp = float2(i.texcoord.x, i.texcoord.y);
				float2 sp = float2(i.texcoord.x + mx, i.texcoord.y + my);
				float4 col  = tex2D(_MainTex, sp);
				
				return col;
			}
			ENDCG
		}
		
	} 
	FallBack "Diffuse"
}
