using UnityEngine;
using System.Collections;

public class ShaderUpdatesSpineColor : ShaderUpdates {

	public float brightness = 0.5f;
	public float alpha = 1.0f;
	public Color color = Color.white;
	public Shader shader;

	public override void Init () {
		shader = Shader.Find("Custom/SkeletonCustom");	
	}

	protected override void UpdateEach (Material m) {
		m.shader = shader;
		m.SetFloat("_Brightness",brightness);
		m.SetFloat("_Alpha",alpha);
		m.SetColor("_Color",color);
	}

}
