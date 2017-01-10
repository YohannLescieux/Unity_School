using UnityEngine;
using System.Collections;

public class InteractionSouris : MonoBehaviour {
	Camera camera;
	GUIText myText;
    GUIText info;
	public GameObject guitext;
    public GameObject informations;
	private GameObject monObjet;
	private GameObject monSaveObjet;
	private Plane monPlan;

	// Use this for initialization
	void Start () {
		monObjet = null;
		camera = GetComponent<Camera>();
		myText = guitext.GetComponent<GUIText>();
		myText.transform.position = new Vector3(0.8f,0.9f,0.0f);
		myText.fontSize = 20;
        info = informations.GetComponent<GUIText>();
        info.transform.position = new Vector3(0.0f, 0.2f, 0.0f);
        info.fontSize = 15;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			//On récupère le ray
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) {
				monObjet = hit.collider.gameObject;
				myText.text = monObjet.name+"\n"+monObjet.transform.position;
			}
		}

		//Etape 2
		/*if (Input.GetMouseButton (1) && Input.GetKey(KeyCode.RightShift)) {
			Vector3 v3Prev = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.z);
			v3Prev = Camera.main.ScreenToWorldPoint(v3Prev);
			v3Prev.y = 0.0f;
			monObjet.transform.position = v3Prev;
		}*/

		//Etape 3
		if (Input.GetMouseButton (1) && Input.GetKey(KeyCode.RightControl)) {
			monPlan = new Plane (Camera.main.transform.forward, monObjet.transform.position);
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float ray_distance;
			if (monPlan.Raycast (ray, out ray_distance)) {
				monObjet.transform.position = ray.origin + ray.direction * ray_distance;
			}
			myText.text = monObjet.name+"\n"+monObjet.transform.position;
		}
		if (Input.GetMouseButtonUp (1)) {
			monObjet = null;
		}
	}
}