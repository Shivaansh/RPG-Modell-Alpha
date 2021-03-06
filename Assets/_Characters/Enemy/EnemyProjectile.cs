﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character;

public class EnemyProjectile : MonoBehaviour {
    //formerly Projectile: split into two classes to differentiate particles fired by player and enemy
    //advantage of using the interface design pattern is that this code will now work for ALL
    //gameObjects that can take damage without any modification
    public float damageCaused; //accessible to other classes
    public float proSpeedValue ; //TODO: needs 
    //these values can be set by other classes

    private void OnTriggerEnter(Collider collider)
    {
        Component damagedComponentOfPlayer = collider.gameObject.GetComponent(typeof(IDamageablePlayer));
        //the damaged component should implement the IDamageable interface
        if (damagedComponentOfPlayer)
        {
            (damagedComponentOfPlayer as IDamageablePlayer).TakeDamage(damageCaused);
        }
        Destroy(gameObject);
    } 
}