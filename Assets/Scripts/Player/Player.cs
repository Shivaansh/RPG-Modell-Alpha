using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {

    //maximum player health
    [SerializeField] float maxHealthPoints = 100f;

    //current health level
    private float currentHealthPoints = 100f;

    /**
     * @brief: getter method to return current health level as a value between 0 and 1
     */
    public float getHealthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }

    //Makes player susceptible to damage
    //void IDamageable.TakeDamage(float damage) //by adding IDamageable. we ensure that this method can only be called from the interface
    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
        //ensure that health stays between 0 and max, for the sake of robustness
    }
}
