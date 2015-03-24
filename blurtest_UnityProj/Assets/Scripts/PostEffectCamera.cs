using UnityEngine;
using System.Collections;

public enum PreRenderType{
	HEAT, VELOCITY
}

public class PostEffectCamera : MonoBehaviour {

	PreRenderType[] preRenderTypes;
	public GameObject heatParticle;
	RenderTexture heatTexture;
	Shader shader;
	Camera cam;
	
	void Start(){
		cam = gameObject.camera;
		cam.CopyFrom( Camera.main );
		shader = Shader.Find("Custom/Particles/Heat");
		
		heatTexture = new RenderTexture((int)cam.pixelWidth, (int)cam.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		heatTexture.Create();
		cam.enabled = false;
		
	}
	
	void Update(){
		ExecutePreRenderProcess();
	}
	
	void ExecutePreRenderProcess(){
		cam.CopyFrom( Camera.main );
		cam.targetTexture = heatTexture;
		cam.cullingMask = 1 << 13;//LayerMask.NameToLayer("heat");

		//retain reference to BackBuffer( default active RenderTarget is BackBuffer )
		RenderTexture currentRT = RenderTexture.active; 
		RenderTexture.active = cam.targetTexture;

		//heat
		cam.Render();
//		cam.RenderWithShader(shader,"Heat");


		//here you can add many phase as you want
		//********

		//********

		//********

		//********

		//********


		//put back RenderTarget to 
		RenderTexture.active = currentRT; 
	}

	public void AddPreRenderType(PreRenderType type){


	}

	public RenderTexture HeatMapTexture{
		get{ return heatTexture; }
	}
	


}
