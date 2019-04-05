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

    enum SpawnWall {
        Ground,
        Left,
        Right,
        Ceil
    };

	// Use this for initialization
	void Start () {
        rand = new System.Random();
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasSpawned) {
            StartCoroutine(SpawnObstacle());
        }
	}

    IEnumerator SpawnObstacle() {
        hasSpawned = true;
        
        // TODO: Stay on ground until player begins to rotate around walls
        System.Array values = SpawnWall.GetValues(typeof(SpawnWall));
        SpawnWall randomWall = (SpawnWall)values.GetValue(rand.Next(values.Length));

        Debug.Log("Spawn Wall: " + randomWall.ToString());

        // Spawn Position
        if (randomWall == SpawnWall.Ground) {                        
            xPos = Random.Range(minP, maxP);
            yPos = -6;            
        } else if (randomWall == SpawnWall.Ceil) {            
            xPos = Random.Range(minP, maxP);
            yPos = 6;
        } else if (randomWall == SpawnWall.Left) {                  
            yPos = Random.Range(minP, maxP);
            xPos = -6;
        } else if (randomWall == SpawnWall.Right) {            
            yPos = Random.Range(minP, maxP);
            xPos = 6;
        }

        // TODO: obstacles larger than 1 unit

        // magic number 88 because my wife loves 8s
        Instantiate(obstaclePrefab, new Vector3(xPos, yPos, 88.0f), Quaternion.identity);

        yield return new WaitForSeconds(spawnTime);

        hasSpawned = false;
    }
}
