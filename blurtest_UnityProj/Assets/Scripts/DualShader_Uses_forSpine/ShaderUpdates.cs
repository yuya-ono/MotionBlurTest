using UnityEngine;
using System.Collections;

public class ShaderUpdates {

	public Shader shader;
	public Transform target;

	public virtual void Init () { }

	public virtual void Update (Material[] mats) {
		foreach(Material m in mats){
			if(m!=null)
				UpdateEach( m );
		}
	}

	protected virtual void UpdateEach (Material m) {
		m.shader = shader;
	}

}
