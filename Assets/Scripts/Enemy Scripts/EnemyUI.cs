using UnityEngine;

// Add a UI Socket transform to your enemy - Create a child GameObject for enemy prefab
// Attach this script to the socket - Drag into inspector for UI Socket GO
// Link to a canvas prefab
public class EnemyUI : MonoBehaviour {

    // Works around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField] GameObject enemyCanvasPrefab = null;

    Camera cameraToLookAt;
    //Vector3 pos = new Vector3(0f, 0f, 0f); //a test variable

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        Instantiate(enemyCanvasPrefab, transform.position, transform.rotation, transform);
        //pos is a test variable

        //TODO:FIX ERROR
        /**
         * When instantiated, the enemyCanvas is instantiated with an offset, instead of being instantiated at the enemy pposition.
         * Also, the scale of the canvas should 0.05 oon X and Y scale. 
         * 
         * Possible fix: reset transform of enemy canvas prefab.
         * SOLUTION: Need to reset the transform and scale of enemy canvas prefab before adding to enemy prefabs
         */
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}