using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f; // seconds per oscillation


    // todo remove from inspector
    [Range(0,1)] [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved

    Vector3 startingPosition; //must be storred fo absolute movement
    
    // Use this for initialization
    void Start ()
    {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        float rawSinWave;
        if (period <= Mathf.Epsilon) { return; }
           
        float cycles = Time.time / period; // grows continually from 0
        const float tau = Mathf.PI * 2; // about 6.28
        rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f; //0<->1 
        
        Vector3 offset = movementVector * movementFactor; 
        transform.position = startingPosition + offset;
	}
}
