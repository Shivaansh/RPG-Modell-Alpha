using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu(menuName = "Special Ability/Area of Effect attack")]
    public class AreaAttackConfig : SpecialAbilityConfig
    {
        [Header("Area of Effect properties")]
        [SerializeField] float damageRadius = 6f;
        [SerializeField] float damageDealt = 20f;

        //attaches a behaviour component for this ability to the player at runtime
        public override void AddComponent(GameObject objectToAttachTo)
        {
            var behaviourComponent = objectToAttachTo.AddComponent<AreaAttackBehaviour>();
            behaviourComponent.setConfig(this); //attach this instance of PowerAttackConfig 
            //to the power attack behaviour
            behaviour = behaviourComponent;
        }

        //returns the Area of effect
        public float getDamageRadius()
        {
            return damageRadius;
        }

        //returns damage dealt to each enemy in range
        public float getDamageDealt()
        {
            return damageDealt;
        }
    }
}
