﻿using UnityEngine;
using System.Collections;

public class LoopRotate : MonoBehaviour
{
    public Vector3 rotation;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime);
    }
}
