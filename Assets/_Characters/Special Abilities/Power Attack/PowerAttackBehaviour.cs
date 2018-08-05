using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
    public class PowerAttackBehaviour : MonoBehaviour, ISpecialAbility
    {
        PowerAttackConfig config; //reference to PowerAttackConfig
        //This class just keeps a reference of behaviour for the respective config

        //setter method to read and modify config data
        public void setConfig(PowerAttackConfig configToSet)
        {
            this.config = configToSet;
        }

        void Start()
        {
            Debug.Log("Power attack behavior attached to " + gameObject.name);
        }

        void Update()
        {

        }
        
        //implemented by the SpecialAbilityConfig abstract class due to forwarding

            //THIS IS THE LOWEST LEVEL USE METHOD.
        public void Use(AbilityUseParameters parameters)//this method is used here because this class uses the ISpecialAbility interface
        {
            print("energy ball released by: " + gameObject.name);
            //print("Player base damge: " + parameters.baseDamage); //check if parameters pass as struct correctly
            //total damage caused by ability is player  base damage PLUS ability damage
            float damageToCauseOnHit = parameters.baseDamage + config.getBonusDamage();
            parameters.target.TakeDamage(damageToCauseOnHit); //using IDamageableEnemy interface
            //instantiate projectile and add animation?
        }
    }
}