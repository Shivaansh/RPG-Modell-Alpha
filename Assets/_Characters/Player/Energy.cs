using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;

namespace RPG.Character
{
    [RequireComponent(typeof(RawImage))]
    public class Energy : MonoBehaviour
    {

        [SerializeField] RawImage energyBarImage; //energy bar image
        [SerializeField] float maxEnergypoints = 100f;
        [SerializeField] float pointsPerCast = 15f;
        CameraRaycaster rayCaster;
        float currentEnergyPoints;

        // Use this for initialization
        void Start()
        {
            currentEnergyPoints = maxEnergypoints;
            rayCaster = Camera.main.GetComponent<CameraRaycaster>();
            rayCaster.notifyRightClickObservers += ProcessRightClick;
        }

        void ProcessRightClick(RaycastHit raycasthit, int layerHit)
        {
            print("Right clicked");
            float newEnergyLevel = currentEnergyPoints - pointsPerCast;
            currentEnergyPoints = Mathf.Clamp(newEnergyLevel, 0, maxEnergypoints);
            updateEnergyBar();
        }

        void updateEnergyBar()
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

