using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class
//other classes need to derive from this class
namespace RPG.Character
{
    public abstract class SpecialAbilityConfig : ScriptableObject
    {
        [Header("Special Ability: General")] //header for inspector
        [SerializeField] float energyCost = 10f;
        //can add more properties similarly

        abstract public ISpecialAbility AddComponent(GameObject objectToAttachTo);
        //this is something that all derived classes must implement
    }
}


