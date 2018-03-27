using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherBehaviourScript : MonoBehaviour {
    public float Strength;
    public float Range;

    public Sprite inactiveSprite;
    public Sprite activeSprite;
    private bool inRange;

    // Use this for initialization
    void Start () {

        GetComponent<SpriteRenderer>().sprite = inactiveSprite;
    }

    void FixedUpdate()
    {
        GameObject []energySpheres;
        energySpheres = GameObject.FindGameObjectsWithTag("Player");

        GetComponent<SpriteRenderer>().sprite = inactiveSprite;

        inRange = false;

        foreach (GameObject energySphere in energySpheres)
        {
            if (Vector3.Distance(transform.position, energySphere.GetComponent<Transform>().position) < Range){
                inRange = true;
                var vsub = transform.position - energySphere.GetComponent<Transform>().position;
                energySphere.GetComponent<Rigidbody2D>().AddForce(vsub.normalized * -(Mathf.Max((Strength - vsub.magnitude) / 5, 0)), ForceMode2D.Force);
            }
        }

        if (inRange)
        {
            GetComponent<SpriteRenderer>().sprite = activeSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = inactiveSprite;
        }

    }


 
}
