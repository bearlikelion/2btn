using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private float spawnTime = 3.0f;

    [SerializeField]
    private GameObject obstaclePrefab;

    private bool hasSpawned = false;

	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasSpawned) {
            StartCoroutine(SpawnObstacle());
        }
	}

    IEnumerator SpawnObstacle() {
        hasSpawned = true;

        // TODO: Spawn obstacles on random walls

        float xPos = Random.Range(-6.0f, 6.0f);
        Instantiate(obstaclePrefab, new Vector3(xPos, 0, 88.0f), Quaternion.identity); // 88 magic number because my wife loves 8s

        yield return new WaitForSeconds(spawnTime);

        hasSpawned = false;
    }
}
