using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody rb;
    public Animation anim;
    public float vitesseRotate = 20f;

    void Start()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Déplacement lateral droit
        if (Input.GetKey(KeyCode.RightArrow))
        {
            anim.CrossFade("soldierWalk");
            this.transform.position += Vector3.right * speed * Time.deltaTime;
        }
        //Deplacement latéral gauche
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            anim.CrossFade("soldierWalk");
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        //Avancer
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.CrossFade("soldierWalk");
            this.transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        //Reculer
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.CrossFade("soldierWalk");
            this.transform.position += Vector3.back * speed * Time.deltaTime;
        }


        //Tourner à gauche
        if (Input.GetKey(KeyCode.Keypad4))
        {
            this.transform.RotateAround(rb.position, Vector3.down, Time.deltaTime * vitesseRotate);
        }
        //Tourner à droite
        if (Input.GetKey(KeyCode.Keypad6))
        {
            this.transform.RotateAround(rb.position, Vector3.up, Time.deltaTime * vitesseRotate);
        }
    }
}

