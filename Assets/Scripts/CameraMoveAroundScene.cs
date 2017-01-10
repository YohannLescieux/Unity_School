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
