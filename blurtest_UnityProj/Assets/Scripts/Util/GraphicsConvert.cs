using UnityEngine;
using System.Collections;

public static class GraphicsConvert {

	private static bool isInitialized = false;
	private static Material brightnessMaterial;
	private static Material blurXMaterial;
	private static Material blurYMaterial;
	private static Material addMaterial;
	private static Material explostionBlurMaterial;

	public static void Init(){
		if (isInitialized)
						return;

		brightnessMaterial = new Material( Shader.Find("Custom/BrightnessFilter") );
		blurXMaterial = new Material( Shader.Find("Custom/BlurFilterX") );
		blurYMaterial = new Material( Shader.Find("Custom/BlurFilterY") );
		addMaterial = new Material( Shader.Find("Custom/AddFilter") );
		explostionBlurMaterial = new Material( Shader.Find("Custom/ExplosionBlur") );
	}

	#region Filters

	// add filter
	public static void AddFilter (RenderTexture baseSource, RenderTexture addsource, RenderTexture dest)
	{
		Material mat = addMaterial;
		mat.SetTexture("_AdditiveTex",addsource);
		Graphics.Blit(baseSource, dest, mat);
	}

	public static void BlurFilter(RenderTexture source, RenderTexture dest, float power, float count){
		Init ();
		RenderTexture writeRT = RenderTexture.GetTemporary (source.width, source.height, 0);
		writeRT = source;
		blurXMaterial.SetFloat ("_Power", power);
		blurYMaterial.SetFloat ("_Power", power);
		for (int i=0; i<count; i++) {
						//****** BLUR X ****** OK
//						RenderTexture bufferBlurX = RenderTexture.GetTemporary (source.width, source.height, 0);
//						blurXMaterial.SetFloat ("_Power", power);
						Graphics.Blit (writeRT, writeRT, blurXMaterial);
//						Graphics.Blit (source, bufferBlurX, blurXMaterial);
			
						//****** BLUR Y ****** OK
//						RenderTexture bufferBlurXY = RenderTexture.GetTemporary (source.width, source.height, 0);
//						blurYMaterial.SetFloat ("_Power", power);
						Graphics.Blit (writeRT, writeRT, blurYMaterial);
//						Graphics.Blit (bufferBlurX, bufferBlurXY, blurYMaterial);
//						RenderTexture.ReleaseTemporary( bufferBlurX );
//						RenderTexture.ReleaseTemporary( bufferBlurXY );
		}
		Graphics.Blit( writeRT, dest);
//		Graphics.Blit( bufferBlurXY, dest);
		RenderTexture.ReleaseTemporary( writeRT );

//		RenderTexture.ReleaseTemporary( bufferBlurX );
//		RenderTexture.ReleaseTemporary( bufferBlurXY );
	}
	
	public static void BloomFilter(RenderTexture source, RenderTexture dest, float power){
		Init ();

		//****** BRIGHTNESS ****** OK
		RenderTexture buffer = RenderTexture.GetTemporary(source.width, source.height, 0);
		Graphics.Blit(source, buffer, brightnessMaterial);
		
		//****** BLUR X ****** OK
		RenderTexture bufferBlurX = RenderTexture.GetTemporary(source.width, source.height, 0);
		blurXMaterial.SetFloat ("_Power", power);
		Graphics.Blit(buffer, bufferBlurX, blurXMaterial);
		
		//****** BLUR Y ****** OK
		RenderTexture bufferBlurXY = RenderTexture.GetTemporary(source.width, source.height, 0);
		blurYMaterial.SetFloat ("_Power", power);
		Graphics.Blit(bufferBlurX, bufferBlurXY, blurYMaterial);

		AddFilter(source, bufferBlurXY, dest);

		
		RenderTexture.ReleaseTemporary( buffer );
		RenderTexture.ReleaseTemporary( bufferBlurX );
		RenderTexture.ReleaseTemporary( bufferBlurXY );
	}

	public static void RaidalBlurFilter(RenderTexture source, RenderTexture dest, float power, float count, float x, float y){
		Init ();

		explostionBlurMaterial.SetTexture ("_MainTex", source);
		explostionBlurMaterial.SetFloat ("_CenterTexelX", x);
		explostionBlurMaterial.SetFloat ("_CenterTexelY", y);
		explostionBlurMaterial.SetFloat ("_Power", power);

//		Graphics.Blit( source, dest,explostionBlurMaterial);

		RenderTexture writeRT = RenderTexture.GetTemporary (source.width, source.height, 0);
		Graphics.Blit( source, writeRT);
		for (int i=0; i<count; i++) {
			RenderTexture writeRTdest = RenderTexture.GetTemporary (source.width, source.height, 0);
			Graphics.Blit( writeRT, writeRTdest, explostionBlurMaterial);
			Graphics.Blit( writeRTdest, writeRT);
			RenderTexture.ReleaseTemporary( writeRTdest );
		}

		Graphics.Blit( writeRT, dest);
		RenderTexture.ReleaseTemporary( writeRT );
	}

	#endregion

}
