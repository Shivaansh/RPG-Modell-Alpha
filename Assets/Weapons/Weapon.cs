using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG / Weapon"))]
public class Weapon : ScriptableObject { //MonoBehaviour { -> ScriptableObject inherits
                                         //directly from object

    public Transform gripPos;
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] AnimationClip attackAimation;

	//// Use this for initialization
	//void Start () {
		
	//}
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public GameObject getPrefab()
    {
        return weaponPrefab;
    }
}
