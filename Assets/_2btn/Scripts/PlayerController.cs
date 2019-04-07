using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Wideness of the lanes
    public float laneWideness;

    private Rigidbody rb;

    //Current player position
    private Vector3 currentPos;

    private RotateCamera cam;

    public enum SIDE {
        TOP,
        LEFT,
        RIGHT,
        BOTTOM
    };

    //Side the player is currently at
    public SIDE currentSide = SIDE.BOTTOM;

    void Start()
    {
        cam = Camera.main.GetComponent<RotateCamera>();
        rb = GetComponent<Rigidbody>();
        currentPos = rb.position;

        // rb.freezeRotation = true;
        // rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();
    }

    void ControlPlayer()
    {
        if (Input.GetButtonDown("Left"))
        {
            FindCurrentSide();
            MovePlayer(-laneWideness);
        }
        if (Input.GetButtonDown("Right"))
        {
            FindCurrentSide();
            MovePlayer(laneWideness);
        }
    }

    //Change side based on input, moves left <-> right depengin on button pressed
    void ChangeSide(SIDE left, SIDE right)
    {
        if (Input.GetButtonDown("Left"))
        {
            currentSide = left;
        }
        if (Input.GetButtonDown("Right"))
        {
            currentSide = right;
        }

        cam.Rotate();
    }

    //If player is in specific position check for input and change side
    void FindCurrentSide()
    {
        if (currentPos.x == 6 && currentPos.y == -6)
        {
            ChangeSide(SIDE.BOTTOM, SIDE.RIGHT);
        }
        if (currentPos.x == -6 && currentPos.y == -6)
        {
            ChangeSide(SIDE.LEFT, SIDE.BOTTOM);
        }
        if (currentPos.x == 6 && currentPos.y == 6)
        {
            ChangeSide(SIDE.RIGHT, SIDE.TOP);
        }
        if (currentPos.x == -6 && currentPos.y == 6)
        {
            ChangeSide(SIDE.TOP, SIDE.LEFT);
        }
    }

    //Move player based on side
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

        rb.MovePosition(currentPos);
    }

    void RotatePlayer(float angle)
    {
        Quaternion currentRot = rb.rotation;
        currentRot *= Quaternion.Euler(0, 0, angle);
        rb.rotation = currentRot;
    }
}
