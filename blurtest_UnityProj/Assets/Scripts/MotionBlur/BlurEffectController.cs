using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurEffectController : MonoBehaviour {

	public Camera velocityCamera;
	public Shader blurShader;

	public const int blurFrame = 1;
//	public GameObject[] targetObjects;public RenderTexture velocityTexture;

	[HideInInspector]
	public RenderTexture velocityTexture;

	private List<BlurEffect> targetObjects = new List<BlurEffect>();

	// Use this for initialization
	void Awake () {

		velocityCamera.CopyFrom(Camera.main);
		velocityTexture = new RenderTexture((int)velocityCamera.pixelWidth, (int)velocityCamera.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		velocityTexture.Create();
		
		velocityCamera.targetTexture = velocityTexture;
		velocityCamera.enabled = false;

		blurShader = Shader.Find("Custom/BlurObject");
//		blurShader = Shader.Find("Custom/basic");

	}
	
	// Update is called once per frame
	void Update () {
		velocityCamera.CopyFrom(Camera.main);
		velocityCamera.targetTexture = velocityTexture;

		ChangeToBlurShader();
		RendererWithBlur();
		PutBackToDefaultShader();
	}

	public void Add(GameObject go){
		BlurEffect blurEffect = go.AddComponent<BlurEffect>();
		blurEffect.blurShader     = blurShader;
		blurEffect.velocityCamera = velocityCamera;
		targetObjects.Add(blurEffect);
	}

	#region Private
	private void RendererWithBlur(){

		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = velocityCamera.targetTexture;

		velocityCamera.Render();

		RenderTexture.active = currentRT;

	}

	private void ChangeToBlurShader(){
		foreach(BlurEffect blur in targetObjects)
			blur.SwitchToBlurShader();
	}
	private void PutBackToDefaultShader(){
		foreach(BlurEffect blur in targetObjects)
			blur.PutBackToDefaultShader();
	}
	#endregion

}
