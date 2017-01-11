using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
    public GameObject cible;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(cible.transform);
        this.transform.position = Vector3.MoveTowards(this.transform.position, cible.transform.position, 3);
	}
}
