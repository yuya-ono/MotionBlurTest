using UnityEngine;
using System.Collections;

public class Bloom : MonoBehaviour {

	public float brightness = 0.0f;
	public float blurVolume = 1.0f;
	Material brightnessMat;
	Material blurMat;
	Material additiveMat;

	void Start(){
		brightnessMat = new Material( Shader.Find("Custom/BrightnessFilter") );
		blurMat       = new Material( Shader.Find("Custom/BlurFilter") );
		additiveMat   = new Material( Shader.Find("Custom/AdditiveFilter") );
	}

	public void Destroy(){
		Destroy( this );
	}

	void OnDestroy(){
		if(brightnessMat!=null)DestroyImmediate(brightnessMat);
		if(blurMat!=null)DestroyImmediate(blurMat);
		if(additiveMat!=null)DestroyImmediate(additiveMat);
	}

	// brightness filter
	private void BrightnessFilter (RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest, brightnessMat);
	}

	private void BlurFilter (RenderTexture source, RenderTexture dest)
	{
		blurMat.SetFloat("_Volume",brightness);
		Graphics.Blit(source, dest, blurMat);
	}

	// add filter
	private void AddFilter (RenderTexture source,RenderTexture addsource, RenderTexture dest)
	{
		additiveMat.SetFloat("_AddRate",brightness);
		additiveMat.SetTexture("_AdditiveTex",addsource);
		Graphics.Blit(source, dest, additiveMat);
	}

	private void DownSample(){

	}

	// Update is called once per frame
	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		int rtW = source.width;
		int rtH = source.height;

		//****** BRIGHTNESS ****** OK
		RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
		BrightnessFilter(source, buffer);

		//****** BLUR  ****** OK
		RenderTexture bufferBlur = RenderTexture.GetTemporary(rtW, rtH, 0);
		BlurFilter(buffer, bufferBlur);

		//****** ADD ****** 
		RenderTexture bufferAdditive = RenderTexture.GetTemporary(rtW, rtH, 0);
		AddFilter(source, bufferBlur, bufferAdditive);

		Graphics.Blit( bufferAdditive, dest);
		
		RenderTexture.ReleaseTemporary( buffer );
		RenderTexture.ReleaseTemporary( bufferBlur );
		RenderTexture.ReleaseTemporary( bufferAdditive );
	}
}
