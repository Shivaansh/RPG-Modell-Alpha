using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character
{
    public class AreaAttackBehaviour : MonoBehaviour, ISpecialAbility
    {
        AreaAttackConfig config;

        //setter method to store reference to config file for this ability
        public void setConfig(AreaAttackConfig configToSet)
        {
            this.config = configToSet;
        }

        // Use this for initialization
        void Start()
        {
            print("Area effect behaviour attached to " + gameObject.name); //testing
        }

        // Update is called once per frame
        void Update()
        {

        }

        //will not work with interface unless public
        public void Use(AbilityUseParameters parameters)
        {
            //write ability usage logic here
            print("Force shield deployed upto 6m by " + gameObject.name);
            //use static sphere cast?
            RaycastHit[] objectsHit =
                Physics.SphereCastAll(transform.position, config.getDamageRadius(), 
                Vector3.up, config.getDamageRadius()); //SphereCastAll returns an array of RaycastHit objects
            //sweep made static by keeping radius and maxdistance same

            //iterates over each object of the SphereCast returned array
            foreach (RaycastHit obj in objectsHit)
            {
                var damageable = obj.collider.gameObject.GetComponent<IDamageableEnemy>(); //if the object has IDamageableEnemy component
                if (damageable != null)
                {
                    //deal respective damage
                    float damageToDeal = config.getDamageDealt();
                    damageable.TakeDamage(damageToDeal);
                    //this ability ONLY deals bonus damage, player damage values are \
                    //NOT accounted for
                }
            }
        }
    }
}


