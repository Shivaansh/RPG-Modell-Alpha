using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;
//TODO: fix WASD = Click movement conflict


[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]

public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter gameCharacter = null;   // A reference to the ThirdPersonCharacter on the object
   AICharacterControl aiControl = null; //reference to AI Character control
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget, clickPoint;
    GameObject runPoint;

    const int walkableLayerIndex = 8; //coinst so that it works with switch statement
    const int enemyLayerIndex = 9;

    bool isInDirectMode = false;
    //checks whether the game is set to Direct Movement ( WASD / Gamepad input) mode 

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        gameCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
        aiControl = GetComponent<AICharacterControl>(); //ai character control script identified and stored
        runPoint = new GameObject("runPoint");
        cameraRaycaster.notifyMouseClickObservers += clickData;
     }

    void clickData(RaycastHit raycastHit, int layerHit)
    {
        print("Clicked!"); //check if click is detected

        switch (layerHit)
        {
            case (enemyLayerIndex):
                //go to enemy
                GameObject foe = raycastHit.collider.gameObject;
                currentClickTarget = foe.transform.position;
                aiControl.SetTarget(foe.transform);
                break;

            case (walkableLayerIndex):
                //walk to point on map
                runPoint.transform.position = raycastHit.point;
                currentClickTarget = raycastHit.point;
                aiControl.SetTarget(runPoint.transform);
                break;

            default:
                //print error message, stay in position
                print("invalid movement target");
                return;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(currentClickTarget, 0.5f);
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

    //private void runToDestination()
    //{
    //    var playerToClick = currentClickTarget - transform.position;

    //    if (playerToClick.magnitude >= walkStopRadius)
    //    //move if the distance between player and target is more than stopping radius
    //    {
    //        //move player from current position to click target
    //        gameCharacter.Move(currentClickTarget - transform.position, false, false);
    //    }
    //    else
    //    {
    //        gameCharacter.Move(Vector3.zero, false, false);
    //    }
    //}
    
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

