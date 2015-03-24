using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurSpecial : MonoBehaviour {

	public BlurPostEffect blurPostEffect;
	private Camera cam;
	private Shader shader;
	private Shader defaultShader;
	public Renderer target;
//	public RenderTexture mapDisplay;
	public int blurFrame = 1;


	Matrix4x4 lastMV;
	Matrix4x4 lastMVP;
	Queue<Matrix4x4> queue;

	[HideInInspector]
	public RenderTexture velocityTexture;

	void Start(){
		cam = GetComponent<Camera>();
		shader = Shader.Find("Custom/BlurObject");
//		shader = Shader.Find("Custom/basic");

		velocityTexture = new RenderTexture((int)cam.pixelWidth, (int)cam.pixelHeight, 32, RenderTextureFormat.ARGBFloat);
		velocityTexture.Create();

		cam.targetTexture = velocityTexture;
		cam.enabled = false;

//		rtvelocityTextureCreate();

		queue = new Queue<Matrix4x4>();
		defaultShader = target.material.shader;
//		target.material.shader = shader;

//		lastMV = GetMVP();
//		materials = GetComponent<Renderer>().materials;
		//set shader
//		blurShader = Shader.Find("Custom/BlurTest");
//		foreach(Material m in materials){
//			m.shader = blurShader;
//		}

	}
	
	void Update(){
		cam.CopyFrom(Camera.main);
		if(cam.targetTexture==null)cam.targetTexture = velocityTexture;
		//calculate MVP


		Matrix4x4 MV = GetMVP();
		if(queue.Count > blurFrame)
			lastMV = queue.Dequeue();
		else
			lastMV = MV;
		
		//set to shader
//		Shader.SetGlobalMatrix("_CurMV",MV);
//		Shader.SetGlobalMatrix("_PrevMV",lastMV);
		target.material.shader = shader;
		target.material.SetMatrix("_CurMV",MV);
		target.material.SetMatrix("_PrevMV",lastMV);
		
		queue.Enqueue( MV );

		//renderer velocity map
		RendererWithBlur();

		target.material.shader = defaultShader;


	}


	Matrix4x4 GetMVP(){
		//HERE TARGEIT IS NOT CORRECT
		Matrix4x4 M = target.transform.localToWorldMatrix; //transform target object to world matriz
		Matrix4x4 V = cam.worldToCameraMatrix; //transform matrix of World to matirix of Camera
		Matrix4x4 P = cam.projectionMatrix; //to screen coordinate
		Matrix4x4 MVP = P * V * M;
		return MVP;
	}
	Matrix4x4 GetMV(){
		Matrix4x4 M = target.transform.localToWorldMatrix; //transform target object to world matriz
		Matrix4x4 V = cam.worldToCameraMatrix; //transform matrix of World to matirix of Camera
		Matrix4x4 MV = V * M;
		return MV;
	}

	//	void OnRenderImage(RenderTexture source, RenderTexture dest) {
//		Material mat = blurMaterial;
//		mat.shader = blurShader;
//		mat.SetTexture("_VelocityTex",velocityMap);
//		mat.SetMatrix("_CurMV",MV);
//		mat.SetMatrix("_PrevMV",lastMV);
//		
//		//		int rtW = source.width/4;
//		//		int rtH = source.height/4;
//		//		tex = RenderTexture.GetTemporary(rtW, rtH, 0);
//		
//		
//		Graphics.Blit(source, dest, mat);
//	}

	public void RendererWithBlur(){
//		RenderTexture buffer = RenderTexture.GetTemporary(500, 500, 0);
//		camera.targetTexture = buffer;

		RenderTexture currentRT = RenderTexture.active;
		RenderTexture.active = cam.targetTexture;

//		cam.RenderWithShader(shader,"");
		cam.Render();
//		cam.RenderWithShader(shader);
//		Debug.Log("rendered !!");

//		Texture2D image = new Texture2D(cam.targetTexture.width, cam.targetTexture.height);
//		image.ReadPixels(new Rect(0, 0, cam.targetTexture.width, cam.targetTexture.height), 0, 0);
//		image.Apply();

		RenderTexture.active = currentRT;
//		gUITexture.texture = image;
//		mapDisplay.material.mainTexture = image;

//		Graphics.Blit(buffer, mapDisplay);
//		Graphics.Blit(buffer, velocityTexture);


//		velocityTexture = cam.targetTexture;

//		RenderTexture.ReleaseTemporary(buffer);

//		return image;

	}

}
