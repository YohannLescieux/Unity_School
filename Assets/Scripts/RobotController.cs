using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {
	private Light lt;
	private Animator anim;
	private AudioSource audi;
	public GameObject robot;
	public ParticleSystem part;
	public ParticleSystem part2;

	//Nos variables
	public float duration = 1.0F;

	// Use this for initialization
	void Start () {
		lt = this.GetComponent<Light> ();
		audi = this.GetComponent<AudioSource> ();
		anim = robot.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Burn")) {
			//Light
			float phi = Time.time * 40 * Mathf.PI;
			float amplitude = Mathf.Cos (phi) * 20.0F;
			lt.intensity = amplitude;
			lt.range = 3.2f;
			//Audio
			audi.Play ();
			//Particules
			part.Play();
			part2.Play ();

		} else {
			lt.intensity = 0;
			part.Pause();
			part2.Pause ();
			part.Clear ();
			part2.Clear ();
		}
	}
}
