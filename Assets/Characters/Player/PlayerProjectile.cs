using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    //spinoff of EnemyProjectile, this script ensures that Player fired projectiles do not damage the player
    //advantage of using the interface design pattern is that this code will now work for ALL
    //gameObjects that can take damage without any modification
    public float damageCaused; //accessible to other classes
    public float proSpeedValue ; //TODO: needs 
    //these values can be set by other classes

    private void OnTriggerEnter(Collider collider)
    {
        Component damagedComponent = collider.gameObject.GetComponent(typeof(IDamageableEnemy));
        print("Projectile fired by PLAYER hit " + damagedComponent); //using IDamageable interface to identify object damaged
        //the damaged component should implement the IDamageable interface
        if (damagedComponent)
        {
            (damagedComponent as IDamageableEnemy).TakeDamage(damageCaused);
        }
        Destroy(gameObject);
    } 
}