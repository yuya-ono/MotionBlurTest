Shader "Custom/Dual" {
	Properties {
		_Cutoff ("Shadow alpha cutoff", Range(0,1)) = 0.1
		_MainTex ("Texture to blend", 2D) = "black" {}
		_Alpha("Alpha", float) = 1.0
//		_Size("Size", float) = 0.1
		_Color("Color", Color) = (0,0,0,0)
	}
	
	CGINCLUDE

	#include "UnityCG.cginc"
	
	uniform sampler2D _MainTex;
	uniform fixed4 _Color;
//	uniform float _Size;
	uniform float _Alpha;
	uniform float4x4 _SizedMVP;
	
	struct appdata {
		float4 vertex   : POSITION;
	    half2 texcoord : TEXCOORD0;
	    float4 col : COLOR;
	};

	struct v2f{
		fixed4 pos : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		fixed4 col : COLOR;
	};
	
	v2f vert(appdata v){
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord ); 
		o.col = v.col;
		return o;
	}
	
	fixed4 frag(v2f i): COLOR{
		fixed4 col = tex2D(_MainTex, i.texcoord);
		return col;
	}
	
	v2f vertD(appdata v){
		v2f o;
		fixed4 tpos = mul(_SizedMVP, v.vertex);
		
		o.pos = tpos;
		o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord ); 
		o.col = v.col;
		return o;
	}
	//** vertex shader using w to scale **//
//	v2f vertD(appdata v){
//		v2f o;
//		fixed4 tpos = mul(UNITY_MATRIX_MVP, v.vertex);
//		tpos.w *= _Size;
//		o.pos = tpos;
//		o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord ); 
//		o.col = v.col;
//		return o;
//	}
	
	fixed4 fragD(v2f i): COLOR{
		fixed4 col = tex2D(_MainTex, i.texcoord);
		col.a *= _Alpha;
		return col;
	}
	
	ENDCG
		
		
	
	
	
	
	// 2 texture stage GPUs
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
//		Blend One
//		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass
		{	
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
		
		Pass
		{	
//			Blend SrcAlpha OneMinusSrcAlpha
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vertD
			#pragma fragment fragD
			ENDCG
		}
	}
	
		// 1 texture stage GPUs
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Cull Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		Lighting Off

		Pass {
			ColorMaterial AmbientAndDiffuse
			SetTexture [_MainTex] {
				Combine texture * primary DOUBLE, texture * primary
			}
		}
	}

		
		
//		Pass {
//			Cull Off
//			ZWrite Off
//			ZTest LEqual
//			Lighting Off
//			Blend SrcAlpha OneMinusSrcAlpha
//			Fog { Mode Off }
//			
//			CGPROGRAM
//			
//			#pragma vertex vert
//			#pragma fragment frag
//			#include "UnityCG.cginc"
//			
//			uniform sampler2D _MainTex;
//			uniform fixed4 _Color;
//			uniform float _Size;
//			uniform float _Alpha;
//			
//			struct appdata {
//	        	float4 vertex   : POSITION;
//	            half2 texcoord : TEXCOORD0;
//	            float4 col : COLOR;
//	        };
//	        
//			struct v2f{
//				fixed4 pos : SV_POSITION;
//				half2 texcoord : TEXCOORD0;
//				fixed4 col : COLOR;
//			};
//			
//			v2f vert(appdata v){
//				v2f o;
//				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord ); 
//				o.col = v.col;
//				return o;
//			}
//			
//			fixed4 frag(v2f i): COLOR{
//				fixed4 col = tex2D(_MainTex, i.texcoord);
//				return col;
//			}
//			ENDCG
//
//		}


//		Pass {
//			Name "Caster"
//			Tags { "LightMode"="ShadowCaster" }
//			Offset 1, 1
//			
//			Fog { Mode Off }
//			ZWrite On
//			ZTest LEqual
//			Cull Off
//			Lighting Off
//
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#pragma multi_compile_shadowcaster
//			#pragma fragmentoption ARB_precision_hint_fastest
//			#include "UnityCG.cginc"
//			struct v2f { 
//				V2F_SHADOW_CASTER;
//				float2  uv : TEXCOORD1;
//			};
//
//			uniform float4 _MainTex_ST;
//
//			v2f vert (appdata_base v) {
//				v2f o;
//				TRANSFER_SHADOW_CASTER(o)
//				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
//				return o;
//			}
//
//			uniform sampler2D _MainTex;
//			uniform fixed _Cutoff;
//
//			float4 frag (v2f i) : COLOR {
//				fixed4 texcol = tex2D(_MainTex, i.uv);
//				clip(texcol.a - _Cutoff);
//				SHADOW_CASTER_FRAGMENT(i)
//			}
//			ENDCG
//		}

}