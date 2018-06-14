using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageablePlayer {

    //maximum player health
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] int enemyLayer = 9;
    [SerializeField] float playerDamagePerHit = 90f;
    [SerializeField] float timeBetnhits = 1f;
    [SerializeField] float meleeRange = 2f;

    float lastHitTime = 0f;

    GameObject currentTarget; //reference to enemy
    CameraRaycaster camCaster;

    //current health level
    private float currentHealthPoints;


    void Start()
    {
        camCaster = FindObjectOfType<CameraRaycaster>();
        camCaster.notifyMouseClickObservers += onMouseClick;
        currentHealthPoints = maxHealthPoints;
    }

     void onMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer) //if the layer hit by ray has same index as enemy layer
        {
            var enemy = raycastHit.collider.gameObject; //enemy now holds the enemy gameObject
            //print("Clicked on enemy");
            currentTarget = enemy;
            float attackDistance = Vector3.Distance(transform.position, enemy.transform.position);
            var enemyComponent = enemy.GetComponent<Enemy>();
            
           
            if (Time.time - lastHitTime > timeBetnhits && attackDistance <= meleeRange)
            {
                enemyComponent.TakeDamage(playerDamagePerHit);
                lastHitTime = Time.time;
            }
        }
    }

    /**
* @brief: getter method to return current health level as a value between 0 and 1
*/
    public float getHealthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }

    //Makes player susceptible to damage
    //void IDamageable.TakeDamage(float damage) //by adding IDamageable. we ensure that this method can only be called from the interface
    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        //ensure that health stays between 0 and max, for the sake of robustness
        if (currentHealthPoints <= 0) { Destroy(gameObject); } //kills the player when health reached 0
        //TODO: add a load level statement to load death scene.
     //this  = Player
     }

    //TODO : maybe make a regenerating method?
}