using UnityEngine;
using System.Collections;

public class PostEffectCamera : MonoBehaviour {

	protected Camera cam;
	
	virtual protected void Start(){
		cam = gameObject.camera;
		cam.CopyFrom( Camera.main );
		cam.enabled = false;
	}
	
	virtual protected void Update(){
		ExecutePreRenderProcess();
	}
	
	virtual protected void ExecutePreRenderProcess(){

		//retain reference to BackBuffer( default active RenderTarget is BackBuffer )
		RenderTexture currentRT = RenderTexture.active; 
		RenderTexture.active = cam.targetTexture;

		CustomRendererProcess ();

		//put back RenderTarget to 
		RenderTexture.active = currentRT; 
	}

	virtual protected void CustomRendererProcess(){


	}



}
