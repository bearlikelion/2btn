using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    [SerializeField]
    private float objectSpeed = 10.0f;

    private Rigidbody rb;

    private GameManager _gameManager;
    private PlayerController playerController;
    private PlayerController.SIDE currentSide;

    // Use this for initialization
    void Start() {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objectSpeed += _gameManager.Difficulty;

        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.velocity = new Vector3(0, 0, -objectSpeed);

        FindSide();
    }

    void FindSide() {
        if (gameObject.transform.position.y == -6) {
            currentSide = PlayerController.SIDE.BOTTOM;
        } else if (gameObject.transform.position.x == 6) {
            currentSide = PlayerController.SIDE.RIGHT;
        } else if (gameObject.transform.position.y == 6) {
            currentSide = PlayerController.SIDE.TOP;
        } else if (gameObject.transform.position.x == -6 ) {
            currentSide = PlayerController.SIDE.LEFT;
        }
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.name == "Start") {
            if (playerController.currentSide == currentSide) {
                _gameManager.ObstacleAvoided();
            }
            Destroy(gameObject);
        }
    }
}
