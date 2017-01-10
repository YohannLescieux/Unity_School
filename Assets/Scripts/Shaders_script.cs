using UnityEngine;
using System.Collections;

public class Shaders_script : MonoBehaviour {
	public float hSliderValue = 0.5F;
	Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		rend.material.SetFloat ("_SliderValue",hSliderValue);
	}

	//GuiEvent
	void OnGUI() {
		GUI.Label(new Rect(0, 0, 400, 40), "Démonstration d'un shader de surface qui mélange deux textures. La quantité de mélange est définie à l'aide d'une fonction linéaire");
		GUI.Label (new Rect (0, 40, 400, 40), "Texture 1 <- ");
		hSliderValue = GUI.HorizontalSlider(new Rect(75, 45, 100, 30), hSliderValue, 0.0F, 1.0F);
		GUI.Label (new Rect (175, 40, 400, 40), " -> Texture 2");

		GUI.Label (new Rect (5, 500, 1500, 800), "- Shader : Un programme exécuté sur le GPU" +
            "\n- Surface Shader : Dans unity, rend beaucoup plus facile l'écriture de shaders grâce à une approche de génération de code" +
            "\n- Property : Lien entre le code d'un shader et les paramètres sous Unity" +
            "\n- Accéder propriété à partir d'un script Basique CS par : Renderer rend; rend = GetComponent<Renderer> (); rend.material.SetFloat (\"_SliderValue\",hSliderValue);" +
            "\n\tAu niveau du surface Shader : Properties {\n\t\t_SliderValue (\"SliderValue\", Range (0, 1) ) = 0.0 \n\t}");
	}
}
