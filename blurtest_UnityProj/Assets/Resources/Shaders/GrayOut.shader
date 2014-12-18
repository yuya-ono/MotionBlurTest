Shader "Custom/GrayOut" {
	
	Properties {
        _MainTex("_MainTex", 2D) = "" {}
    }
    
	SubShader {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
		Pass{
			CGPROGRAM
			
			#pragma vertex vert
	        #pragma fragment frag
	        #include "UnityCG.cginc"
	        
	        uniform sampler2D _MainTex;
	        
	        struct appdata {
	                float4 vertex   : POSITION;
	                float3 normal   : NORMAL;
	                half2 texcoord : TEXCOORD0; //uv
	                float4 color    : Color; //uv
	        };
	        
	        struct v2f {
	          float4 pos : SV_POSITION;
	          fixed4 color : COLOR;
	          half2 texcoord : TEXCOORD0; //uv
	        };
	  

			fixed4 Desaturate(fixed3 color, float Desaturation) 
			{ 
			    fixed3 grayXfer = fixed3(0.3, 0.59, 0.11); 
			    fixed grayf = dot(grayXfer, color); 
			    fixed3 gray = fixed3(grayf, grayf, grayf); 
			    return fixed4(lerp(color, gray, Desaturation), 1.0); 
			}
			
			
			float3 ContrastSaturationBrightness(float3 color, float brt, float sat, float con) 
			{ 
			    // Increase or decrease theese values to adjust r, g and b color channels seperately 
			    const float AvgLumR = 0.1; 
			    const float AvgLumG = 0.1; 
			    const float AvgLumB = 1.0; 
//			    const float AvgLumR = 0.5; 
//			    const float AvgLumG = 0.5; 
//			    const float AvgLumB = 0.5; 
			    const float3 LumCoeff = float3(0.2125, 0.7154, 0.0721); 
//				const float3 LumCoeff = float3(0.9125, 0.1154, 0.0721); 
			    
			    float3 AvgLumin = float3(AvgLumR, AvgLumG, AvgLumB); 
			    float3 brtColor = color * brt; 
			    float intensityf = dot(brtColor, LumCoeff); 
			    float3 intensity = float3(intensityf, intensityf, intensityf); 
			    float3 satColor = lerp(intensity, brtColor, sat); 
			    float3 conColor = lerp(AvgLumin, satColor, con); 
			    return conColor; 
			}
	        
	        v2f vert( appdata v ) {
		         v2f o; 
		         o.pos = mul (UNITY_MATRIX_MVP, v.vertex); 
		         o.texcoord = MultiplyUV( UNITY_MATRIX_TEXTURE0, v.texcoord ); 
		         return o; 
	         }
	        
	        fixed4 frag(v2f i) : COLOR {
	        		fixed4 col = tex2D(_MainTex, i.texcoord);
	        		col.rgb = 1 - col.rgb;
//					return col;
					return fixed4(  ContrastSaturationBrightness( col.rgb , 1.0, 0.0, 0.9), 1.0);
//					return fixed4(  Desaturate( tex2D(_MainTex, i.texcoord).rgb , 0.9));
//					return fixed4(  ContrastSaturationBrightness( tex2D(_MainTex, i.texcoord).rgb , 1.0, 0.0, 0.9), 1.0);
	        }   
			ENDCG
		}	
	} 
	
	FallBack "Diffuse"
}
