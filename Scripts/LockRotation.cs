using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{

    int xLock = -9;
    int yLock = 0;
    int zLock = 0;

    // Use this for initialization
    void Start ()
    {
        //Quaternion fixedRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.rotation = Quaternion.Euler(xLock, yLock, zLock);
	}
}
