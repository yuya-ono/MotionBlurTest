
using UnityEngine;
using System.Collections;

public class PostEffectCameraHeat : PostEffectCamera {

	public GameObject heatParticle;
	RenderTexture heatTexture;
	Shader shader;

	public RenderTexture HeatMapTexture{
		get{ return heatTexture; }
	}

	protected override void Start(){
		base.Start();
		shader = Shader.Find("Custom/Particles/Heat");
		
		heatTexture = new RenderTexture((int)cam.pixelWidth, (int)cam.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		heatTexture.Create();
		
	}

	protected override void CustomRendererProcess ()
	{
		base.CustomRendererProcess ();

		cam.CopyFrom( Camera.main );
		cam.targetTexture = heatTexture;
		cam.cullingMask = 1 << 13;//LayerMask.NameToLayer("heat");

		//heat
		cam.Render();
	}
	
	
	
}
