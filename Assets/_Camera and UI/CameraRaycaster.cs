using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using RPG.Character; //include Enemy type

namespace RPG.CameraUI
{
    public class CameraRaycaster : MonoBehaviour // TODO rename to Cursor
    {
        //(Editor script: CameraRaycasterEditor needs to be removed for these changes to reflect in inspector)

        [SerializeField] Texture2D walkCursor = null; //walk cursor sprite
        [SerializeField] Texture2D enemyCursor = null; //enemy cursor sprite
        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0); //position of cursor image w.r.t. actual mouse position

        const int POTENTIALLY_WALKABLE_LAYER = 8; //Walkable layer index
        float maxRaycastDepth = 100f; // Hard coded value

        //TODO remove 
        int topPriorityLayerLastFrame = -1; // So get ? from start with Default layer terrain

        // New delegates...
        public delegate void OnMouseOverEnemy(Enemy enemy);
        public event OnMouseOverEnemy onMouseOverEnemy;

        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverPotentiallyWalkable;

        // Setup delegates for broadcasting layer changes to other classes

        void Update() //try to keep this block less occupied
        {
            // Check if pointer is over an interactable UI element
            if (EventSystem.current.IsPointerOverGameObject()) //if pointer is over a UI GamObject
            {
                // Implement UI interaction
            }
            else
            {
                performRaycasts();
            }
        }

        void performRaycasts()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //create ray
            // Specify layer priorities below
            //ORDER MATTERS
            if (RaycastForEnemy(ray)) { return; }
            if (RaycastForPotentiallyWalkable(ray)) { return; }
        }
        bool RaycastForEnemy(Ray ray)
        {
            RaycastHit hitInfo; //store raycast info
            Physics.Raycast(ray, out hitInfo, maxRaycastDepth);//perform raycast
            var gameObjectHit = hitInfo.collider.gameObject; //store the gameobject hit by the raycast
            var enemyHit = gameObjectHit.GetComponent<Enemy>(); //if an Enemy script exists on GameObject
            if (enemyHit)
            {
                Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverEnemy(enemyHit);
                return true;
            }
            return false;
        }

        private bool RaycastForPotentiallyWalkable(Ray ray)
        {
            RaycastHit hitInfo; //stores raycast hit information
            LayerMask potentiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER; //bitshift
            bool potentiallyWalkableHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer); //check if potentially walkable layer is hit
            if (potentiallyWalkableHit)
            {
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverPotentiallyWalkable(hitInfo.point);
                return true;
            }
            return false;
        }
    }
}