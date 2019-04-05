using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleApproach : MonoBehaviour {

    [SerializeField]
    private float objectSpeed = 10.0f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate () {
        rb.velocity -= transform.forward * objectSpeed * Time.deltaTime;
        Debug.Log("Velocity: " + rb.velocity);
    }

    private void OnBecameInvisible () {
        Destroy(gameObject);
    }
}
