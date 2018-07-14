using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour, IDamageablePlayer {

    //maximum player health
    [SerializeField] float maxHealthPoints = 100f;
    [SerializeField] int enemyLayer = 9;
    [SerializeField] float playerDamagePerHit = 90f;
    [SerializeField] float timeBetnhits = 1f;
    [SerializeField] float meleeRange = 2f;
    [SerializeField] Weapon weapon1;
    [SerializeField] Weapon weapon2; //alternate weapon
    //[SerializeField] GameObject weaponSocket;
    

    bool w1; //is the primary weapon active?
    bool w2; //is the secondary weapon active?

    float lastHitTime = 0f;

    GameObject currentTarget; //reference to enemy
    CameraRaycaster camCaster;

    //current health level
    private float currentHealthPoints;


    void Start()
    {
        registerLeftClick();
        currentHealthPoints = maxHealthPoints;
        instantiateWeapon(weapon1); 
        w1 = true; //sets primary  weapon active condition to true

    }

    void Update()
    {
        //if 2 is pressed and the primary weapon is currently equipped
        //equip the secondary weapon and remove primary weapon
        if ((Input.GetKeyDown(KeyCode.Alpha2)) && w1 == true)
        {
            instantiateWeapon(weapon2); //equip secondary
            destroyWeapon(weapon1); //destroy primary
            w1 = false; //primary not equipped
            w2 = true; //secondary equipped
        }

        //if 1 is pressed and the secondary weapon is currently equipped
        //equip the primary weapon and remove secondary weapon
        if ((Input.GetKeyDown(KeyCode.Alpha1)) && w2 == true)
        {
            instantiateWeapon(weapon1); //equip primary
            destroyWeapon(weapon2); //destroy secondary
            
            w1 = true; //primary equipped
            w2 = false; //secondary not equipped
        }

        //TODO test the contents of this Update() method
        //test result: new weapon appears but first does not disappear
        //This si because separate local methods are used to create and destroy weapons. So a weapon instantiated by one method is not referenced by another method.
        //TODO : fix weapon destroy method by refencing variables (maybe make global in the scope of this class?)
    
    }

    private void instantiateWeapon(Weapon wep)

    {
        GameObject weaponSocket = RequestDominantHand();
        var weaponPrefToCreate = wep.getPrefab();
        var weapon = Instantiate(weaponPrefToCreate, weaponSocket.transform);
        //weapon = Instantiate(weaponPrefToCreate, weaponSocket.transform);
        weapon.transform.localPosition = wep.gripPos.localPosition; //sets weapon instantiate point to left hand
        weapon.transform.localRotation = wep.gripPos.localRotation;
    }

    //return the dominant hand game object
    private GameObject RequestDominantHand()
    {
        var dominantHands = GetComponentsInChildren<DominantHand>();
        int numberOfDomHands = dominantHands.Length;
        //assertions for 0 hands
        Assert.IsFalse(numberOfDomHands <=  0, "No hand found, add one!");
        Assert.IsFalse(numberOfDomHands > 1, "Multiple dominant hands? NOT POSSIBLE! Remove one!");

        return dominantHands[0].gameObject; //return the dominant hand game object
    }

    private void destroyWeapon(Weapon wep)
    {
        var weaponPrefToDestroy = wep.getPrefab();
        Destroy(weaponPrefToDestroy);
    }

    private void registerLeftClick()
    {
        camCaster = FindObjectOfType<CameraRaycaster>();
        camCaster.notifyMouseClickObservers += onMouseClick;
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