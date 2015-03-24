Shader "Custom/ExplosionBlur" {

	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Power ("Base (RGB)", Range(0,1)) = 0
		_CenterTexelX ("Explosion Center X", Range(0,1)) = 0.5
		_CenterTexelY ("Explosion Center Y", Range(0,1)) = 0.5
	}
	
	CGINCLUDE
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform float _Power;
			uniform float _CenterTexelX;
			uniform float _CenterTexelY;
			
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
				
//				float2 v = normalize( i.texcoord - float2(_CenterTexelX, _CenterTexelY) ) * float2(_MainTex_TexelSize.x,_MainTex_TexelSize.y);
				float2 v = float2(_CenterTexelX, _CenterTexelY) - i.texcoord;
				float size = length( v );
				v = normalize( v );
				
				v *= size * _Power;
//				v *= size * _Power; //the more vector is bigger, the more blur is effected
				//texel size
//				float offX = _MainTex_TexelSize.x*2.0f;
//				float offY = _MainTex_TexelSize.y*2.0f;
				float4 col = tex2D(_MainTex, i.texcoord + v ) * 0.4f;
				float4 col1 = tex2D(_MainTex, i.texcoord + v * 2 ) * 0.3f;
				float4 col2 = tex2D(_MainTex, i.texcoord + v * 3 ) * 0.2f;
				float4 col3 = tex2D(_MainTex, i.texcoord + v * 4 ) * 0.1f;
				
				col = col + col1 + col2 + col3;
				return col;
			}
	
	ENDCG
	
	
	SubShader {
		ZTest Off Cull Off ZWrite Off Blend Off
	  	Fog { Mode off }  
		
		Pass
		{
			ZTest Always
			Cull Off
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		
	} 
	
	FallBack "Diffuse"
}
