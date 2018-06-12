using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageable{
    //TODO: Fine tune range values for enemy stop and player AttackStop

    //maximum player health
    [SerializeField] float maxHealthPoints = 100f;

    //enemy attacking range
    [SerializeField] float triggerRadius = 4f;
    [SerializeField] float moveRadius = 14f;

    //current health level
    private float currentHealthPoints = 100f;

    //reference to the player (external object)
    GameObject player = null;
   
    //reference to AICharacterControl script attached to enemy
    AICharacterControl CharacterControl = null;
    

    /**
     * @brief: getter method to return current health level as a value between 0 and 1
     */
    public float getHealthAsPercentage
    {
        get
        {
            return currentHealthPoints / (float)maxHealthPoints;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //player variable retrieves the player from the scene
        //character control is the AI Character control script
        CharacterControl = GetComponent<AICharacterControl>();
    }

    /*
 * Basically, when the player comes into a range less than or equal to the trigger radius, the player will be set as the target
 * for the AI character control script. Stopping radius will be the distance between the player and enemy.
 */

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //distance between player and THIS enemy

        if (distToPlayer <= triggerRadius)
        {
            print("Enemy in attack phase");
            //placeholder for attack method call
        }

        if (distToPlayer <= moveRadius)
        {
            CharacterControl.SetTarget(player.transform); //sets player as target for AICharacterControl Script
            //this makes the player the target for the enemy to move to
        }
        else
        {
            CharacterControl.SetTarget(transform);
            //when the player goes far away from the enemy, the enemy stops and stays in position
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
    }

    private void OnDrawGizmos()
    {
        //Draw attack radius sphere
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);

        //Draw move radius sphere
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, moveRadius);
    }
}