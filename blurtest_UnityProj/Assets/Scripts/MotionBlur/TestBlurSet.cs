using UnityEngine;
using System.Collections;

public class TestBlurSet : MonoBehaviour {

	public GameObject[] setBlurObject;
	public BlurEffectController blurEffectController;

	// Use this for initialization
	void Start () {
		foreach(GameObject go in setBlurObject){
			blurEffectController.Add( go );
		}
	}

}
