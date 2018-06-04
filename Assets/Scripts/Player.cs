using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //maximum player health
    [SerializeField] float maxHealthPoints = 100f;

    //current health level
    private float currentHealthPoints = 100f;

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
}
