using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Weapons
{
    [CreateAssetMenu(menuName = ("RPG / Weapon"))]
    public class Weapon : ScriptableObject
    { //MonoBehaviour { -> ScriptableObject inherits
      //directly from object

        public Transform gripPos;
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;

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
}

