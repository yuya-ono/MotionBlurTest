using UnityEngine;
using System.Collections;

public class ShaderUpdatesSpineDual : ShaderUpdates {

	private Camera cam;
	public float size = 1.0f;
	public float alpha = 1.0f;

	public override void Init () {
		shader = Shader.Find("Custom/Dual");	
		cam = Camera.main;
	}

	protected override void UpdateEach (Material m) {
		m.shader = shader;
		m.SetFloat("_Alpha", alpha );
		m.SetMatrix("_SizedMVP", GetSizedMVP(size) );
	}

	private Matrix4x4 GetSizedMVP(float scale){
		//HERE TARGEIT IS NOT CORRECT
//		Quaternion rotation = Quaternion.Euler(target.localEulerAngles.x,target.localEulerAngles.y,target.localEulerAngles.z);
		Matrix4x4 M = target.localToWorldMatrix * Matrix4x4.Scale(new Vector3(scale,scale,scale));//Matrix4x4.TRS (target.localPosition, rotation, target.localScale * scale);
		Matrix4x4 V = cam.worldToCameraMatrix; //transform matrix of World to matirix of Camera
		Matrix4x4 P = cam.projectionMatrix; //to screen coordinate
		Matrix4x4 MVP = P * V * M;
		return MVP;
	}

}
