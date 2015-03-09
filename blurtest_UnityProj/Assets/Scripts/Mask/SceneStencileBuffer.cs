using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneStencileBuffer : MonoBehaviour {

	public Dictionary<int, RenderTexture> stencileBuffers;
	private int defaultLayers;

	// Use this for initialization
	void Start () {
		stencileBuffers = new Dictionary<int, RenderTexture>();
		Add ("Water");
	}
	
	// Update is called once per frame
	void Update () {
		defaultLayers = Camera.main.cullingMask;
	}

	void OnPreRender() {

		//create stencile buffer to each surface
		foreach( int layer in stencileBuffers.Keys){
			
			RenderTexture currentRT = RenderTexture.active;
			RenderTexture.active = Camera.main.targetTexture;
			
			int layermask = 1 << layer;
			Camera.main.cullingMask = layermask;
			Camera.main.RenderWithShader( Shader.Find("Mask"), "Player");
			
			//			velocityCamera.Render();
			
			RenderTexture.active = currentRT;
			//			Graphics.Blit(
		}

	}


	void OnRenderImage(RenderTexture source, RenderTexture dest) {

//		//create stencile buffer to each surface
//		foreach( int layer in stencileBuffers.Keys){
//
//			RenderTexture currentRT = RenderTexture.active;
//			RenderTexture.active = Camera.main.targetTexture;
//
//			int layermask = 1 << layer;
//			Camera.main.cullingMask = layermask;
//			Camera.main.RenderWithShader( Shader.Find("Mask"), "Player");
//
////			velocityCamera.Render();
//			
//			RenderTexture.active = currentRT;
////			Graphics.Blit(
//		}

//		Material mat = blurMaterial;
//		mat.shader = blurShader;
//		mat.SetTexture("_VelocityTex",blurEffectController.velocityTexture);
//		
//		//		Graphics.Blit(blurObject.velocityTexture, dest);
//		Graphics.Blit(source, dest, mat);
//		//		Graphics.Blit(source, dest);
	}

//	public void RendererWithTag(){
//		//		RenderTexture buffer = RenderTexture.GetTemporary(500, 500, 0);
//		//		camera.targetTexture = buffer;
//		
//		RenderTexture currentRT = RenderTexture.active;
//		RenderTexture.active = cam.targetTexture;
//		
//		//		cam.RenderWithShader(shader,"");
//		cam.Render();
//		//		cam.RenderWithShader(shader);
//		//		Debug.Log("rendered !!");
//		
//		//		Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
//		//		image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
//		//		image.Apply();
//		
//		RenderTexture.active = currentRT;
//		//		gUITexture.texture = image;
//		//		mapDisplay.material.mainTexture = image;
//		
//		//		Graphics.Blit(buffer, mapDisplay);
//		//		Graphics.Blit(buffer, velocityTexture);
//		
//		
//		//		velocityTexture = cam.targetTexture;
//		
//		//		RenderTexture.ReleaseTemporary(buffer);
//		
//		//		return image;
//		
//	}

	#region Mask Input Output

	public void Add(string tag){
		Debug.Log("layer : "+LayerMask.NameToLayer( tag ));
		stencileBuffers.Add( LayerMask.NameToLayer( tag ) , null );
	}

	public void Remove(string tag){
		stencileBuffers.Remove( LayerMask.NameToLayer( tag ) );
	}

	public RenderTexture Get(string tag){
		if(stencileBuffers.ContainsKey( LayerMask.NameToLayer( tag ) ))
			return stencileBuffers[ LayerMask.NameToLayer( tag ) ];
		else
			return null;
	}

	#endregion

}
