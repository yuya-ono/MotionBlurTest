Shader "Custom/BlurTest" {
	Properties {
		//_CurMV
		//_PrevMV
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VelocityTex ("Base (RGB)", 2D) = "white" {}
//		_CurMV("Current MVP", float4x4) = 0
//		_PrevMV("Previous MVP", float4x4) = 0
		
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
			uniform sampler2D _VelocityTex;
//			uniform float4x4 _CurMV;
//			uniform float4x4 _PrevMV;
			
			struct appdata {
		        	float4 vertex   : POSITION; //position of vertex
		        	float3 normal   : NORMAL; //position of vertex
		            float2 texcoord : TEXCOORD0; //1st texture coord
		            float4 col : COLOR; //vertex color
		    };
		        
			struct v2f{
				float4 Pos : SV_POSITION;//position of vertex under view frustrum? 
//				fixed4 vPos : TEXCOORD0; //texture coord for velocity map
//				fixed3 Velocity : TEXCOORD1;
				float2 texcoord : TEXCOORD0;
				float4 Col : COLOR;
			};

			v2f vert(appdata i){
//				v2f o;
//				fixed4 curP  = mul(_CurMV,i.vertex);//current vertex position in View Coordinate
//				fixed4 prevP = mul(_PrevMV,i.vertex);//last frame vertex position in View Coordinate
//				fixed3 vel = curP.xyz - prevP.xyz; //velocity of vertex in View Coordinate
//				fixed3 n = mul((float3x3)_CurMV,i.normal); //normal Vector of current vertex in View Coordinate
				
//				fixed flag = dot(vel, n) >= 0; //did vertex move toward the velocity's direction??
//				fixed4 pos = flag ? curP : prevP; //YES => use current vertex pos, NO => use previous vertex pos
//				
//				o.Pos = pos;
//				o.Pos = pos; //now you have vertex's position(float4) with velocity, in Projection coordinate
//				
//				//then create velocity map from the position
////				o.vPos.xy = o.Pos.xy;
////				o.vPos.w  = o.Pos.w;
//				o.vPos.xy = o.Pos.xy + o.Pos.w;
//				o.vPos.w  = 2.0f * o.Pos.w;
//				
//				//speed under texture coordinate
//				curP.xyz /= curP.w;
//			  	prevP.xyz /= prevP.w;
//				o.Velocity = (curP.xyz - prevP.xyz)*0.5f;
				v2f o; 
		        o.Pos = mul (UNITY_MATRIX_MVP, i.vertex); 
		        o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, i.texcoord ); 
				return o;
			}
			
			float4 frag(v2f i):COLOR{
//				const float samples = 16;
				float4 col = 0;
				float4 vel = 0;
				vel = tex2D(_VelocityTex, i.texcoord);
//				vel.xy -= 0.25f;
//				vel.xy += 0.5f;
//				vel.xy -= 0.5f;
//				vel.xy *= 10.0f;
//				vel.xy = float2(0,0.1);
//				vel.xy *= 100;
//				if(vel.x == 0.0f){
//				 	vel.x = 1;
//				 }else{
//				 	vel.x = -2.0f;
//				 }
//				if(vel.y == 0.0f){
//				 	vel.y = 1;
//				 }else{
//				 	vel.y = 2.0f;
//				 }
//				vel.xy = float2(-0.5f,-0.5f);
//				vel.x -= 0.5f;
//				vel.y -= 0.5f;
//				vel.xy -= float2(0.25f,0.25f);
//				vel = fixed4(3,0,0,0);
//				float2 dir = float2(vel.x,vel.y);
//				fixed2 dir = fixed2(0.5f,0.5f);
//				col = tex2D(_MainTex, i.texcoord + dir*0.5f);
//				col = fixed4(vel.x,vel.y,0,1);
				col = tex2D(_MainTex, i.texcoord);
				col += tex2D(_MainTex, i.texcoord + vel.xy * 0.1f);
//				col += tex2D(_MainTex, i.texcoord + vel.xy * 0.2f * 2);

//				vel = fixed4(vel.x,vel.y,0,1);
//				vel += tex2D(_MainTex, i.texcoord) * 0.3f;
//				dir = fixed2(-1,-1);
//				col = tex2D(_MainTex, i.texcoord);
//				for ( float j=1; j<5; j++){
//			 		col += tex2D(_MainTex, i.texcoord + j * vel.xy * 0.1f );
//			  	}
//			  	col /= 5;
//				return col;
			  	return vel;
//				return fixed4(vel.x,0,0,1);
//				return fixed4(vel.x,vel.y,0,1);
				
			}
			ENDCG
		}
	} 
	
	FallBack "Diffuse"
}








//Shader "Custom/BlurTest" {
//	Properties {
//		//_CurMV
//		//_PrevMV
//		_MainTex ("Base (RGB)", 2D) = "white" {}
////		_CurMV("Current MVP", float4x4) = 0
////		_PrevMV("Previous MVP", float4x4) = 0
//		
//	}
//	
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
//		
//		Pass {
//		
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#include "UnityCG.cginc"
//
//			uniform sampler2D _MainTex;
//			uniform float4x4 _CurMV;
//			uniform float4x4 _PrevMV;
//			
//			struct appdata {
//		        	float4 vertex   : POSITION; //position of vertex
//		        	float3 normal   : NORMAL; //position of vertex
//		            half2 texcoord : TEXCOORD0; //1st texture coord
//		            float4 col : COLOR; //vertex color
//		    };
//		        
//			struct v2f{
//				fixed4 Pos : SV_POSITION;//position of vertex under view frustrum? 
//				fixed4 vPos : TEXCOORD0; //texture coord for velocity map
//				fixed3 Velocity : TEXCOORD1;
//				half2 texcoord : TEXCOORD2;
//				fixed4 Col : COLOR;
//			};
//
//			v2f vert(appdata i){
//				v2f o;
//				fixed4 curP  = mul(_CurMV,i.vertex);//current vertex position in View Coordinate
//				fixed4 prevP = mul(_PrevMV,i.vertex);//last frame vertex position in View Coordinate
//				fixed3 vel = curP.xyz - prevP.xyz; //velocity of vertex in View Coordinate
//				fixed3 n = mul((float3x3)_CurMV,i.normal); //normal Vector of current vertex in View Coordinate
//				
//				fixed flag = dot(vel, n) >= 0; //did vertex move toward the velocity's direction??
//				fixed4 pos = flag ? curP : prevP; //YES => use current vertex pos, NO => use previous vertex pos
//				
//				o.Pos = pos; //now you have vertex's position(float4) with velocity, in Projection coordinate
//				
//				//then create velocity map from the position
////				o.vPos.xy = o.Pos.xy;
////				o.vPos.w  = o.Pos.w;
//				o.vPos.xy = o.Pos.xy + o.Pos.w;
//				o.vPos.w  = 2.0f * o.Pos.w;
//				
//				//speed under texture coordinate
//				curP.xyz /= curP.w;
//			  	prevP.xyz /= prevP.w;
//				o.Velocity = (curP.xyz - prevP.xyz)*0.5f;
//				
//				o.texcoord = i.texcoord;
//				return o;
//			}
//			
//			fixed4 frag(v2f i):COLOR{
//				fixed4 col;
//				//by dividing x, y, z by w, you can get screen position between -1<x<1,-1<y<1,0<z<1
//				//this [tex] is the position of each vertex in the screen
//				float2 tex = i.vPos.xy/i.vPos.w; 
//				float2 velocity = i.Velocity.xy * 0.3f;
//				
////				for ( float i=0; i<10; i++){
////			  		float t = (i+1)/samples;
////			 		Out += tex2D(_MainTex, tex + t *velocity);
////			  	}
//
//				col = tex2D(_MainTex, i.texcoord);
////				col = tex2D(_MainTex, tex + velocity);
//				return col;
//				
//			}
//			ENDCG
//		}
//	} 
//	
//	FallBack "Diffuse"
//}
