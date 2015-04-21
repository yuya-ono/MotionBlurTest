using UnityEngine;
using System.Collections;

public class PostEffectCameraGodRay : PostEffectCamera {

//	RenderTexture terrainSilhouetteTexture;
	RenderTexture godRaySilhouetteTexture;
	RenderTexture sunTexture;
	RenderTexture sunTextureAlpha;

	public Shader maskShader;
//	public Shader sunShader;

//	public RenderTexture TerrainSilhouetteTexture{
//		get{return terrainSilhouetteTexture;}
//	}

	public RenderTexture GodRaySilhouetteTexture{
		get{return godRaySilhouetteTexture;}
	}

	public RenderTexture SunTexture{
		get{return sunTexture;}
	}
	public RenderTexture SunTextureAlpha{
		get{return sunTextureAlpha;}
	}


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		godRaySilhouetteTexture = new RenderTexture((int)cam.pixelWidth, (int)cam.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		godRaySilhouetteTexture.Create();
		sunTexture = new RenderTexture((int)cam.pixelWidth, (int)cam.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		sunTexture.Create();
		sunTextureAlpha = new RenderTexture((int)cam.pixelWidth, (int)cam.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		sunTextureAlpha.Create();
	}


	protected override void CustomRendererProcess ()
	{
		base.CustomRendererProcess ();

		cam.CopyFrom( Camera.main );

		//**** Create Terrain Silhouette Texture

		//first terrain
		cam.backgroundColor = Color.clear;
		cam.targetTexture = godRaySilhouetteTexture;
		cam.cullingMask = 1 << 9 | 1 << 10; 
//		cam.cullingMask = 1 << 9; 
//		Shader.SetGlobalColor ("_MaskColor", Color.black);
		cam.Render ();



//		cam.RenderWithShader(maskShader, "");

//		cam.backgroundColor = Color.white;
//		cam.targetTexture = terrainSilhouetteTexture;
//		cam.cullingMask = 1 << 9; 
//		Shader.SetGlobalColor ("_MaskColor", Color.black);
//		cam.RenderWithShader(maskShader, "");

		//secondly trees


		//**** Sun Texture
		cam.backgroundColor = Color.black;
		cam.targetTexture = sunTexture;
		cam.cullingMask = 1 << 8;
		Shader.SetGlobalColor ("_MaskColor", Color.white);
		cam.RenderWithShader(maskShader, "");

		//**** Sun Texture Alpha
		cam.targetTexture = sunTextureAlpha;
		Shader.SetGlobalColor ("_MaskColor", new Color(1,1,1,0.5f));
		cam.RenderWithShader(maskShader, "");
		
		//********

	}
}
