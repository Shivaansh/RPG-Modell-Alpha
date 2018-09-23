using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

public class LoadNewLevel : MonoBehaviour {

    Enemy enemyScript;
    LevelManager lvlManager;
	// Use this for initialization
	void Start () {
        enemyScript = GetComponent<Enemy>();
        lvlManager = GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
        print("Update method functional");
        print(enemyScript.getHealth());
        if (enemyScript.getHealth() <= 0f)
        {
            print("Victory level loading");
            lvlManager.LoadNextLevel();
        }
	}
}
