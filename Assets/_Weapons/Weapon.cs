using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
    [CreateAssetMenu(menuName = ("RPG / Weapon"))]
    public class Weapon : ScriptableObject
    { //MonoBehaviour { -> ScriptableObject inherits
      //directly from object

        public Transform gripPos;
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;

        [SerializeField] float timeBetnhits ; //TODO consider making these weapons properties
        [SerializeField] float meleeRange; //and using getters to reference values in Player.cs
        [SerializeField] float DamagePerHit;

        //return the fire rate of the weapon
        public float GetTimeBetnHits()
        {
            //take animation time into account
            return timeBetnhits;
        }

        //returns the range of the weapon
        public float GetMeleeRange()
        {
            return meleeRange;
        }

        //returns damage per shot for the weapon
        public float GetDamagePerHit()
        {
            return DamagePerHit;
        }

        public GameObject getPrefab()
        {
            return weaponPrefab;
        }

        //getter method to return the animation clip associated with the weapon
        public AnimationClip getAttackAnimClip()
        {
            removeAnimationEvents(); 
            return attackAnimation;
        }

        //clears animation event list, so that asset packs cannot create errors
        private void removeAnimationEvents()
        {
            attackAnimation.events = new AnimationEvent[0]; //set animation event list to an array of size 0
        }
    }
}

