using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    //advantage of using the interface design pattern is that this code will now work for ALL
    //gameObjects that can take damage without any modification
    [SerializeField] float damageValue = 30f; //TODO: needs tuning

    private void OnTriggerEnter(Collider collider)
    {
        Component damagedComponent = collider.gameObject.GetComponent(typeof(IDamageable));
        print("Projectile hit " + damagedComponent); //using IDamageable interface to identify object damaged
        //the damaged component should implement the IDamageable interface
        if (damagedComponent)
        {
            
            (damagedComponent as IDamageable).TakeDamage(damageValue);
        }
    } 
}