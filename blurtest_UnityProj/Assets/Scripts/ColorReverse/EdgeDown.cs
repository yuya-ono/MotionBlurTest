using UnityEngine;
using System.Collections;

public class EdgeDown : MonoBehaviour {
	
	public Material material;
	private RenderTexture tex;

	void Start(){
		if(material == null){
			material = Resources.Load<Material>("Debug/Materials/"+"EdgeDown");
		}
	}

	// Downsamples the texture to a quarter resolution.
	private void DownSample4x (RenderTexture source, RenderTexture dest)
	{
		Material mat = material;
		mat.SetTexture("_MainTex", tex);
		Graphics.Blit (source, dest, mat);
	}
	
	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		
		Material mat = material;
		
		int rtW = source.width/4;
		int rtH = source.height/4;
		//		RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
		tex = RenderTexture.GetTemporary(rtW, rtH, 0);
		//		tex = source;
		//		DownSample4x (source, buffer);
		
		//		Material mat = material;
		
		//		mat.SetTexture("_MainTex", tex);
		
		Graphics.Blit(source, dest, mat);
	}
	
}
