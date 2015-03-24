using UnityEngine;
using System.Collections;

public class Nothing : MonoBehaviour {

	void Start(){

	}

	
	void OnRenderImage(RenderTexture source, RenderTexture dest) {
		Graphics.Blit(source, dest);
	}
}
