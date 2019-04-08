using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleApproach : MonoBehaviour {

    [SerializeField]
    private float objectSpeed = 10.0f;

    private Rigidbody rb;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        rb.velocity -= transform.forward * objectSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter (Collision collision) {
        if (collision.gameObject.name == "Start") {
            Destroy(gameObject);
        }
    }
}
