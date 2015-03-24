using UnityEngine;
using System.Collections;

public class Heat : MonoBehaviour {

	PostEffectCamera post;
	Material heatMat;

	void Start(){
		heatMat = new Material( Shader.Find("Custom/Heat") );
		post = GameObject.Find("PostEffectCamera").GetComponent<PostEffectCamera>();
	}

	void HeatFilter(RenderTexture source, RenderTexture heatTex, RenderTexture dest){
		//set x y movement speed
		heatMat.SetTexture("_MainTex",source);
		heatMat.SetTexture("_HeatMapTex",heatTex);
		Graphics.Blit( source, dest, heatMat);
	}

	void OnRenderImage(RenderTexture source, RenderTexture dest) {

		int rtW = source.width;
		int rtH = source.height;
		
		//****** HEAT  ****** OK
		RenderTexture bufferHeat = RenderTexture.GetTemporary(rtW, rtH, 0);
		HeatFilter(source, post.HeatMapTexture, bufferHeat);
		
		Graphics.Blit( bufferHeat, dest);
		
		RenderTexture.ReleaseTemporary( bufferHeat );
		
	}

}
