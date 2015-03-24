using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlurEffect : MonoBehaviour {

	public Camera velocityCamera;
	public Shader blurShader;

	private List<BlurShaderProperty> blurMaterials;
	private List<Material> materials;
	
	void Start(){
		//Get list of materials used in this gameobjects
		blurMaterials = new List<BlurShaderProperty>();
		foreach(Renderer rend in gameObject.GetComponentsInChildren<Renderer>()){
			foreach(Material mat in rend.materials){
				BlurShaderProperty blurShaderProperties = new BlurShaderProperty();
				blurShaderProperties.material      = mat;
				blurShaderProperties.defaultShader = mat.shader;
				blurShaderProperties.trans         = rend.transform;
				blurShaderProperties.queue         = new Queue<Matrix4x4>();
				blurMaterials.Add( blurShaderProperties );
			}
		}
	}

	//call before renderering velocity map
	public void SwitchToBlurShader(){
		foreach(BlurShaderProperty prop in blurMaterials){
			Matrix4x4 MVP = ShaderUtil.GetMV( velocityCamera, prop.trans );
			if(prop.queue.Count > BlurEffectController.blurFrame)
				prop.lastMVP = prop.queue.Dequeue();
			else
				prop.lastMVP = MVP;
			//set
			prop.material.shader = blurShader;
			prop.material.SetMatrix("_CurMV",MVP);
			prop.material.SetMatrix("_PrevMV",prop.lastMVP);
			prop.material.SetMatrix("_CurP",velocityCamera.projectionMatrix);

			prop.queue.Enqueue( MVP );
		}
	}

//	public void SwitchToBlurShader(){
//		foreach(BlurShaderProperty prop in blurMaterials){
//			Matrix4x4 MVP = ShaderUtil.GetMVP( velocityCamera, transform );
//			//set
//			prop.material.shader = blurShader;
//			prop.material.SetMatrix("_CurMV",MVP);
//			prop.material.SetMatrix("_PrevMV",MVP);
//		}
//	}


	//call after renderering velocity map
	public void PutBackToDefaultShader(){
		foreach(BlurShaderProperty prop in blurMaterials){
			prop.UseDefaultShader();
		}
	}

}
