using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject Player;

    private Vector3 offset;


	// Use this for initialization
	void Start () {
        offset = transform.position - Player.transform.position;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Player.transform.position + offset;
	}
}
