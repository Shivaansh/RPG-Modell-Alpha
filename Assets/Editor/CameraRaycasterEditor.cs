using UnityEditor;
//Editor scripts must ALWAYS import UnityEditor namespace

// TODO consider changing to a property drawer
namespace RPG.CameraUI
{
    [CustomEditor(typeof(CameraRaycaster))]
    public class CameraRaycasterEditor : Editor //Editor scripts are NOT inherited from MonoBehavior, rather from Editor
    {
        bool isLayerPrioritiesUnfolded = true; // store the UI state (drop down is unfolded by default)

        public override void OnInspectorGUI()
        //called every time the inspector GUI is brought up 
        {
            serializedObject.Update(); // Serialize cameraRaycaster instance - makes it into a saveable format

            //done in memory
            //creates a drop down menu
            //Foldout method creates a label with a foldout arrow to the left, the string is the content of the label
            isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
            //the '=' sign not only draws the label but also assigns a value to a variable which can then
            //define the behavior of the drop down  menu
            if (isLayerPrioritiesUnfolded) //if unfolded (dropped down)
            {
                EditorGUI.indentLevel++;
                {
                    BindArraySize(); //draw array size (let's say size is x)
                    BindArrayElements(); //draw x elements of array

                }
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties(); // applies modified properties back to serialized object
        }

        // ******************************* HELPER METHODS ************************


        void BindArraySize()
        {
            int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
            int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
            if (requiredArraySize != currentArraySize)
            {
                //find new value of array size and store it in requiredArraySize, ONLY IN MEMORY
                serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
            }
        }

        void BindArrayElements()
        {
            int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
            for (int i = 0; i < currentArraySize; i++)
            {
                //form a string to find a property, find property from array (the 0 is replaced with the value of i)
                //basically creates field boxes in the inspector, in this case a list of layers
                var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
                prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
            }
        }
    }
}

