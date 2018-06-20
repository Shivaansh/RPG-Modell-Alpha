using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using System.Collections.Generic;


/*
 * This class is now responsible for announcing the current layer that is raycast as 
 * well as the current layer that is clicked on (using the FindTopPriorityHit (RaycastHit[] raycastHits) ) method.
 * The layer that this method returns will be the same as the layer that CursorAffordance.cs
 * reads, thus reducing discrepancies and improving consistency.
 * 
 */
public class CameraRaycaster : MonoBehaviour
{
	// INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
	[SerializeField] int[] layerPriorities; //exposes the array in inspector
    //layerType to be replaced with integer indexed layers
    
    float maxRaycastDepth = 100f; // Hard coded value
	int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

	// Setup delegates for broadcasting layer changes to other classes
    public delegate void OnCursorLayerChange(int newLayer); // declare new delegate type
    public event OnCursorLayerChange notifyLayerChangeObservers; // instantiate an observer set

    //new delegate system
	public delegate void OnClickPriorityLayer(RaycastHit raycastHit, int layerHit); // declare new delegate type
	public event OnClickPriorityLayer notifyMouseClickObservers; // instantiate an observer set


    void Update()
	{
		// Check if pointer is over an interactable UI element (GameObject must be of UI type)
        //requires EventSystem to work
		if (EventSystem.current.IsPointerOverGameObject ())
		{
			NotifyObserersIfLayerChanged (5);
			return; // Stop looking for other objects
		}

		// Raycast to max depth, every frame as things can move under mouse
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit[] raycastHits = Physics.RaycastAll (ray, maxRaycastDepth);
        //list of Raycast hits returned for every raycast

        //on raycast hit, find the top priority hit from the array of hits
        RaycastHit? priorityHit = FindTopPriorityHit(raycastHits);
        if (!priorityHit.HasValue) // if hit no priority object
		{
			NotifyObserersIfLayerChanged (0); // broadcast default layer
			return;
		}

		// Notify delegates of layer change
		var layerHit = priorityHit.Value.collider.gameObject.layer; //look up demeter's law and optimize
		NotifyObserersIfLayerChanged(layerHit);
		
		// Notify delegates of highest priority game object under mouse when clicked
		if (Input.GetMouseButton (0)) //mouse click enquiry, not needed in PLayerMovement anymore
		{
			notifyMouseClickObservers (priorityHit.Value, layerHit);
		}
	}

    //notifies the observers of a change in layer
	void NotifyObserersIfLayerChanged(int newLayer)
	{
		if (newLayer != topPriorityLayerLastFrame)
		{
			topPriorityLayerLastFrame = newLayer;
			notifyLayerChangeObservers (newLayer);
		}
	}

//************************************************
    //this is the most important method of this class
    RaycastHit? FindTopPriorityHit (RaycastHit[] raycastHits)
	{
		// Form list of layer numbers hit
		List<int> layersOfHitColliders = new List<int> ();
		foreach (RaycastHit hit in raycastHits)
		{
			layersOfHitColliders.Add (hit.collider.gameObject.layer);
		}

		// Step through layers in order of priority looking for a gameobject with that layer
		foreach (int layer in layerPriorities)
		{
			foreach (RaycastHit hit in raycastHits)
			{
				if (hit.collider.gameObject.layer == layer)
				{
					return hit; // stop looking
				}
			}
		}
		return null; // because cannot use GameObject? nullable
	}
//************************************************************
}