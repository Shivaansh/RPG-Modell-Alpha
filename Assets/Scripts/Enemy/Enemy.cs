using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour, IDamageableEnemy{
    //TODO: Fine tune range values for enemy stop and player AttackStop
    
    [SerializeField] float triggerRadius = 4f;  //enemy attacking range
    [SerializeField] float moveRadius = 14f;    

    [SerializeField] GameObject projectileAttack;     //reference to projectile prefab
   // [SerializeField] GameObject projectileSpawnPoint;     //reference to projectile spawn point (a prefab and a child of the Enemy)  -> projectileSpawnPoint has unexplained behavior issues

    //projectile speed and damage, rate of fire
    [SerializeField] float  projectileSpeed = 10f;
    [SerializeField] float  damagePerShot = 10f;
    [SerializeField] float timeBetnShot = 1.5f; //time (in seconds) between successive shots

    [SerializeField] Vector3 aimOffset = new Vector3(0, 1f, 0); //for melee attacks

    AICharacterControl CharacterControl = null;     //reference to AICharacterControl script attached to enemy
    GameObject player = null;     //reference to the player (external object)
    private float currentHealthPoints = 100f; //current health level
    [SerializeField] float maxHealthPoints = 100f;     //maximum player health
    [SerializeField] float heightToFire = 1.3f; //height at which to shoot player, 2 seems good
    bool isAttacking = false;

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
        currentHealthPoints = maxHealthPoints;
    }

    /*
 * Basically, when the player comes into a range less than or equal to the trigger radius, the player will be set as the target
 * for the AI character control script. Stopping radius will be the distance between the player and enemy.
 */

    void Update()
    {
        float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distToPlayer <= triggerRadius && !isAttacking)
        {
            //print(gameObject.name + " in attack phase");
            isAttacking = true;
            //attackPlayer();
            InvokeRepeating("attackPlayer", 0f, timeBetnShot); //TODO: maybe switch to co-routines
            //InvokeRepeating invokes methods by string name reference, params are method name, initial delay and subsequent delay
        }
        if(distToPlayer > triggerRadius)
        {
            CancelInvoke();
            isAttacking = false;
           // isAttacking = !isAttacking; -> using this statement prevents the enemy from attacking till it is itself attacked
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

    //Instantiates an attack projectile and assigns it a direction towards the player
    void attackPlayer()
    {

        Vector3 position = transform.position;
        position.y += 3f;
        position.x += 0f;
        Vector3 spawnPosition = position;

        // GameObject newBall = Instantiate(projectileAttack, projectileSpawnPoint.transform.position, transform.rotation);  -> projectileSpawnPoint has unexplained behavior issues
        GameObject newBall = Instantiate(projectileAttack, spawnPosition, transform.rotation);
        EnemyProjectile proj = newBall.GetComponent<EnemyProjectile>();
        proj.damageCaused = damagePerShot;

        Vector3 pos = player.transform.position;
        pos.y += heightToFire;
        Vector3 unit = (pos - spawnPosition).normalized;
       // Vector3 unit = (player.transform.position - projectileSpawnPoint.transform.position).normalized; -> projectileSpawnPoint has unexplained behavior issues
        float projectileSpeed = proj.proSpeedValue;
        newBall.GetComponent<Rigidbody>().velocity = unit * projectileSpeed;
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        if (currentHealthPoints <= 0) { Destroy(gameObject); } //kills enemy when health reaches 0
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