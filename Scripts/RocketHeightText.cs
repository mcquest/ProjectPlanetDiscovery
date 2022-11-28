using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketHeightText : MonoBehaviour {

    [SerializeField] Transform rocket;

    Text height;

	// Use this for initialization
	void Start ()
    {
        height = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        height.text = ((rocket.position.y - 2).ToString("0") + " m");
	}
}
