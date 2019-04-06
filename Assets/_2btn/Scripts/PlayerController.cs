using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //How wide the lanes are
    public float laneWideness;

    private Rigidbody rb;

    //current pos of the player
    private Vector3 currentPos;

    private Camera cam;
    private Vector3 camPosition;
    private Quaternion camRotation;

    [SerializeField]
    private float transitionTime = 15.0f;

    enum SIDE { LEFT, RIGHT, TOP, BOTTOM };

    //Side the player is currently at
    SIDE currentSide = SIDE.BOTTOM;

    void Start()
    {
        cam = Camera.main;
        camPosition = new Vector3(0, -3, -15);

        rb = GetComponent<Rigidbody>();        
        currentPos = rb.position;        

        // rb.freezeRotation = true; // I like the rotation on collision
        // rb.useGravity = false; // Set in editor
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void LateUpdate () {
        // if RotateCamera modifies camPosition Lerp to new position
        if (camPosition != cam.transform.position) {
            cam.transform.position = Vector3.Lerp(cam.transform.position, camPosition, transitionTime * Time.deltaTime);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, camRotation, transitionTime * Time.deltaTime);
        }            
    }

    void GetInput() 
    { 
        if (Input.GetButtonDown("Left")) {
            MovePlayer(-laneWideness);
            FindCurrentSide();
        }

        if (Input.GetButtonDown("Right")) {
            MovePlayer(laneWideness);
            FindCurrentSide();
        }
    }

    void RotateCamera () {
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

    void FindCurrentSide()
    {
        if (currentPos.x == 6 && currentPos.y == -6) {
            if (currentSide == SIDE.BOTTOM) {
                currentSide = SIDE.RIGHT;                        
            } else {
                currentSide = SIDE.BOTTOM;
            }
        }

        if (currentPos.x == -6 && currentPos.y == -6) {
            if (currentSide == SIDE.BOTTOM) {
                currentSide = SIDE.LEFT;                
            } else {
                currentSide = SIDE.BOTTOM;
            }
        }

        if (currentPos.x == 6 && currentPos.y == 6) {
            if (currentSide == SIDE.RIGHT) {
                currentSide = SIDE.TOP;
            } else {
                currentSide = SIDE.RIGHT;
            }
        }

        if (currentPos.x == -6 && currentPos.y == 6) {
            if (currentSide == SIDE.TOP) {
                currentSide = SIDE.LEFT;
            } else {
                currentSide = SIDE.TOP;
            }
        }
    }

    void MovePlayer(float moveDistance)
    {
        switch (currentSide)
        {
            case SIDE.BOTTOM:
                currentPos.x += moveDistance;
                break;
            case SIDE.TOP:
                currentPos.x -= moveDistance;
                break;
            case SIDE.LEFT:
                currentPos.y -= moveDistance;
                break;
            case SIDE.RIGHT:
                currentPos.y += moveDistance;
                break;
        }

        Debug.Log("Current Side: " + currentSide.ToString());

        rb.MovePosition(currentPos);
        RotateCamera();
    }
}
