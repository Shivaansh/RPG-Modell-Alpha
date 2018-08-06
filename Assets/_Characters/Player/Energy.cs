using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Character
{
    [RequireComponent(typeof(RawImage))]
    public class Energy : MonoBehaviour
    {

        [SerializeField] RawImage energyBarImage = null; //energy bar image
        [SerializeField] float maxEnergypoints = 100f;
        [SerializeField] float regenPerSecond = 5f;
        float currentEnergyPoints;
        float timePeriod = 0f;

        void Start()
        {
            currentEnergyPoints = maxEnergypoints;
        }

        void Update()
        {
            regenerateEnergy();
        }

        //regenerates energy
        private void regenerateEnergy()
        {
            if (timePeriod > 1f)
            {
                //update energy using mathf.clamp
                currentEnergyPoints = Mathf.Clamp(currentEnergyPoints, 0f, maxEnergypoints - 5f);
                currentEnergyPoints += regenPerSecond;
                updateEnergyBar();
                timePeriod = 0f;
            }
            timePeriod += Time.deltaTime;
        }

        //return if a certain amount of energy is available
        public bool isEnoughEnergyAvailable(float cost)
        {
            return cost <= currentEnergyPoints;
        }

        public  void consumeEnergy(float cost)
        {
            float newEnergyLevel = currentEnergyPoints - cost;
            currentEnergyPoints = Mathf.Clamp(newEnergyLevel, 0, maxEnergypoints);
            updateEnergyBar();
        }

        private void updateEnergyBar()
        {
            float xValue = -(getHealthAsPercentage() / 2f) - 0.5f;
            energyBarImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
        }

        float getHealthAsPercentage()
        {
            return (currentEnergyPoints / maxEnergypoints) ;
        }

        public float getEnergyLevel()
        {
            return currentEnergyPoints;
        }
    }
}