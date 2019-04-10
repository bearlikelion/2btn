using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleApproach : MonoBehaviour {

    [SerializeField]
    private float objectSpeed = 10.0f;

    private Rigidbody rb;

    private GameManager _gameManager;

    // Use this for initialization
    void Start() {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        objectSpeed += _gameManager.Difficulty;

        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.velocity = new Vector3(0, 0, -objectSpeed);
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.name == "Start") {
            _gameManager.ObstacleAvoided();
            Destroy(gameObject);
        }
    }
}
