using UnityEngine;


// SECTION 1, Lecture 10: Watch Unity RPG course for full information on how this works, for reference
public class CameraRaycaster : MonoBehaviour
{
    void Start()
    {
        viewCamera = Camera.main;
        //finds main camera and assigns to viewCamera variable, camera should be tagged appropriately

        
        /*
         * We do not use () with layerChangeHandler because layerChangeObservers is a SET OF FUNCTIONS and this statement
         * aims to imply that we are adding the layerChangeHandler function to the set of functions. THIS IS NOT A FUNCTION CALL
         * NOTE: can be protected using the event keyword
         */
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities) //loops through layer hit
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                m_hit = hit.Value;
                if (m_layerHit != layer) //if the layer has changed
                {
                    m_layerHit = layer;
                    layerChangeObservers(); //calling delegates
                }
                
                return;
            }
        }
        // Otherwise return background hit
        //this is an artificial hit

        //allows unknown cursor to work
        m_hit.distance = distanceToBackground;
        if(m_layerHit != Layer.RaycastEndStop)
        {
            m_layerHit = Layer.RaycastEndStop;
            layerChangeObservers(); //calling delegates
        }
    }

    // ? for variable return types, RaycastHit is non nullable, ? allows the null return type
    RaycastHit? RaycastForLayer(Layer layer)
    {
        //forms layer mask
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
                                         //bit shift example, quick on hardware level
        //given a screen point, turn it into a ray
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        //out parameter
        RaycastHit hit; // used as an out parameter

        //hit stores the result of raycast for the ray 'ray', for a max distance of distanceToBackground
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);
        if (hasHit)
        {
            return hit;
        }
        return null;
    }



    // *********************** HELPER METHODS **********************

    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float distanceToBackground = 100f; //max raycast distance
    //[SerializeField] behaves like final in Java for other classes
    //other classes cannot edit this value
    Camera viewCamera;

    //RaycastHit is a rich type which gives lot of info about what the ray hits
    RaycastHit m_hit;

    //getter method
    public RaycastHit hit
    {
        get { return m_hit; }
    }

    //getter method
    Layer m_layerHit;
    public Layer layerHit
    {
        get { return m_layerHit; }
    }

    /**************************************OBSERVER PATTERN IMPLEMENTATION METHODS******************************************/


    //declare new delegate type; for Observer Pattern implementation
    public delegate void OnLayerChange();
    public event OnLayerChange layerChangeObservers; //instantiating set of observers 


    

  

}
