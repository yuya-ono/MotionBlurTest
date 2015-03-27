using UnityEngine;
using System.Collections;

public class PostEffectCameraGodRay : PostEffectCamera {

	RenderTexture terrainSilhouetteTexture;
	RenderTexture sunTexture;

	Shader maskShader;
	Shader sunShader;

	public RenderTexture TerrainSilhouetteTexture{
		get{return terrainSilhouetteTexture;}
	}

	public RenderTexture SunTexture{
		get{return sunTexture;}
	}


	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}


	protected override void CustomRendererProcess ()
	{
		base.CustomRendererProcess ();

		cam.CopyFrom( Camera.main );

		//**** Create Terrain Silhouette Texture
		cam.targetTexture = terrainSilhouetteTexture;
		cam.cullingMask = 1 << LayerMask.NameToLayer ("terrain");
		cam.RenderWithShader(maskShader, "");
		//		cam.Render();



		//**** Create Terrain Silhouette Radial Blured Texture
		
		
		//**** Sun Texture
		cam.targetTexture = sunTexture;
		cam.cullingMask = 1 << LayerMask.NameToLayer ("sun");
		cam.RenderWithShader(sunShader, "");
		
		//********

	}
}
