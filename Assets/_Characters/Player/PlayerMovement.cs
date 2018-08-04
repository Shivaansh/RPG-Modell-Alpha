using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
using RPG.CameraUI; //TODO Consider rewiring, we may not want this dependency to exist

namespace RPG.Character
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(AICharacterControl))]

    public class PlayerMovement : MonoBehaviour
    {
        ThirdPersonCharacter gameCharacter = null;   // A reference to the ThirdPersonCharacter on the object
        AICharacterControl aiControl = null; //reference to AI Character control
        CameraRaycaster cameraRaycaster;
        Vector3 currentClickTarget, clickPoint;
        GameObject runPoint;

        bool isInDirectMode = false;
        //checks whether the game is set to Direct Movement ( WASD / Gamepad input) mode 

        private void Start()
        {
            cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
            gameCharacter = GetComponent<ThirdPersonCharacter>();
            currentClickTarget = transform.position;
            aiControl = GetComponent<AICharacterControl>(); //ai character control script identified and stored
            runPoint = new GameObject("runPoint");

            cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            //adding OnMouseOverPotentiallyWalkable to observer set, hence subscribed
        }


        void OnMouseOverEnemy(Enemy enemy)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(1)) //on left/right click
            {
                //TODO consider moving only on left click
                aiControl.SetTarget(enemy.transform); //move to enemy
            }
        }
         
         void OnMouseOverPotentiallyWalkable(Vector3 destination)
        {

            if (Input.GetMouseButton(0)) //on left clik
            {
                runPoint.transform.position = destination;
                currentClickTarget = destination;
                aiControl.SetTarget(runPoint.transform); //move to click target
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(currentClickTarget, 0.2f);
        }

        /**
         * This methods helps with WASD and controller input movement
         */
        private void ProcessDirectMovement()
        {
            Debug.Log("WASD movement mode");
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");


            // calculate camera relative direction to move:
            Vector3 camForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 moveOn = v * camForward + h * Camera.main.transform.right;

            gameCharacter.Move(moveOn, false, false);
        }

        /**
         *  @brief: Reduces a vector by a specified float value (used for fine tuning gizmo rendering)
         *  @param: destination - the vector to be shortened
         *  @param: shorteningLength - the factor by which the vector is shortened
         */
        Vector3 ShortDestination(Vector3 destination, float shorteningLength)
        {
            Vector3 reductionVector = (destination - transform.position).normalized * shorteningLength;
            return destination - reductionVector;
        }
    }
}


