Shader "Custom/BlurFilterY" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	CGINCLUDE
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
//			sampler2D _GrabTexture;
			uniform half4 _MainTex_TexelSize;
			
			
			struct appdata{
				float4 pos :POSITION;
				float2 texcoord: TEXCOORD;
//				float2 uv[4]: TEXCOORD;
				float4 col :COLOR;
			};
			struct v2f{
				float4 pos :POSITION;
				float2 texcoord: TEXCOORD;
				float4 col :COLOR;
			};

//			v2f vertX(appdata i){
//				v2f o;
//				o.pos = mul (UNITY_MATRIX_MVP, i.pos ); 
//		        o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, i.texcoord ); 
//				return o;
//			}
//			float4 fragX(v2f i):COLOR{
//				//texel size
//				float offX = _MainTex_TexelSize.x*2.0f;
////				float offY = _MainTex_TexelSize.y*2.0f;
//				float4 col = tex2D(_MainTex, i.texcoord) * 0.1f;
//				float4 col1 = tex2D(_MainTex, i.texcoord + float2(offX * 1.0f,0)) * 0.1f;
//				float4 col2 = tex2D(_MainTex, i.texcoord + float2(offX * 2.0f,0)) * 0.1f;
//				float4 col3 = tex2D(_MainTex, i.texcoord + float2(offX * 3.0f,0)) * 0.1f;
//				float4 col4 = tex2D(_MainTex, i.texcoord + float2(offX * 4.0f,0)) * 0.1f;
//				float4 col5 = tex2D(_MainTex, i.texcoord + float2(offX * 5.0f,0)) * 0.05f;
//				
//				float4 col6 = tex2D(_MainTex, i.texcoord + float2(offX * -1.0f,0)) * 0.1f;
//				float4 col7 = tex2D(_MainTex, i.texcoord + float2(offX * -2.0f,0)) * 0.1f;
//				float4 col8 = tex2D(_MainTex, i.texcoord + float2(offX * -3.0f,0)) * 0.1f;
//				float4 col9 = tex2D(_MainTex, i.texcoord + float2(offX * -4.0f,0)) * 0.1f;
//				float4 col10 = tex2D(_MainTex, i.texcoord + float2(offX * -5.0f,0)) * 0.05f;
//				
//				col = col + col1 + col2 + col3 + col4 + col5
//				 		  + col6 + col7 + col8 + col9 + col10; 
//				return col;
//			}
			
			v2f vertY(appdata i){
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, i.pos ); 
		        o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, i.texcoord ); 
				return o;
			}
			float4 fragY(v2f i):COLOR{
				//texel size
//				float offX = _MainTex_TexelSize.x*2.0f;
				float offY = _MainTex_TexelSize.y*2.0f;
				float4 col = tex2D(_MainTex, i.texcoord) * 0.1f;
				float4 col1 = tex2D(_MainTex, i.texcoord + float2(0, offY * 1.0f)) * 0.1f;
				float4 col2 = tex2D(_MainTex, i.texcoord + float2(0, offY * 2.0f)) * 0.1f;
				float4 col3 = tex2D(_MainTex, i.texcoord + float2(0, offY * 3.0f)) * 0.1f;
				float4 col4 = tex2D(_MainTex, i.texcoord + float2(0, offY * 4.0f)) * 0.1f;
				float4 col5 = tex2D(_MainTex, i.texcoord + float2(0, offY * 5.0f)) * 0.05f;
				
				float4 col6 = tex2D(_MainTex, i.texcoord + float2(0, offY * -1.0f)) * 0.1f;
				float4 col7 = tex2D(_MainTex, i.texcoord + float2(0, offY * -2.0f)) * 0.1f;
				float4 col8 = tex2D(_MainTex, i.texcoord + float2(0, offY * -3.0f)) * 0.1f;
				float4 col9 = tex2D(_MainTex, i.texcoord + float2(0, offY * -4.0f)) * 0.1f;
				float4 col10 = tex2D(_MainTex, i.texcoord + float2(0, offY * -5.0f)) * 0.05f;
				
				col = col + col1 + col2 + col3 + col4 + col5
				 		  + col6 + col7 + col8 + col9 + col10; 
				
				return col;
			}
//			float4 fragY(v2f i):COLOR{
//				//texel size
////				float offX = _MainTex_TexelSize.x*2.0f;
//				float offY = _MainTex_TexelSize.y*2.0f;
//				float4 col = tex2D(_MainTex, i.texcoord) * 0.1f;
//				float4 col1 = tex2D(_MainTex, i.texcoord + float2(0, offY * 1.0f)) * 0.1f;
//				float4 col2 = tex2D(_MainTex, i.texcoord + float2(0, offY * 2.0f)) * 0.1f;
//				float4 col3 = tex2D(_MainTex, i.texcoord + float2(0, offY * 3.0f)) * 0.1f;
//				float4 col4 = tex2D(_MainTex, i.texcoord + float2(0, offY * 4.0f)) * 0.1f;
//				float4 col5 = tex2D(_MainTex, i.texcoord + float2(0, offY * 5.0f)) * 0.05f;
//				
//				float4 col6 = tex2D(_MainTex, i.texcoord + float2(0, offY * -1.0f)) * 0.1f;
//				float4 col7 = tex2D(_MainTex, i.texcoord + float2(0, offY * -2.0f)) * 0.1f;
//				float4 col8 = tex2D(_MainTex, i.texcoord + float2(0, offY * -3.0f)) * 0.1f;
//				float4 col9 = tex2D(_MainTex, i.texcoord + float2(0, offY * -4.0f)) * 0.1f;
//				float4 col10 = tex2D(_MainTex, i.texcoord + float2(0, offY * -5.0f)) * 0.05f;
//				
//				col = col + col1 + col2 + col3 + col4 + col5
//				 		  + col6 + col7 + col8 + col9 + col10; 
//				
//				return col;
//			}
	
	ENDCG
	
	
	SubShader {
		ZTest Off Cull Off ZWrite Off Blend Off
	  	Fog { Mode off }  
		
//		Pass
//		{
//			ZTest Always
//			Cull Off
//			
//			CGPROGRAM
//			#pragma vertex vertX
//			#pragma fragment fragX
//			ENDCG
//		}
//		
//        GrabPass { }
            
		Pass
		{
			ZTest Always
			Cull Off
			CGPROGRAM
			#pragma vertex vertY
			#pragma fragment fragY
			ENDCG
		}
	} 
	
	FallBack "Diffuse"
}
