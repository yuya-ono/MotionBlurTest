using UnityEngine;
using System.Collections;
using System;

public enum SpineShaderType{
	Dual,ColorEdit, None
}

public class ShaderExchangeUtil : MonoBehaviour {
	public SpineShaderType spineShaderType;
	public ShaderUpdates shaderUpdates{get; private set;}
	private SkeletonRenderer skeletonRenderer;
	
	// Use this for initialization
	public ShaderUpdates Create (SpineShaderType shaderType = SpineShaderType.None) {
		spineShaderType = shaderType;
		if(spineShaderType == SpineShaderType.None){
			Destroy(this);
			return null;
		}

		if(skeletonRenderer==null)skeletonRenderer = gameObject.GetComponent<SkeletonRenderer>();

		//create class
		CreateShaderUpdatesClass( spineShaderType );

		if(shaderUpdates==null){
			Destroy(this);
			return null;
		}

		//set property
		shaderUpdates.target = transform;
		shaderUpdates.Init();
		return shaderUpdates;
	}
	
	// Update is called once per frame
	void Update () {
		skeletonRenderer.MaterialPostFix( (mats) =>{
			shaderUpdates.Update( mats );
		});
	}
	
	void OnDestroy(){
		if(skeletonRenderer!=null)
			skeletonRenderer.DestroyPostMaterial();
	}

	private bool CreateShaderUpdatesClass(SpineShaderType shaderType){
		string classname = GetClassName( shaderType );
		if(classname == "") return false;

		Type type = Type.GetType( classname );
		shaderUpdates = (ShaderUpdates)Activator.CreateInstance( type );

		return true;
	}

	private string GetClassName(SpineShaderType shaderType){
		if(shaderType == SpineShaderType.ColorEdit)
			return "ShaderUpdatesSpineColor";
		if(shaderType == SpineShaderType.Dual)
			return "ShaderUpdatesSpineDual";
		else
			return "";
	}
	
}
