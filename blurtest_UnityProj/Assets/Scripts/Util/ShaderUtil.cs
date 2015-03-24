using UnityEngine;
using System.Collections;

public static class ShaderUtil {

	public static Matrix4x4 GetMVP(Camera cam, Transform target){
		Matrix4x4 M = target.localToWorldMatrix; //transform target object to world matriz
		Matrix4x4 V = cam.worldToCameraMatrix; //transform matrix of World to matirix of Camera
		Matrix4x4 P = cam.projectionMatrix; //to screen coordinate
		Matrix4x4 MVP = P * V * M;
		return MVP;
//		Matrix4x4 M = Matrix4x4.TRS( target.position, target.rotation, target.localScale);// target.localToWorldMatrix; //transform target object to world matriz
//		Matrix4x4 V = cam.worldToCameraMatrix; //transform matrix of World to matirix of Camera
//		Matrix4x4 P = cam.projectionMatrix; //to screen coordinate
//		Matrix4x4 MVP = P * V * M;
////		Matrix4x4 MVP = V * M;
//		return MVP;
	}

//	Matrix4x4 modelViewMatrix = Camera.mainCamera.worldToCameraMatrix * Matrix4x4.TRS( transform.position, transform.rotation, transform.localScale);

	public static Matrix4x4 GetMV(Camera cam, Transform target){
		Matrix4x4 M = target.localToWorldMatrix; //transform target object to world matriz
		Matrix4x4 V = cam.worldToCameraMatrix; //transform matrix of World to matirix of Camera
		Matrix4x4 MV = V * M;
		return MV;
	}
}
