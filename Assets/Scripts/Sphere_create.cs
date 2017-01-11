using UnityEngine;
using System.Collections;

public class Sphere_create : MonoBehaviour {
	Camera camera;
	GameObject clone;
	bool test = false;
	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//On prend en compte le clic souris gauche && le lancer de rayon sur un objet precis
		if (Input.GetMouseButton (0)) {
			//On récupère le ray
			test = true;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit) && test == true) {
				GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				sphere.AddComponent<Rigidbody> ();
				Vector3 temp = hit.point;
				temp.z = -0.99f;
				sphere.GetComponent<Renderer> ().material.color = Random.ColorHSV (0f, 1f, 1f, 1f, 0.5f, 1f);
				sphere.transform.position = temp;

				//clone = (GameObject)Instantiate(sphere, hit.point, Quaternion.identity);
			}
		} else {
			test = false;
		}
	}
}
