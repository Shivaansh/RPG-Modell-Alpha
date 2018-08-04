using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
    [CreateAssetMenu (menuName = "Special Ability/Power Attack")]
    //the create option text reflects when you go to create in the right click menu of project window
    //would  create a scriptable object
    public class PowerAttackConfig : SpecialAbilityConfig //this class inherits from an 
        //abstract class
        //is a DERIVED scriptable object
    {
        //override coz this class is derived from an abstract class with the same method

        //Specific field is exclusive to PowerAttackConfig, NOT a field in SpecialAbilityConfig
        [Header("Special Ability: Specific")]
        [SerializeField] float bonusDamage = 10f;

        public override ISpecialAbility AddComponent(GameObject objectToAttachTo)
        {
            var behaviourComponent = objectToAttachTo.AddComponent<PowerAttackBehaviour>();
            behaviourComponent.setConfig(this); //attach this instance of PowerAttackConfig 
            //to the power attack behaviour
            return behaviourComponent;
        }
    }
}

