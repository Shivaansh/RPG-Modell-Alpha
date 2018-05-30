using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		// this method assumes that only one object can have the "Player" tag in the game
		// this method is not the best way to find and assign objects to variables
	}
	
	// lateUpdate performs acctions after all computations are performed
	void LateUpdate () {
		this.transform.position = player.transform.position;

		//now to change the camera angle, we need to edit the transform of the MainCamera relative to CameraArm

	}
}
