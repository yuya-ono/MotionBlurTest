using UnityEngine;
using System.Collections;
//using Holoville.HOTween;

public class ExplosionBlur : MonoBehaviour {
	public bool isExplode = true;
	public float maxExpPower = 0.1f;
	public float expPower = 0.0f; //blur power
	public float centerX = 0.5f; 
	public float centerY = 0.7f; 
	public float time = 0.5f;
	Material mat;

	void Start(){
		Shader shd = (isExplode) ? Shader.Find("Custom/ExplosionBlur") : Shader.Find("Custom/ExplosionBlurInside");
		mat = new Material( shd );
		mat.SetFloat("_CenterTexelX",centerX);
		mat.SetFloat("_CenterTexelY",centerY);
//		HOTween.To (this, time, new TweenParms ().Prop ("expPower", maxExpPower).OnComplete(ExplosionCompleteHandler));
	}

	void ExplosionCompleteHandler(){
		Destroy ( this );
	}

	// explosion filter
	private void ExplosionFilter (RenderTexture source, RenderTexture dest)
	{
		mat.SetFloat("_Power",expPower);
		Graphics.Blit(source, dest, mat);
	}
		// Update is called once per frame
	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		int rtW = source.width;
		int rtH = source.height;
		
		//****** BLUR  ****** OK
		RenderTexture bufferBlur = RenderTexture.GetTemporary(rtW, rtH, 0);
		ExplosionFilter(source, bufferBlur);

		Graphics.Blit( bufferBlur, dest);

		RenderTexture.ReleaseTemporary( bufferBlur );

	}
}

