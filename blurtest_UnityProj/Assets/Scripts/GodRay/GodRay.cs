using UnityEngine;
using System.Collections;

public class GodRay : MonoBehaviour {

	PostEffectCameraGodRay post;

	private string matDir = "Materials/PostEffect";
	Material raialBlurMaterial;
	Material bloomMaterial;
	Material blurmMaterial;
	Material maskMaterial;
	Material multiplyMaterial;
	Material addMaterial;
	
	void Start(){
		raialBlurMaterial = new Material( Shader.Find("Custom/Heat") );
		bloomMaterial = new Material( Shader.Find("Custom/Heat") );
		blurmMaterial = new Material( Shader.Find("Custom/Heat") );

		maskMaterial = new Material( Shader.Find("Custom/Heat") );
		multiplyMaterial = new Material( Shader.Find("Custom/Heat") );
		addMaterial = new Material( Shader.Find("Custom/Heat") );

		post = GameObject.Find("PostEffectCamera").GetComponent<PostEffectCameraGodRay>();
	}


	void RadialBluredTerrainSilhouetteFilter(RenderTexture source, RenderTexture dest){
		raialBlurMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, raialBlurMaterial);
	}

	void MaskedSunBlurFilter(RenderTexture source, RenderTexture mask , RenderTexture dest){
		blurmMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, blurmMaterial);
	}

	void SingleSunBloomFilter(RenderTexture source, RenderTexture dest){
		blurmMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, blurmMaterial);
	}

	void SunMaskFilter(RenderTexture source, RenderTexture maskTexture, RenderTexture dest){
		maskMaterial.SetTexture("_MainTex",source);
		Graphics.Blit( source, dest, maskMaterial);
	}


	void MutiplyFilter(RenderTexture source1, RenderTexture souce2, RenderTexture dest){
		multiplyMaterial.SetTexture("_MainTex",source1);
		Graphics.Blit( source1, dest, multiplyMaterial);
	}

	void AddFilter(RenderTexture source1, RenderTexture souce2, RenderTexture dest){
		addMaterial.SetTexture("_MainTex",source1);
		Graphics.Blit( source1, dest, addMaterial);
	}
	

	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		
		int rtW = source.width;
		int rtH = source.height;
		
		//****** Radial Blur Terrain Silhouette  ****** 
		RenderTexture bufferRadialBluredTerrainSilhouette = RenderTexture.GetTemporary(rtW, rtH, 0);
		RadialBluredTerrainSilhouetteFilter(post.TerrainSilhouetteTexture, bufferRadialBluredTerrainSilhouette);
		//****** Single Sun Bloom Texture  ****** 
		RenderTexture bufferSingleSunBloom = RenderTexture.GetTemporary(rtW, rtH, 0);
		SingleSunBloomFilter(post.SunTexture, bufferSingleSunBloom);
		//****** Multiplied (Radial Blur Terrain Silhouett) X (Single Sun Bloom Texture) = GODRAY IMAGE ************
		RenderTexture bufferGodRay = RenderTexture.GetTemporary(rtW, rtH, 0);
		MutiplyFilter(bufferRadialBluredTerrainSilhouette, bufferSingleSunBloom, bufferSingleSunBloom);


		//****** Sun Mask  ****** 
		RenderTexture bufferSunMask = RenderTexture.GetTemporary(rtW, rtH, 0);
		SunMaskFilter(post.SunTexture, post.TerrainSilhouetteTexture, bufferSunMask);
		//****** Masked Sun Blur  ****** 
		RenderTexture bufferSunBlur = RenderTexture.GetTemporary(rtW, rtH, 0);
		MaskedSunBlurFilter(bufferSunMask, post.TerrainSilhouetteTexture, bufferSunBlur);


		//****** Add all to get Final Image  ****** 
		RenderTexture bufferFinalImage1 = RenderTexture.GetTemporary(rtW, rtH, 0);
		AddFilter(source, bufferGodRay, bufferFinalImage1);
		RenderTexture bufferFinalImage2 = RenderTexture.GetTemporary(rtW, rtH, 0);
		AddFilter(bufferFinalImage1, bufferSunBlur, bufferFinalImage2);




		Graphics.Blit( bufferFinalImage2, dest);
		
		RenderTexture.ReleaseTemporary( bufferRadialBluredTerrainSilhouette );
		RenderTexture.ReleaseTemporary( bufferSingleSunBloom );
		RenderTexture.ReleaseTemporary( bufferGodRay );
		RenderTexture.ReleaseTemporary( bufferSunMask );
		RenderTexture.ReleaseTemporary( bufferSunBlur );
		RenderTexture.ReleaseTemporary( bufferFinalImage1 );
		RenderTexture.ReleaseTemporary( bufferFinalImage2 );
		
	}
	


}
