using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurPostEffect : MonoBehaviour {

	public BlurEffectController blurEffectController;
//	public RenderTexture velocityMap;
//	public Rect rtSize;
//	public GUITexture gUITexture;
//	public int blurFrame = 1;
	GameObject target;
//	Matrix4x4 lastMV;
//	Matrix4x4 lastMVP;

//	Texture velocityMap;

	RenderTexture rTex;
	Material[] materials;
	Material blurMaterial;
	Shader blurShader;


	void Start(){
		//set shader
		blurShader = Shader.Find("Custom/BlurTest");
//		blurShader = Shader.Find("Custom/basic");
		blurMaterial = Resources.Load<Material>("Materials/blurtest");
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture dest) {

		Material mat = blurMaterial;
		mat.shader = blurShader;
		mat.SetTexture("_VelocityTex",blurEffectController.velocityTexture);
		
//		Graphics.Blit(blurObject.velocityTexture, dest);
		Graphics.Blit(source, dest, mat);
//		Graphics.Blit(source, dest);
	}
	
}
