using UnityEngine;
using System.Collections;

public class ExplostionBlur_Test : MonoBehaviour {

	GameObject camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");

	}

	void Update(){

		if (Input.GetMouseButtonUp (0)) {
			Vector2 touchPosition = new Vector2( Input.mousePosition.x/Screen.width,  Input.mousePosition.y/Screen.height);
			AddExplosionBlur( touchPosition );
			Debug.Log(touchPosition);
		}



	}

	void AddExplosionBlur(Vector2 screenPos){
		ExplosionBlur explosionBlur = camera.AddComponent<ExplosionBlur> ();
		explosionBlur.centerX = screenPos.x;
		explosionBlur.centerY = screenPos.y;
	}

}
