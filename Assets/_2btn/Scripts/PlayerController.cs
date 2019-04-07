using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool wallClimb = false;

    //Wideness of the lanes
    public float laneWideness;

    private Rigidbody rb;

    //Current player position
    private Vector3 currentPos;

    private RotateCamera cam;

    Renderer rend;
    Vector3 targetRot;

    float angle = 0;

    public enum SIDE {
        TOP,
        LEFT,
        RIGHT,
        BOTTOM
    };

    //Side the player is currently at
    public SIDE currentSide = SIDE.BOTTOM;

    void Start () {
        cam = Camera.main.GetComponent<RotateCamera>();
        rb = GetComponent<Rigidbody>();
        currentPos = rb.position;
        rend = GetComponent<Renderer>();
        targetRot = rend.bounds.min;
    }

    // Update is called once per frame
    void Update () {
        ControlPlayer();
    }

    void ControlPlayer () {
        if (Input.GetButtonDown("Left")) {
            FindCurrentSide();
            if (transform.position.x == currentPos.x)
            {
                MovePlayer(-laneWideness);
                angle += 90;
                StartCoroutine("RotateLeft");
            }
        }
        if (Input.GetButtonDown("Right")) {
            FindCurrentSide();
            //MovePlayer(laneWideness);
            if (transform.position.x == currentPos.x)
            {
                MovePlayer(laneWideness);
                angle -= 90;
                StartCoroutine("RotateRight");
            }
        }

        if(angle == 360)
        {
            angle = 0;
        }
    }

    //Change side based on input, moves left <-> right depengin on button pressed
    void ChangeSide (SIDE left, SIDE right) {
        if (Input.GetButtonDown("Left")) {
            currentSide = left;
        }
        if (Input.GetButtonDown("Right")) {
            currentSide = right;
        }

        if (!wallClimb) {
            wallClimb = true;
        }

        cam.Rotate();
    }

    //If player is in specific position check for input and change side
    void FindCurrentSide () {
        if (currentPos.x == 6 && currentPos.y == -6) {
            ChangeSide(SIDE.BOTTOM, SIDE.RIGHT);
        }
        if (currentPos.x == -6 && currentPos.y == -6) {
            ChangeSide(SIDE.LEFT, SIDE.BOTTOM);
        }
        if (currentPos.x == 6 && currentPos.y == 6) {
            ChangeSide(SIDE.RIGHT, SIDE.TOP);
        }
        if (currentPos.x == -6 && currentPos.y == 6) {
            ChangeSide(SIDE.TOP, SIDE.LEFT);
        }
    }

    //Move player based on side
    void MovePlayer (float moveDistance) {
        switch (currentSide) {
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
        //rb.MovePosition(currentPos);
    }

    //fix and clean this shit
    IEnumerator RotateLeft()
    {
        switch (currentSide)
        {
            case (SIDE.BOTTOM):
                targetRot = rend.bounds.min;
                while (transform.position.x > currentPos.x + 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, 1000 * Time.deltaTime);
                    yield return null;
                }
                break;
            case (SIDE.LEFT):
                targetRot = rend.bounds.min;
                targetRot.y += 1;
                while (transform.position.y < currentPos.y - 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, 1000 * Time.deltaTime);
                    yield return null;
                }
                break;
            case (SIDE.RIGHT):
                targetRot = rend.bounds.max;
                targetRot.y -= 1;
                while (transform.position.y > currentPos.y + 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, 1000 * Time.deltaTime);
                    yield return null;
                }
                break;
            case (SIDE.TOP):
                targetRot = rend.bounds.max;
                while (transform.position.x < currentPos.x - 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, 1000 * Time.deltaTime);
                    yield return null;
                }
                break;
        }
        transform.position = currentPos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator RotateRight()
    {
        switch (currentSide)
        {
            case (SIDE.BOTTOM):
                targetRot = rend.bounds.min;
                targetRot.x += 1;
                while (transform.position.x < currentPos.x - 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, -1000 * Time.deltaTime);
                    yield return null;
                }
                break;
            case (SIDE.LEFT):
                targetRot = rend.bounds.min;
                while (transform.position.y > currentPos.y + 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, -1000 * Time.deltaTime);
                    yield return null;
                }
                break;
            case (SIDE.RIGHT):
                targetRot = rend.bounds.max;
                while (transform.position.y < currentPos.y - 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, -1000 * Time.deltaTime);
                    yield return null;
                }
                break;
            case (SIDE.TOP):
                targetRot = rend.bounds.max;
                targetRot.x -= 1;
                while (transform.position.x > currentPos.x + 0.1)
                {
                    transform.RotateAround(targetRot, Vector3.forward, -1000 * Time.deltaTime);
                    yield return null;
                }
                break;
        }
        transform.position = currentPos;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
