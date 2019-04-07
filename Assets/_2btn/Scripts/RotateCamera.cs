using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    private PlayerController player;

    private Camera cam;
    private Vector3 camPosition;
    private Quaternion camRotation;

    [SerializeField]
    private float transitionTime = 15.0f;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        camPosition = new Vector3(0, -3, -15);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate () {
        // if RotateCamera modifies camPosition Lerp to new position
        if (camPosition != cam.transform.position) {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPosition, transitionTime * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camRotation, transitionTime * Time.deltaTime);
        }
    }

    void Rotate () {
        switch (currentSide) {
            case SIDE.BOTTOM:
                camPosition = new Vector3(0, -3, -15);
                camRotation = Quaternion.Euler(0, 0, 0);
                break;
            case SIDE.LEFT:
                camPosition = new Vector3(-3, 0, -15);
                camRotation = Quaternion.Euler(0, 0, -90);
                break;
            case SIDE.TOP:
                camPosition = new Vector3(0, 3, -15);
                camRotation = Quaternion.Euler(0, 0, 180);
                break;
            case SIDE.RIGHT:
                camPosition = new Vector3(3, 0, -15);
                camRotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
}
