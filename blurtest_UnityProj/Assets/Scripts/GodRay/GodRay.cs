using UnityEngine;
using System.Collections;

public class GodRay : MonoBehaviour {

	public GameObject sun;

	PostEffectCameraGodRay post;
	Camera cam;
	Vector3 screenSpaceSunPosition;

	private string matDir = "Materials/PostEffect";

	Material blackFillMaterial;

	Material radialBlurMaterial;
	Material bloomMaterial;
	Material blurmMaterial;

	Material brightnessMaterial;
	Material blurXMaterial;
	Material blurYMaterial;

	Material maskMaterial;
	Material multiplyMaterial;
	Material addMaterial;
	
	void Start(){
		//black fill
		blackFillMaterial = new Material (Shader.Find ("Custom/BlackFill"));

		//explosion blur
		radialBlurMaterial = new Material( Shader.Find("Custom/ExplosionBlur") );
		//??
		bloomMaterial = new Material( Shader.Find("Custom/Heat") );
		blurmMaterial = new Material( Shader.Find("Custom/Heat") );
		//bloom
		brightnessMaterial = new Material( Shader.Find("Custom/BrightnessFilter") );
		blurXMaterial = new Material( Shader.Find("Custom/BlurFilterX") );
		blurYMaterial = new Material( Shader.Find("Custom/BlurFilterY") );

		maskMaterial = new Material( Shader.Find("Custom/Heat") );
		multiplyMaterial = new Material( Shader.Find("Custom/Multiply") );
		addMaterial = new Material( Shader.Find("Custom/Heat") );

		post = GameObject.Find("PostEffectCamera").GetComponent<PostEffectCameraGodRay>();
		cam = GetComponent<Camera> ();
		if (sun == null)
			sun = GameObject.Find ("Sun");
	}

	//frame calculation
	void Update(){
		screenSpaceSunPosition = cam.WorldToViewportPoint (sun.transform.position);
	}



	void TerrainSilhouetteFilter(RenderTexture source, RenderTexture dest){
		blackFillMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, blackFillMaterial);
	}

	void RadialBluredTerrainSilhouetteFilter(RenderTexture source, RenderTexture dest){
		radialBlurMaterial.SetTexture("_MainTex",source);
		radialBlurMaterial.SetFloat("_CenterTexelX",screenSpaceSunPosition.x);
		radialBlurMaterial.SetFloat("_CenterTexelY",screenSpaceSunPosition.y);
		radialBlurMaterial.SetFloat("_Power",0.03f);
		Graphics.Blit( source, dest, radialBlurMaterial);
	}

	void MaskedSunBlurFilter(RenderTexture source, RenderTexture mask , RenderTexture dest){
		blurmMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, blurmMaterial);
	}


	void SunMaskFilter(RenderTexture source, RenderTexture maskTexture, RenderTexture dest){
		maskMaterial.SetTexture("_MainTex",source);
		maskMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, maskMaterial);
	}

	void MutiplyFilter(RenderTexture source1, RenderTexture souce2, RenderTexture dest){
		multiplyMaterial.SetTexture("_MainTex",source1);
		multiplyMaterial.SetTexture("_SubTex",souce2);
		Graphics.Blit( source1, dest, multiplyMaterial);
	}

	

	void OnRenderImage(RenderTexture source, RenderTexture dest) {

		int rtW = source.width;
		int rtH = source.height;

		//****** create Terrain Silhouette  ok ****** 
		RenderTexture terrainSilhouette = RenderTexture.GetTemporary(rtW, rtH, 0);
		TerrainSilhouetteFilter(post.GodRaySilhouetteTexture, terrainSilhouette);

		//****** Radial Blur(from center position of sun) Terrain Silhouette  ok ****** 
		RenderTexture bufferRadialBluredTerrainSilhouette = RenderTexture.GetTemporary(rtW, rtH, 0);
		GraphicsConvert.RaidalBlurFilter(terrainSilhouette, bufferRadialBluredTerrainSilhouette, 0.07f, 4, screenSpaceSunPosition.x, screenSpaceSunPosition.y );

		//****** Single Sun Bloom Texture  ok ****** 
		RenderTexture bufferSingleSunBloom = RenderTexture.GetTemporary(rtW, rtH, 0);
		GraphicsConvert.BloomExtractFilter(post.SunTextureAlpha, bufferSingleSunBloom, 5.0f);
		Graphics.Blit( bufferSingleSunBloom, dest);

		//****** Multiplied (Radial Blur Terrain Silhouett) X (Single Sun Bloom Texture) = GODRAY IMAGE ************
		RenderTexture bufferGodRay = RenderTexture.GetTemporary(rtW, rtH, 0);
		MutiplyFilter(bufferRadialBluredTerrainSilhouette, bufferSingleSunBloom, bufferGodRay);

		//****** Sun Mask  ****** 
		RenderTexture bufferSunMask = RenderTexture.GetTemporary(rtW, rtH, 0);
		MutiplyFilter(post.SunTexture, post.GodRaySilhouetteTexture, bufferSunMask);

		//****** Masked Sun Blur  ****** 
		RenderTexture bufferSunBlur = RenderTexture.GetTemporary(rtW, rtH, 0);
		GraphicsConvert.BloomFilter (bufferSunMask, bufferSunBlur, 2.0f);

		//****** Add all to get Final Image  ****** 
		RenderTexture bufferFinalImage1 = RenderTexture.GetTemporary(rtW, rtH, 0);
		GraphicsConvert.AddFilter(source, bufferGodRay, bufferFinalImage1);
		RenderTexture bufferFinalImage2 = RenderTexture.GetTemporary(rtW, rtH, 0);
		GraphicsConvert.AddFilter(bufferFinalImage1, bufferSunBlur, bufferFinalImage2);

		Graphics.Blit( bufferFinalImage2, dest);

		RenderTexture.ReleaseTemporary( terrainSilhouette );
		RenderTexture.ReleaseTemporary( bufferRadialBluredTerrainSilhouette );
		RenderTexture.ReleaseTemporary( bufferSingleSunBloom );
		RenderTexture.ReleaseTemporary( bufferGodRay );
		RenderTexture.ReleaseTemporary( bufferSunMask );
		RenderTexture.ReleaseTemporary( bufferSunBlur );
		RenderTexture.ReleaseTemporary( bufferFinalImage1 );
		RenderTexture.ReleaseTemporary( bufferFinalImage2 );
		
	}
	


}
