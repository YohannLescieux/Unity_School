using UnityEngine;
using System.Collections;

public class NavigationFPS : MonoBehaviour {
	private float angle = 80.0f;
	private Animation anim;
	public GameObject myHead;
	private Camera cam; 	

	//Mouse
	public float hSpeed = 15.0f;
	public float vSpeed = 15.0f;
	private float horizontal = 0.0f;
	private float vertical = 0.0f;
	private Quaternion test;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animation>();
		cam = myHead.GetComponent<Camera> ();
		test = cam.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		//Souris
		//Mouse axe X & Y
		horizontal += hSpeed * Input.GetAxis("Mouse X");
		vertical += vSpeed * Input.GetAxis("Mouse Y");
		//Permet d'avoir un angle correct sur 360°
		horizontal = ClampAngleX (horizontal, -360.0f, 360.0f);
		vertical = ClampAngleX (vertical, -60.0f, 60.0f);
		Quaternion xQuaternion = Quaternion.AngleAxis (horizontal, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (vertical, -Vector3.right);
		cam.transform.localRotation =  test * xQuaternion * yQuaternion;

		//Controle vitesse
		/*if (Input.GetMouseButton (0)) {
			
		}*/

		//Clavier
		//Mouvements perso (On test si une touche est préssé sinon IdleRelaxed)
		if (Input.anyKey) {
			if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightControl))
			{
				anim["soldierRun"].speed = 1;
				anim.CrossFade("soldierSprint");
				this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * 3.0f);
			}
			if (Input.GetKey (KeyCode.UpArrow)) {
				anim["soldierRun"].speed = 1;
				anim.CrossFade ("soldierWalk");
				this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				anim["soldierRun"].speed = -1;
				anim.CrossFade ("soldierWalk");
				this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
			}

			if (Input.GetKey (KeyCode.RightArrow)) {
				anim.CrossFade ("soldierSpinRight");
				this.transform.Rotate(new Vector3(0,1,0), angle * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				anim.CrossFade ("soldierSpinLeft");
				this.transform.Rotate(new Vector3(0,-1,0), angle * Time.deltaTime);
			} 
		} else {
			anim.CrossFade ("soldierIdleRelaxed");
		}
	}

	//Permet d'avoir le rotate correct (en X)
	public static float ClampAngleX (float angle, float min, float max)
	{
		if (angle == -360F)
			angle += 360F;
		if (angle == 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}

	//Permet d'avoir le rotate correct (en X)
	public static float ClampAngleY (float angle, float min, float max)
	{
		if (angle == -60F)
			angle += 60F;
		if (angle == 60F)
			angle -= 60F;
		return Mathf.Clamp (angle, min, max);
	}
}
