using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera)),
ExecuteInEditMode]
public class CameraDualPoint : MonoBehaviour {
	Camera cam;
	public float speed = 2.0f;
	private float zoomSpeed = 2.0f;

	public float minX = -360.0f;
	public float maxX = 360.0f;
	
	public float minY = -45.0f;
	public float maxY = 45.0f;
	
	public float sensX = 100.0f;
	public float sensY = 100.0f;
	
	float rotationY = 0.0f;
	float rotationX = 0.0f;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Avancer caméra
		if (Input.GetKey (KeyCode.LeftArrow)) {
			cam.transform.position += Vector3.left * Time.deltaTime * speed;
		} 
		if (Input.GetKey (KeyCode.RightArrow)) {
			cam.transform.position += Vector3.right * Time.deltaTime * speed;
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			cam.transform.position += Vector3.forward  * Time.deltaTime * speed;
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			cam.transform.position += Vector3.back * Time.deltaTime * speed;
		}

		//Descendre ou monter
		float scroll = Input.GetAxis("Mouse ScrollWheel");
		transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);


		if (Input.GetMouseButton (0)) {
			rotationX += Input.GetAxis ("Mouse X") * sensX * Time.deltaTime;
			rotationY += Input.GetAxis ("Mouse Y") * sensY * Time.deltaTime;
			rotationY = Mathf.Clamp (rotationY, minY, maxY);
			cam.transform.localEulerAngles = new Vector3 (-rotationY, rotationX, 0);
		}
	}
}