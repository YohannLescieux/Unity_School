//CAMERA MOVE AROUND SCENE (Caméra qui tourne autour d'une cible précise\\
using UnityEngine;
using System.Collections;

public class CameraMoveAroundScene : MonoBehaviour
{
    public GameObject cible;
    public float angle = 2f;

    //use : Vector3.up ; transform.position ; Transform.RotateAround(..) ; Transform.LookAt(..)
    // Use this for initialization
    void Start()
    {
        Debug.Log("Objet : " + gameObject.name, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(cible.transform);
        //Up va faire tourner la caméra dans le sens des aiguilles
        this.transform.RotateAround(cible.transform.position, Vector3.up, angle * Time.deltaTime);
        //this.transform.RotateAround(cible.transform.position, new Vector3(0,1,0), angle * Time.deltaTime);
    }
}




// ROTATION OBJET (un objet tourne)\\
using UnityEngine;
using System.Collections;
using System;

public class RotationObjet : MonoBehaviour {
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




// Animation portes
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




// Caméra dual point
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





// Interaction Souris, on clique sur un objet pour afficher son nom, peut déplacer l'objet
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





// Navigation souris, on clique sur objet, baisse la souris = zoom vers l'objet
// Rotation aussi de la caméra a une certaine vitesse

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




// Shader script
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



//Vertex shader
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




//Surface shader
Shader "Lescieux/SurfaceShader" {
    Properties {
        _MainTex ("First Texture", 2D) = "texture1" {}
            _SecondTex ("Other Texture", 2D) = "texture2" {}
            _SliderValue ("SliderValue", Range (0, 1) ) = 0.0 
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        //Pour rendu des shaders, utilisation de surf
        //CS SHADERG SHEET
        CGPROGRAM
        #pragma surface surf Lambert

        //Mettre meme nom que dans properties (Sampler2D = texture)
        sampler2D _MainTex;
        sampler2D _SecondTex;
        float _SliderValue;

        struct Input {
            float2 uv_MainTex;
            float2 uv_SecondTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            //Base
            //_MainTex à coord uv Maintex (retourne une couleur)
            half4 firstTex = tex2D (_MainTex, IN.uv_MainTex);
            half4 secondTex = tex2D (_SecondTex, IN.uv_SecondTex);

            //Récupérer float de Slider
            //Combinaison linéaire entre les deux textures
            //Ajouter à albedo
            o.Albedo = firstTex.rgb * (1.0 - _SliderValue) + secondTex.rgb * _SliderValue;
            o.Alpha = firstTex.a;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}