using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using RPG.CameraUI; //TODO Consider rewiring, we may not want this dependency to exist
using RPG.Weapons;
namespace RPG.Character
{
    public class Player : MonoBehaviour, IDamageablePlayer
    {

        //maximum player health
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] int enemyLayer = 9;
        [SerializeField] float playerDamagePerHit = 90f;
        [SerializeField] float timeBetnhits = 1f; //TODO consider making these weapons properties
        [SerializeField] float meleeRange = 2f; //and using getters to reference values in Player.cs
        Weapon activeWeapon;
        [SerializeField] Weapon[] weaponList = new Weapon[2];
        [SerializeField] int startWeapon = 0; //only for testing purposes, remove when weapon switching is enabled.
        //[SerializeField] Weapon weapon2; //alternate weapon
        [SerializeField] AnimatorOverrideController animOController; //stores the defined animator override controller
        Animator animator;
        float lastHitTime = 0f;

        CameraRaycaster camCaster;

        //current health level
        private float currentHealthPoints;


        void Start()
        {
            activeWeapon = weaponList[startWeapon];
            registerLeftClick();
            initializeHealthSetup();
            instantiateWeapon(activeWeapon);
            setupAnimator();
        }

        private void initializeHealthSetup()
        {
            currentHealthPoints = maxHealthPoints;
        }

        //overrides the animation for the animation controlller at runtime 
        private void setupAnimator()
        {
            animator = GetComponent<Animator>();  //animator component reference
            animator.runtimeAnimatorController = animOController; //reference to the serialized animator override controller
            animOController["DEFAULT ATTACK"] = activeWeapon.getAttackAnimClip(); //maybe remove constant
                                                                                  // throw new NotImplementedException();
        }

        void Update()
        {
            weaponSwitch();
        }

        private void weaponSwitch()
        {
            //TODO implement weapon switching
            //if 2 is pressed and the primary weapon is currently equipped
            //equip the secondary weapon and remove primary weapon
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {

                // DestroyImmediate(activeWeapon, true); //destroy primary
                // destroyWeapon(activeWeapon); //not working
                activeWeapon = weaponList[1]; //make secondary primary
                instantiateWeapon(activeWeapon);//equip secondary
            }

            //if 1 is pressed and the secondary weapon is currently equipped
            //equip the primary weapon and remove secondary weapon
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                // DestroyImmediate(activeWeapon, true); //destroy primary
                //destroyWeapon(activeWeapon);
                activeWeapon = weaponList[0]; //make secondary primary
                instantiateWeapon(activeWeapon);//equip secondary
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
            Assert.IsFalse(numberOfDomHands <= 0, "No hand found, add one!");
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
                var enemyComponent = enemy.GetComponent<Enemy>(); //gets the Enemy (script) component from the current enemy GameObject
                attackTarget(enemy, enemyComponent);
            }
        }

        private void attackTarget(GameObject target, Enemy targetComponent)
        {
            if (isTimerReset() && isTargetInRange(target)) // when time elapsed since last hit is greater than time between hits, and attack distance is less than melee range
            {
                animator.SetTrigger("AxeSwing");//trigger attack animation, string reference is name of trigger in the animator
                targetComponent.TakeDamage(playerDamagePerHit); //damage the enemy
                lastHitTime = Time.time; //set time of last hit to current time
            }
        }

        private bool isTimerReset()
        {
            return (Time.time - lastHitTime) > timeBetnhits; //returns true if time since last hit is more than time between hits
        }

        private bool isTargetInRange(GameObject enemy)
        {
            float distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position); //the distance between player and enemy at time of attacking
            return distanceToTarget <= meleeRange;
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
}
