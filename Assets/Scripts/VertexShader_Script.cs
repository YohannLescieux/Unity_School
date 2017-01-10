using UnityEngine;
using System.Collections;

public class VertexShader_Script : MonoBehaviour {
	private float hSliderValue = 0.0f;
	Renderer rendArms, rendBody, rendHead;


	// Use this for initialization
	void Start () {
		rendArms = GameObject.Find ("armorArms").GetComponent<Renderer>();
		rendBody = GameObject.Find ("armorBody").GetComponent<Renderer>();
		rendHead = GameObject.Find ("head").GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {
		rendArms.material.SetFloat ("_SliderValue",hSliderValue);
		rendBody.material.SetFloat ("_SliderValue",hSliderValue);
		rendHead.material.SetFloat ("_SliderValue",hSliderValue);
	}

	void OnGUI() {
		GUI.Label(new Rect(0, 0, 400, 40), "Vertex Shader");
		GUI.Label (new Rect (0, 15, 400, 40), "Fin <- ");
		hSliderValue = GUI.HorizontalSlider(new Rect(40, 20, 100, 30), hSliderValue, -0.05f, 0.05f);
		GUI.Label (new Rect (145, 15, 400, 40), " -> Gros");
        GUI.Label (new Rect (5, 600, 1000, 800), "- Vertex shader : Au sein du pipeline graphique, calcule les transformations à appliquer aux sommets");
	}
}
