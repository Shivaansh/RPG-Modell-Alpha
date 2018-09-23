using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChecker : MonoBehaviour {

    LevelManager lvlManager;
	// Use this for initialization
	void Start () {
        lvlManager = GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.Find("Barbarian King") == null)
        {
            lvlManager.LoadNextLevel();
        }
	}
}
