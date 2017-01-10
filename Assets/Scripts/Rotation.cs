using UnityEngine;
using System.Collections;
using System;

public class Rotation : MonoBehaviour {
    public float vitesse = 20;

	// Use this for initialization
	void Start () {
        Debug.Log("Objet : " + gameObject.name, gameObject);
	}
	
	// Update is called once per frame
	void Update () {
      //this.transform.Rotate(new Vector3(0,1,0) * vitesse * Time.deltaTime);
			//down va faire tourner notre sphere dans le sens inverse des aiguilles
		this.transform.Rotate(Vector3.down * vitesse * Time.deltaTime);
	}
}
