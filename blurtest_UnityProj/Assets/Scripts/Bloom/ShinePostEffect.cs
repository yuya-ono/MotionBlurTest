using UnityEngine;
using System.Collections;

public class ShinePostEffect : MonoBehaviour {
	
	public Material brightnessMaterial;
	public Material blurXMaterial;
	public Material blurYMaterial;
	public Material bloomMaterial;
	Shader brightnessShader;

	void Start(){
		//set shader
//		brightnessShader = Shader.Find("Custom/Brightness");
//		brightnessMaterial = Resources.Load<Material>("Materials/Brightness");
	}

	// Downsamples the texture to a quarter resolution.
	private void DownSample4x (RenderTexture source, RenderTexture dest)
	{
//		float off = 1.0f;
//		Graphics.BlitMultiTap (source, dest, material,
//		                       new Vector2(-off, -off),
//		                       new Vector2(-off,  off),
//		                       new Vector2( off,  off),
//		                       new Vector2( off, -off)
//		                       );
	}

	// brightness filter
	private void BrightnessFilter (RenderTexture source, RenderTexture dest)
	{
		Material mat = brightnessMaterial;
		Graphics.Blit(source, dest, mat);
	}

	// blur filter
//	private void BlurFilter (RenderTexture source, RenderTexture dest)
//	{
//		Material mat = blurMaterial;
//		Graphics.Blit(source, dest, mat);
//	}
	private void BlurFilterX (RenderTexture source, RenderTexture dest)
	{
		Material mat = blurXMaterial;
		Graphics.Blit(source, dest, mat);
	}

	private void BlurFilterY (RenderTexture source, RenderTexture dest)
	{
		Material mat = blurYMaterial;
		Graphics.Blit(source, dest, mat);
	}

	// add filter
	private void AddFilter (RenderTexture baseSource, RenderTexture addsource, RenderTexture dest)
	{
		Material mat = bloomMaterial;
		mat.SetTexture("_AdditiveTex",addsource);
		Graphics.Blit(baseSource, dest, mat);
	}

	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		//****** BRIGHTNESS ****** OK
//		RenderTexture buffer = RenderTexture.GetTemporary(source.width, source.height, 0);
//		Material mat = brightnessMaterial;
//		Graphics.Blit(source, dest, mat);

		//****** BLUR ****** OK
//		RenderTexture buffer = RenderTexture.GetTemporary(source.width, source.height, 0);
//		Material mat = blurMaterial;
//		Graphics.Blit(source, dest, mat);

		//****** BRIGHTNESS ****** OK
		RenderTexture buffer = RenderTexture.GetTemporary(source.width, source.height, 0);
		BrightnessFilter(source, buffer);

		//****** BLUR X ****** OK
		RenderTexture bufferBlurX = RenderTexture.GetTemporary(source.width, source.height, 0);
		BlurFilterX(buffer, bufferBlurX);

		//****** BLUR Y ****** OK
		RenderTexture bufferBlurXY = RenderTexture.GetTemporary(source.width, source.height, 0);
		BlurFilterY(bufferBlurX, bufferBlurXY);

		//****** ADD ****** 
		AddFilter(source, bufferBlurXY, dest);

		RenderTexture.ReleaseTemporary( buffer );
		RenderTexture.ReleaseTemporary( bufferBlurX );
		RenderTexture.ReleaseTemporary( bufferBlurXY );
		
	}	
//	void OnRenderImage(RenderTexture source, RenderTexture dest) {
//		RenderTexture buffer = RenderTexture.GetTemporary(source.width, source.height, 0);
//	
//		//1: renderer the scene and get brightness from Color & PhoneShading
//		BrightnessFilter( source, buffer );
//
//
//		//2: Blurfilter the result of brightness you got in 1.
//		//create small buffer texture
//		int rtW = buffer.width/4;
//		int rtH = buffer.height/4;
//		RenderTexture buffer1 = RenderTexture.GetTemporary(rtW, rtH, 0);
//
//		// Copy source to the 4x4 smaller texture.
////		DownSample4x (buffer, buffer1);
//
//		//blit to buffer2
//		RenderTexture buffer2 = RenderTexture.GetTemporary(rtW, rtH, 0);
//		BlurFilter( buffer1, buffer2);
//
//
//		//3. add Composite the blured brightness texture to scene renderer texture
//		AddFilter( source, buffer2, dest);
//
//	}
}
