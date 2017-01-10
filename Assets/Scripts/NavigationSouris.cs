using UnityEngine;
using System.Collections;

public class NavigationSouris : MonoBehaviour {
    Vector3 oldCameraPos, hitPos;
    Vector3 oldMousePos, mousePos;
    bool objectVise;

    float vitRotation = 1.0f;
    float maxVitRotation = 5.0f;

    GUIText info;
    public GameObject informations;

	// Use this for initialization
	void Start () {
        objectVise = false;
        info = informations.GetComponent<GUIText>();
        info.transform.position = new Vector3(0.0f, 0.05f, 0.0f);
        info.fontSize = 15;
	}
	
	// Update is called once per frame
	void Update () {
		//Nous ne "tenons" plus d'objet en main
		if (Input.GetMouseButtonUp(0)) {
            objectVise = false;
		}
        //On rotate proportionnelement à la distance entre notre souris et notre position initiale (Navigation par cible)
		if (Input.GetMouseButton (0)){
            if (!objectVise)
            {
                //Raycast vers un object
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                //Si hit sur un objet
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject)
                {
                    objectVise = true;
                    hitPos = hit.point;
                    oldMousePos = Input.mousePosition;
                    oldCameraPos = Camera.main.transform.position;
                }

            } else {
                //Transformation pourcentage
                float pourcentage;
                if (Input.mousePosition.y > oldMousePos.y)
                {
                    pourcentage= 0.0f;
                } else if (Input.mousePosition.y < 0.0f) {
                    pourcentage = 1.0f;
                } else {
                    pourcentage = 1 - (Input.mousePosition.y / oldMousePos.y);
                }
                //Transformation de la position de la caméra
                Camera.main.transform.position = oldCameraPos + (hitPos - oldCameraPos) * pourcentage;
            }
		}


        //Rotation
        if (Input.GetMouseButtonDown(1)) {
            oldMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - oldMousePos;
            //Axe y
            float vitesseY = Mathf.Clamp(delta.y * vitRotation * Time.deltaTime, -maxVitRotation, maxVitRotation);
            float newRotation = Camera.main.transform.eulerAngles.x - vitesseY;
            if (newRotation > 270f || newRotation < 90f)
            {
                Camera.main.transform.RotateAround(this.transform.position, this.transform.right, -vitesseY);
            }
            //Axe x
            float vitesseX = Mathf.Clamp(delta.x * vitRotation * Time.deltaTime, -maxVitRotation, maxVitRotation);
            this.transform.RotateAround(this.transform.position, Vector3.up, vitesseX);
        }
    }
}
