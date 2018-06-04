using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
//automatically imports CameraRaycaster object into any GameObject where
//CursorAffordances is imported
public class CursorAffordance : MonoBehaviour {

    /**
     * NOTE: [SerializeField]  is used for showing a private variable value in the inspector, other classes however cannot modify these values
     * */
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D enemyCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    //these variables store the sprites to use for the cursor

    [SerializeField] Vector2 cursorHotpot = new Vector2(96, 96);
    //the value (96, 96) is derived via trial and error
    // TODO: check the movement for different values of Cursor hotspot

    CameraRaycaster camraycaster;
	// Use this for initialization
	void Start ()
    {
        camraycaster = GetComponent<CameraRaycaster>();
        camraycaster.layerChangeObservers += OnDelegateCall; //registering        
	}
	
	// Update is called once per frame
	void OnDelegateCall()
    {    
        print("Cursor now on new layer");
        switch (camraycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(walkCursor, cursorHotpot, CursorMode.Auto);
                break;

            case Layer.Enemy:
                Cursor.SetCursor(enemyCursor, cursorHotpot, CursorMode.Auto);
                break;

            case Layer.RaycastEndStop:
                Cursor.SetCursor(unknownCursor, cursorHotpot, CursorMode.Auto);
                break;

            default:
                Debug.Log("Unknown dimension detected, no cursor applicable");
                return;
        }
        /**
         * camraycaster retrieves ray casting information
         */
    }



}
