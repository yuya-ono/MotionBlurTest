Shader "Custom/BlurObject" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
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
			uniform float4x4 _CurMV;
			uniform float4x4 _PrevMV;
			uniform float4x4 _CurP;
			
			struct appdata {
		        	float4 vertex   : POSITION; //position of vertex
		        	float3 normal   : NORMAL; //position of vertex
		            float2 texcoord : TEXCOORD0; //1st texture coord
		            float4 col : COLOR; //vertex color
		    };
		        
			struct v2f{
				float4 Pos : SV_POSITION;//position of vertex under view frustrum? 
				float4 vPos : TEXCOORD0; //texture coord for velocity map
				float3 Velocity : TEXCOORD1;
				float2 texcoord : TEXCOORD2;
				float4 Col : COLOR;
			};

			v2f vert(appdata i){
				v2f o;
				float4 curP  = mul(_CurMV,i.vertex);//current vertex position in View Coordinate(MV)
				float4 prevP = mul(_PrevMV,i.vertex);//last frame vertex position in View Coordinate(MV)
				float3 vel = curP.xyz - prevP.xyz; //velocity of vertex in View Coordinate
				float3 n = mul((float3x3)_CurMV,i.normal); //normal Vector of current vertex in View Coordinate
				
				float flag = dot(vel, n) >= 0; //did vertex move toward the velocity's direction??
				float4 pos = flag ? curP : prevP; //YES => use current vertex pos, NO => use previous vertex pos
				
				//then create velocity map from the position
//				o.vPos.xy = o.Pos.xy + o.Pos.w;
//				o.vPos.w  = 2.0f * o.Pos.w;
				o.vPos.xy = pos.xy;
				o.vPos.w  = pos.w;
				
				//speed under texture coordinate
				curP.xyz /= curP.w;
			  	prevP.xyz /= prevP.w;
				o.Velocity = (curP.xyz - prevP.xyz)*0.5f;
				
				//tes
//				float4 P = mul(_CurMV,i.vertex);
//				o.Pos = P;
//				o.Pos = mul (UNITY_MATRIX_MVP, i.vertex ); 
				
				pos = mul(_CurP,pos);
				o.Pos = pos; //now you have vertex's position(float4) with velocity, in view coordinate(MV)
				
				
				o.texcoord = i.texcoord;
				return o;
			}
			
			float4 frag(v2f i):COLOR{
				float4 col;
				//by dividing x, y, z by w, you can get screen position between -1<x<1,-1<y<1,0<z<1
				//this [tex] is the position of each vertex in the screen
				float2 tex = i.vPos.xy/i.vPos.w; 
				float2 velocity = i.Velocity.xy*100;
				col = float4(velocity.x+0.5f, velocity.y+0.5f, 0,1);
//				col = float4(velocity.y+0.5f, velocity.x+0.5f, 0,1);
//				col = float4(velocity.x*10000, 0, 0,1);
//				col = float4(0, velocity.y*10000, 0,1);
				return col;
				
			}
			ENDCG
		}
	} 
	
	FallBack "Diffuse"
}





//Shader "Custom/BlurObject" {
//	Properties {
//		_MainTex ("Base (RGB)", 2D) = "white" {}
////		_CurMV("Current MVP", float4x4) = 0
////		_PrevMV("Previous MVP", float4x4) = 0
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
//		            float2 texcoord : TEXCOORD0; //1st texture coord
//		            float4 col : COLOR; //vertex color
//		    };
//		        
//			struct v2f{
//				float4 Pos : SV_POSITION;//position of vertex under view frustrum? 
//				float4 vPos : TEXCOORD0; //texture coord for velocity map
//				float3 Velocity : TEXCOORD1;
//				float2 texcoord : TEXCOORD2;
//				float4 Col : COLOR;
//			};
//
//			v2f vert(appdata i){
//				v2f o;
//				float4 curP  = mul(_CurMV,i.vertex);//current vertex position in View Coordinate
//				float4 prevP = mul(_PrevMV,i.vertex);//last frame vertex position in View Coordinate
//				float3 vel = curP.xyz - prevP.xyz; //velocity of vertex in View Coordinate
//				float3 n = mul((float3x3)_CurMV,i.normal); //normal Vector of current vertex in View Coordinate
//				
//				float flag = dot(vel, n) >= 0; //did vertex move toward the velocity's direction??
//				float4 pos = flag ? curP : prevP; //YES => use current vertex pos, NO => use previous vertex pos
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
//			float4 frag(v2f i):COLOR{
//				float4 col;
//				//by dividing x, y, z by w, you can get screen position between -1<x<1,-1<y<1,0<z<1
//				//this [tex] is the position of each vertex in the screen
//				float2 tex = i.vPos.xy/i.vPos.w; 
//				float2 velocity = i.Velocity.xy;
//				
////				velocity.xy += 1.0f; //change -1<x<1 to 0<x<2
////				velocity.xy *= 0.5f; //change 0<x<2 to 0<x<1
//				
////				float2 velocity = i.Velocity.xy * 0.3f;
////				col = float4(1, 1, 0,1);
////				col = float4(0.5f, 0.5f, 0.0f,1.0f);
//				col = float4(velocity.x+0.5f, velocity.y+0.5f, 0,1);
////				col = float4(0.5f + (velocity.x/10.0f), 0.5f + (velocity.y/10.0f), 0,1);
//				
////				col.r = velocity.x;
////				col.g = velocity.y;
////				for ( float i=0; i<10; i++){
////			  		float t = (i+1)/samples;
////			 		Out += tex2D(_MainTex, tex + t *velocity);
////			  	}
//
////				col = tex2D(_MainTex, i.texcoord);
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
