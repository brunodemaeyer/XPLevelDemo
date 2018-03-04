using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAround : MonoBehaviour {

    private Rigidbody rbTarget;

    public float Level;
    private float xMovement;
    private float zMovement;
    Vector3 move;
    private float railLock;




    // Use this for initialization
    void Start ()
    {
        rbTarget = GetComponent<Rigidbody>();
        xMovement = 0;  //Random.Range(0, 1.0f);
        zMovement =  Random.Range(1.0f, 2.0f);
        move = new Vector3(xMovement, 0, zMovement);
        Level = 5000;
        railLock = transform.position.z;
    }

    // Update is called once per frame


    void FixedUpdate ()
    {
        rbTarget.AddForce(move * Level);

    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Bad Target") || collision.gameObject.CompareTag("Boundary"))
        //{
        //move = collision.contacts[0].point - transform.position;
        transform.localEulerAngles = new Vector3(0, 90, 0);
        move = -move;
            //rbTarget.AddForce(move * Level);
        //}
    }
}
