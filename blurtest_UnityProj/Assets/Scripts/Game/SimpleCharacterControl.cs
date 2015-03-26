using UnityEngine;
using System.Collections;

public class SimpleCharacterControl : MonoBehaviour {


	public float speed = 1.0f;
	public float horizontalSpeed = 1.0f;
	public float verticalSpeed = 1.0f;
	public float cameraSpeed = 2.0f;
	public Camera characterCamera;
	
	public const float CAMERA_ANG_MAX_Y =  80.0f;
	public const float CAMERA_ANG_MIN_Y = -80.0f;

	private Transform characterCameraTransform;

	// Use this for initialization
	void Start () {
		characterCameraTransform = characterCamera.transform;
	}
	
	// Update is called once per frame
	void Update () {

		float translation = Input.GetAxis("Vertical") * speed;
		float rotation = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		transform.Translate(0, 0, translation);


		float h = cameraSpeed * horizontalSpeed * Input.GetAxis("Mouse X");
		float v = cameraSpeed * verticalSpeed * Input.GetAxis("Mouse Y");
		if (characterCameraTransform.localRotation.y >= CAMERA_ANG_MAX_Y && v >= 0) {
			v = 0;
		}
		if (characterCameraTransform.localRotation.y <= CAMERA_ANG_MIN_Y && v <= 0) {
			v = 0;
		}
		characterCameraTransform.Rotate(v, h, 0);
		Vector3 rot = characterCameraTransform.localEulerAngles;
		characterCameraTransform.localEulerAngles = new Vector3 (rot.x, rot.y, 0);

	}
}
