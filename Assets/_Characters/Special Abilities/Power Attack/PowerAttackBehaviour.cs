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

        }

        void Update()
        {

        }
        public void Use()//this method is used here because this class uses the ISpecialAbility interface
        {

        }
    }
}

