using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


//TODO: fix WASD = Click movement conflict
[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    ThirdPersonCharacter gameCharacter;   // A reference to the ThirdPersonCharacter on the object

    CameraRaycaster cameraRaycaster;

    Vector3 currentClickTarget, clickPoint;

    bool isInDirectMode = false;
    //checks whether the game is set to Direct Movement ( WASD / Gamepad input) mode 

    [SerializeField] float walkStopRadius = 0.2f;
    [SerializeField] float attackMoveStopRadius = 3.0f;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        gameCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) //G for gamepad, allow remapping later TODO
        {
            isInDirectMode = !isInDirectMode; //toggle mode
            currentClickTarget = transform.position; //prevent moving to 
            //previous click point when switching back to mouse movement
        }

        if(isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
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
     * This methods helps with the "click to move" feature
     */
    private void ProcessMouseMovement()
    {
        Debug.Log("Mouse movement mode");

        if (Input.GetMouseButton(0))
        {

            clickPoint = cameraRaycaster.hit.point;

            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    //currentClickTarget = clickPoint;
                    currentClickTarget = ShortDestination(clickPoint, walkStopRadius);
                    //if ray hits a walkable object, click target is the point where ray meets object
                    break;

                case Layer.Enemy:
                    currentClickTarget = ShortDestination(clickPoint, attackMoveStopRadius);
                    //in case the ray hits an enemy
                    break;

                default:
                    Debug.Log("Shouldn't be here");
                    return;
            }

        }

        runToDestination();

    }

    private void runToDestination()
    {
        var playerToClick = currentClickTarget - transform.position;

        if (playerToClick.magnitude >= walkStopRadius)
        //move if the distance between player and target is more than stopping radius
        {
            //move player from current position to click target
            gameCharacter.Move(currentClickTarget - transform.position, false, false);
        }
        else
        {
            gameCharacter.Move(Vector3.zero, false, false);
        }
    }

    //renders gizmos in the game, called every time gizmos are drawn
    void OnDrawGizmos()
    {
        //print("Gizmo drawn"); <- debugging statement

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, currentClickTarget);
        //draw a line from the current position of the Player to the click target
        Gizmos.DrawSphere(currentClickTarget, 0.1f); //final destination visualization
        Gizmos.DrawSphere(clickPoint, 0.15f); //click point visualization

        //draw attack region sphere
        Gizmos.color = new Color(255f, 0f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);

        //consider playing around with these values
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

