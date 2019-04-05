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

    enum SIDE { LEFT, RIGHT, TOP, BOTTOM };

    //Side the player is currently at
    SIDE currentSide = SIDE.BOTTOM;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPos = rb.position;

        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetButtonDown("Left"))
        {
            MovePlayer(-laneWideness);
            FindCurrentSide();
        }
        if (Input.GetButtonDown("Right"))
        {
            MovePlayer(laneWideness);
            FindCurrentSide();
        }
    }

    void FindCurrentSide()
    {
        if (currentPos.x == 6 && currentPos.y == -6)
        {
            if (currentSide == SIDE.BOTTOM)
            {
                currentSide = SIDE.RIGHT;
            }
            else
            {
                currentSide = SIDE.BOTTOM;
            }
        }
        if (currentPos.x == -6 && currentPos.y == -6)
        {
            if (currentSide == SIDE.BOTTOM)
            {
                currentSide = SIDE.LEFT;
            }
            else
            {
                currentSide = SIDE.BOTTOM;
            }

        }
        if (currentPos.x == 6 && currentPos.y == 6)
        {
            if (currentSide == SIDE.RIGHT)
            {
                currentSide = SIDE.TOP;
            }
            else
            {
                currentSide = SIDE.RIGHT;
            }
        }
        if (currentPos.x == -6 && currentPos.y == 6)
        {
            if (currentSide == SIDE.TOP)
            {
                currentSide = SIDE.LEFT;
            }
            else
            {
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

        rb.MovePosition(currentPos);
    }
}
