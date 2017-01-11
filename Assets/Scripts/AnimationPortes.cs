using UnityEngine;
using System.Collections;

public class AnimationPortes : MonoBehaviour {
	private Animation myAnimation;
	public GameObject objet;
	private bool doorsOpen = false;

	// Use this for initialization
	void Start () {
		myAnimation = objet.GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && doorsOpen == false && !myAnimation.isPlaying) {
			myAnimation.Play("Portes_ouvertes");
			doorsOpen = true;
				}
		if (Input.GetMouseButton(1) && doorsOpen == true && !myAnimation.isPlaying) {
			myAnimation.Play("Portes_fermees");
			doorsOpen = false;
				}
	}
}
