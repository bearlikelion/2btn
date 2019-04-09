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
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        Destroy(gameObject, 10f);
        rb.velocity = new Vector3(0, 0, -objectSpeed);
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
