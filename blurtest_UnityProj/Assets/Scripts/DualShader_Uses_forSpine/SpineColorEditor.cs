using UnityEngine;
using System.Collections;

public class SpineColorEditor : MonoBehaviour {

	public float brightness = 0.5f;
	public float alpha = 1.0f;
	public Color color = Color.white;
	public Shader shader;
	private float size = 1.0f;

	private SkeletonRenderer skeletonRenderer;
//	private MaterialPropertyBlock materialPropertyBlock ;

	// Use this for initialization
	void Start () {
		if(skeletonRenderer==null)skeletonRenderer = gameObject.GetComponent<SkeletonRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		skeletonRenderer.MaterialPostFix( (mats) =>{
			foreach(Material m in mats){
				if(m!=null){
					m.shader = shader;
					if(shader.name == "Custom/Dual"){
						if(size>=0)
							size -= 0.01f;
						m.SetFloat("_Size",size);
						m.SetFloat("_Alpha",0.3f);
						m.SetColor("_Color",color);
					}else{
						m.SetFloat("_Brightness",brightness);
						m.SetFloat("_Alpha",alpha);
						m.SetColor("_Color",color);
					}
				}
			}
		});
	}

	void OnDestroy(){
		if(skeletonRenderer!=null)
			skeletonRenderer.DestroyPostMaterial();
	}
	
}
