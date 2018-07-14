﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utility
{
    public class SpinFan : MonoBehaviour
    {

        [SerializeField] int spinDegrees;
        //the speed of degrees rotated per second for the fan 

        //this class was first used to spin the fan on the windmill
        //change if applied to the prefab will cause the fan to spin 25 degrees per second
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            // Rotate the object around its local Z axis at 1 degree per second
            transform.Rotate(Vector3.forward * Time.deltaTime * spinDegrees);
            //right = x
            //up = y
            //forward = z


        }
    }
}

