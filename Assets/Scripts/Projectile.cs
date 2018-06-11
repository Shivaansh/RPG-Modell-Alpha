using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Use this for initialization
    private void OnTriggerEnter(Collider collider)
    {
        print("Projectile hit " + collider.gameObject); //testing collisions
        //collider.gameObject: returns the name of the gameObject that the collider is attached to
    } 
}
