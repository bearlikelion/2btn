using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    private float spawnTime = 3.0f;

    [SerializeField]
    private GameObject obstaclePrefab;

    private bool hasSpawned = false;    
    private System.Random rand;    
    private float xPos, yPos;

    private static int minP = -6;
    private static int maxP = 7; // Random.Range is EXCLUSIVE for max with Integers (stupid unity)

    private PlayerController player;

    enum SpawnWall {
        Ground,
        Left,
        Right,
        Ceil
    };

	// Use this for initialization
	void Start () {
        rand = new System.Random();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!hasSpawned) {
            StartCoroutine(SpawnObstacle());
        }
	}

    IEnumerator SpawnObstacle() {
        hasSpawned = true;
        
        // TODO: Spawn on player side        

        // Spawn obstacles on Player.SIDE
        switch (player.currentSide) {
            case PlayerController.SIDE.BOTTOM:
                xPos = Random.Range(minP, maxP);
                yPos = -6;
                break;
            case PlayerController.SIDE.LEFT:
                yPos = Random.Range(minP, maxP);
                xPos = -6;
                break;
            case PlayerController.SIDE.TOP:
                xPos = Random.Range(minP, maxP);
                yPos = 6;
                break;
            case PlayerController.SIDE.RIGHT:
                yPos = Random.Range(minP, maxP);
                xPos = 6;
                break;
        }        

        // TODO: obstacles larger than 1 unit

        // magic number 88 because my wife loves 8s
        Instantiate(obstaclePrefab, new Vector3(xPos, yPos, 88.0f), Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        hasSpawned = false;
    }
}
