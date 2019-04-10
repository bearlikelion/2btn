using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {


    public float angle = 0;

    public Transform panel;

    Quaternion newRotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Left"))
        {
            angle += 90;
            newRotation = Quaternion.Euler(0, 0, angle);

        }
        if (panel.transform.rotation != newRotation)
            panel.transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 10f * Time.deltaTime);
    }
}
