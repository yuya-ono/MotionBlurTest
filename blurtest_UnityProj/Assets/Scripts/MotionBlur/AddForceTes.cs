using UnityEngine;
using System.Collections;

public class AddForceTes : MonoBehaviour {
	
	Rigidbody rigid;
	bool isUp = false;
	bool isRight = false;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(rigid.velocity.magnitude <= 10){
			float y = isUp ? 1000 : -1000;
			float x = isRight ? 1000 : -1000;
			rigid.AddForce( new Vector3(x, y, 0) );
			isUp = !isUp;
			isRight = !isRight;
		}
	}
}
