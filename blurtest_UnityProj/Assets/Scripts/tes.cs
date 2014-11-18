using UnityEngine;
using System.Collections;

public class tes : MonoBehaviour {
	Shader shader;
	Material material;
	// Use this for initialization
	void Start () {
		shader = Shader.Find("Custom/BlurObject");
		material = renderer.material;
	}
	
	// Update is called once per frame
	void Update () {

		Matrix4x4 MVP = ShaderUtil.GetMVP( Camera.main, transform );

		material.shader = shader;
		material.SetMatrix("_CurMV",MVP);
		material.SetMatrix("_PrevMV",MVP);
	}
}
