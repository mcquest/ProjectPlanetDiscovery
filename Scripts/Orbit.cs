using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{ 
    [SerializeField] Transform orbitPoint;
    [SerializeField] int orbitSpeed = 10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (orbitPoint == null)
        {
            return;
        }

        else
        {
            this.transform.RotateAround(orbitPoint.position,
                Vector3.forward, orbitSpeed * Time.deltaTime);
            this.transform.Rotate(0, 30, 40 * Time.deltaTime);

        }
    }
}
