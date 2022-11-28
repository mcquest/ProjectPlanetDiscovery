using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float smooth = 2f;
    [SerializeField] int zDist = 30;
    [SerializeField] int yDist = 5;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 camTarget = new Vector3(target.position.x, target.position.y +yDist , target.position.z - zDist);
        transform.position = Vector3.Lerp
            (transform.position, camTarget, Time.deltaTime * smooth);
	}
}
