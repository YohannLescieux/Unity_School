using UnityEngine;
using System.Collections;

public class Scene_fragment_script : MonoBehaviour {
	float hSliderValue = 0.05f;
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		rend.material.SetFloat ("_SliderValue",hSliderValue);
	}

	void OnGUI() {
		GUI.Label(new Rect(0, 0, 400, 40), "Vertex & Fragment Shader");
		hSliderValue = GUI.HorizontalSlider(new Rect(20, 25, 100, 30), hSliderValue, 0.05f, 5.0f);
	}
}
    