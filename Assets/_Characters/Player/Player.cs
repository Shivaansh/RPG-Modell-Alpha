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

        [SerializeField] float playerDamage = 10f;

        Weapon activeWeapon;
        [SerializeField] Weapon[] weaponList = new Weapon[2];
        [SerializeField] int startWeapon = 0; //only for testing purposes, remove when weapon switching is enabled.
        [SerializeField] AnimatorOverrideController animOController; //stores the defined animator override controlle
        [SerializeField] float regenPerSecond = 10f;
        [SerializeField] float healTimePeriod = 0f;
        Animator animator;
        float lastHitTime = 0f;

        //array of abilities
        [SerializeField] SpecialAbilityConfig[] abilities; 
        

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
            abilities[0].AddComponent(gameObject);
        }

        public float getHealthLevel()
        {
            return currentHealthPoints;
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
            regenerateHealth();
        }

        //regenerates health
        private void regenerateHealth()
        {
            if (healTimePeriod > 1f)
            {
                //update health using mathf.clamp
                currentHealthPoints = Mathf.Clamp(currentHealthPoints, 0f, maxHealthPoints - 20f);
                currentHealthPoints += regenPerSecond;
                healTimePeriod = 0f;
            }
            healTimePeriod += Time.deltaTime;
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

        private void registerLeftClick()
        {
            camCaster = FindObjectOfType<CameraRaycaster>();
            camCaster.onMouseOverEnemy += OnMouseOverEnemy;
        }

        void OnMouseOverEnemy(Enemy enemy)
        {
            if(Input.GetMouseButton(0) && isTargetInRange(enemy.gameObject))
            {
                attackTarget(enemy);
            }
            else if(Input.GetMouseButtonDown(1))
            {
                AttemptSpecialAbility(0, enemy); //execute special ability on targetted enemy
            }
        }

        //tries to execute special ability on a targetted player
        private void AttemptSpecialAbility(int abilityIndex, Enemy enemy)
        {
            //retrieve energy component of player (it manages energy levels)
            var energyComponent = GetComponent<Energy>();
            float cost = abilities[abilityIndex].getEnergyCost();
            //if energy required for the ability is available
            if (energyComponent.isEnoughEnergyAvailable(cost))
            {
                //reduce required energy from energy store
                energyComponent.consumeEnergy(cost); 

                //construct ability Use parameters struct from instance of the ability
                var abilityParameters = new AbilityUseParameters(enemy, playerDamage);
                abilities[abilityIndex].Use(abilityParameters); //pass ability parameters through struct
            }
        }

        private void attackTarget(Enemy targetComponent)
        {
            //maintain weapon fire rate
            if (Time.time - lastHitTime > activeWeapon.GetTimeBetnHits()) // when time elapsed since last hit is greater than time between hits, and attack distance is less than melee range
            {
                animator.SetTrigger("AxeSwing");//trigger attack animation, string reference is name of trigger in the animator
                targetComponent.TakeDamage(activeWeapon.GetDamagePerHit()); //damage the enemy
                lastHitTime = Time.time; //set time of last hit to current time
            }
        }

        private bool isTimerReset()
        {
            return (Time.time - lastHitTime) > activeWeapon.GetTimeBetnHits(); //returns true if time since last hit is more than time between hits
        }

        private bool isTargetInRange(GameObject enemy)
        {
            float distanceToTarget = Vector3.Distance(transform.position, enemy.transform.position); //the distance between player and enemy at time of attacking
            return distanceToTarget <= activeWeapon.GetMeleeRange();
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
           //TODO: add a load level statement to load death scene, to load game over screen.
           //this  = Player
        }
    }
}
