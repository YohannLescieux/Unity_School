using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour {
	public GameObject myObject;
	private Animation anim;
	public float angle = 80.0f;
    public float vitesseCourse = 2.0f;

	// Use this for initialization
	void Start () {
		anim = myObject.GetComponent<Animation> ();
		anim.CrossFade("soldierIdleRelaxed");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftShift))
        {
            anim["soldierRun"].speed = 1;
            anim.CrossFade("soldierSprint");
            this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime * vitesseCourse);
        }
		else if (Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.UpArrow)) {
			anim["soldierRun"].speed = 1;
			anim.CrossFade("soldierRun");
			this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
			this.transform.Rotate(new Vector3(0,-1,0), angle * Time.deltaTime);
		}
		else if (Input.GetKey (KeyCode.RightArrow) && Input.GetKey (KeyCode.UpArrow)) {
			anim["soldierRun"].speed = 1;
			anim.CrossFade("soldierRun");
			this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
			this.transform.Rotate(new Vector3(0,1,0), angle * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.LeftArrow) && Input.GetKey (KeyCode.DownArrow)) {
			anim["soldierRun"].speed = -1;
			anim.CrossFade("soldierRun");
			this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
			this.transform.Rotate(new Vector3(0,-1,0), angle * Time.deltaTime);
		}
		else if (Input.GetKey (KeyCode.RightArrow) && Input.GetKey (KeyCode.DownArrow)) {
			anim["soldierRun"].speed = -1;
			anim.CrossFade("soldierRun");
			this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
			this.transform.Rotate(new Vector3(0,1,0), angle * Time.deltaTime);
		}
		//Avancer
		else if (Input.GetKey (KeyCode.UpArrow)) {
			anim["soldierRun"].speed = 1;
			anim.CrossFade("soldierRun");
			this.transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
				}
		//Reculer
		else if (Input.GetKey (KeyCode.DownArrow)) {
			anim["soldierRun"].speed = -1;
			anim.CrossFade("soldierRun");
			this.transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
				}
		//Reculer
		else if (Input.GetKey (KeyCode.LeftArrow)) {
			anim.CrossFade("soldierSpinLeft");
			this.transform.Rotate(new Vector3(0,-1,0), angle * Time.deltaTime);
				} 

		else if (Input.GetKey (KeyCode.RightArrow)) {
			anim.CrossFade("soldierSpinRight");
			this.transform.Rotate(new Vector3(0,1,0), angle * Time.deltaTime);
		}else {
			anim.CrossFade("soldierIdleRelaxed");
				}
	}
}
