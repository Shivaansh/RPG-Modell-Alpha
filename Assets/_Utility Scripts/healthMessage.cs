using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Character
{
    public class healthMessage : MonoBehaviour
    {

        private Text healthLevel;//variable to hold text component
        private GameObject playerObject;//declare a variable for the player
        private Player playerScript;
        // Use this for initialization
        void Start()
        {
            healthLevel = GetComponent<Text>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
           playerScript = playerObject.GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            float healthValue = playerScript.getHealthLevel();
            healthLevel.text = "Health: " + healthValue.ToString();
        }
    }
}


