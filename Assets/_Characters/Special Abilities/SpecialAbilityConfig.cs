using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Utility;
//abstract class
//other classes need to derive from this class
namespace RPG.Character
{
    //think of a struct like an ADT
    public struct AbilityUseParameters //parameters to be passed on to children of this abstract class (like Power Attack)
        //here you declare the properties of the ADT
    {
        public IDamageableEnemy target;
        public float baseDamage;

        //constructor for the struct
        //here you add the values to a particular instance of an ADT
        public AbilityUseParameters(IDamageableEnemy target, float baseDamage)
        {
            this.target = target;
            this.baseDamage = baseDamage;
        }
    }

    public abstract class SpecialAbilityConfig : ScriptableObject
    {
        [Header("Special Ability: General")] //header for inspector
        [SerializeField] float energyCost = 10f;
        //can add more properties similarly
        //classes derived from this abstract class need to know what their
        //respective behaviour is
        protected ISpecialAbility behaviour; //only derived classes can set this
        //used by power attack behaviour and other derivatives

        //members derived from this class implementing this method should have the same
        //return type (here, void)
        abstract public void  AddComponent(GameObject objectToAttachTo);
        //this is something that all derived classes must implement

        public void Use(AbilityUseParameters parameters) //to implement the ability through Player.cs in the game at runtime
        {
            behaviour.Use(parameters); //forwarding Use request to behaviour script of derived child
        }

        //return energy cost, method useable by all derived classes
        public float getEnergyCost()
        {
            return energyCost;
        }
    }
    public interface ISpecialAbility
    {
        void Use(AbilityUseParameters parameters);
    }
}