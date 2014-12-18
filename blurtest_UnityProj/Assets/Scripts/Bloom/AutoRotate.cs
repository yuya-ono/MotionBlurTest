using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up,Time.deltaTime * 100.0f);
	}
}
