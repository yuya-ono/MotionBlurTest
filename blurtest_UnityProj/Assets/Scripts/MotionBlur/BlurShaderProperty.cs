using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurShaderProperty {

	public Material material;
	public Shader defaultShader;
	public Transform trans;
	public Matrix4x4 curMVP;
	public Matrix4x4 lastMVP;
	public Queue<Matrix4x4> queue;
	
	public void UseDefaultShader(){
		material.shader = defaultShader;
	}

}