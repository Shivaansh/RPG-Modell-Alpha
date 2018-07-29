using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Character
{
    public class energyMessage : MonoBehaviour
    {

        private Text energyLevel;//variable to hold text component
        private GameObject playerObject;//declare a variable for the player
        private Energy energyScript;
        // Use this for initialization
        void Start()
        {
            energyLevel = GetComponent<Text>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            energyScript = playerObject.GetComponent<Energy>();
        }

        // Update is called once per frame
        void Update()
        {
            float energyValue = energyScript.getEnergyLevel();
            energyLevel.text = "Mana: " + energyValue.ToString();
        }
    }
}


